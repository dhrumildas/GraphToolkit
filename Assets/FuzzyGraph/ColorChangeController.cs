using FuzzyGraph.Runtime;
using System;
using UnityEngine;

public class ColorChangeController : MonoBehaviour
{
    [Header("Context Conditions")]
    [SerializeField]
    private string requiredTrueKey;

    [SerializeField]
    private string[] suppressWhenTrueKeys;

    [SerializeField]
    private bool useProximity = true;
    private float manualStrength;

    [Header("References")]
    [SerializeField] private ProximitySensor proximitySensor;
    [SerializeField] private Renderer targetRenderer;
    [SerializeField]
    private Transform playerFacingTransform;

    [Header("Color Change Appearance")]
    [SerializeField]
    private Color reactionColor =
        new Color(1f, 0.12f, 0.22f, 1f);

    [SerializeField, Range(0f, 1f)]
    private float maximumStrength = 1f;

    [SerializeField, Min(0.01f)]
    private float fadeSpeed = 4f;

    [Header("Player Facing Target")]
    [Tooltip("Color change begins fading when the Player faces roughly toward the Target.")]
    [SerializeField, Range(-1f, 1f)]
    private float facingFadeStart = 0.70f;

    [Header("Facing Behaviour")]
    [SerializeField]
    private bool useFacingSuppression = true;

    [Tooltip("Color change is completely hidden when the Player faces this directly toward the Target.")]
    [SerializeField, Range(-1f, 1f)]
    private float fullyFacingTarget = 0.92f;

    //[Tooltip("Blush is completely hidden at this camera alignment.")]
    //[SerializeField, Range(-1f, 1f)]
    //private float fullyLookingAt = 0.90f;

    [Header("Runtime")]
    [SerializeField, Range(0f, 1f)]
    private float displayedStrength;

    private static readonly int BaseColorID =
        Shader.PropertyToID("_BaseColor");

    private static readonly int ColorID =
        Shader.PropertyToID("_Color");

    private MaterialPropertyBlock propertyBlock;
    private Color normalColor = Color.white;
    private int activeColorProperty = BaseColorID;

    private void Reset()
    {
        targetRenderer = GetComponent<Renderer>();
    }

    private void Awake()
    {
        propertyBlock = new MaterialPropertyBlock();

        ResolvePlayerFacingTransform();

        CacheNormalColour();
        ApplyReactionColor(0f);
    }

    private void ResolvePlayerFacingTransform()
    {
        if (playerFacingTransform != null)
            return;

        TPP_Controller controller =
            FindAnyObjectByType<TPP_Controller>();

        if (controller != null)
        {
            playerFacingTransform =
                controller.transform;
        }
    }

    private void Update()
    {
        ResolvePlayerFacingTransform();

        float targetStrength = CalculateTargetStrength();

        displayedStrength = Mathf.MoveTowards(
            displayedStrength,
            targetStrength,
            fadeSpeed * Time.deltaTime);

        ApplyReactionColor(displayedStrength);
    }

    private float CalculateTargetStrength()
    {
        if (targetRenderer == null)
            return 0f;

        // Optional required context condition.
        if (!string.IsNullOrWhiteSpace(requiredTrueKey))
        {
            if (FG2GameServices.Instance == null ||
                !ReadContextBool(requiredTrueKey))
            {
                return 0f;
            }
        }

        // Optional suppression context conditions.
        if (suppressWhenTrueKeys != null)
        {
            foreach (string key in suppressWhenTrueKeys)
            {
                if (string.IsNullOrWhiteSpace(key))
                    continue;

                if (FG2GameServices.Instance == null)
                    return 0f;

                if (ReadContextBool(key))
                    return 0f;
            }
        }

        float sourceStrength;

        if (useProximity)
        {
            if (proximitySensor == null)
                return 0f;

            sourceStrength = proximitySensor.CurrentProximity;
        }
        else
        {
            sourceStrength = manualStrength;
        }

        float facingSuppression =
            useFacingSuppression
                ? CalculateFacingSuppression()
                : 0f;

        return Mathf.Clamp01(
            sourceStrength *
            (1f - facingSuppression) *
            maximumStrength);
    }

    private float CalculateFacingSuppression()
    {
        if (playerFacingTransform == null ||
            targetRenderer == null)
        {
            return 0f;
        }

        Vector3 directionToTarget = targetRenderer.bounds.center - playerFacingTransform.position;

        // Ignore vertical height difference.
        // We only care about the Player's horizontal rotation.
        directionToTarget.y = 0f;

        if (directionToTarget.sqrMagnitude < 0.001f)
            return 1f;

        directionToTarget.Normalize();

        Vector3 playerForward =
            playerFacingTransform.forward;

        playerForward.y = 0f;

        if (playerForward.sqrMagnitude < 0.001f)
            return 0f;

        playerForward.Normalize();

        float alignment = Vector3.Dot(
            playerForward,
            directionToTarget);

        return Mathf.InverseLerp(
            facingFadeStart,
            fullyFacingTarget,
            alignment);
    }

    private bool ReadContextBool(string key)
    {
        PersistentContext context =
            FG2GameServices.Instance.Context;

        bool found = context.TryGet(
            key,
            out FuzzyValue value);

        return found &&
               value.type == FuzzyValueType.Bool &&
               value.boolVal;
    }

    private void CacheNormalColour()
    {
        if (targetRenderer == null ||
            targetRenderer.sharedMaterial == null)
        {
            return;
        }

        Material material =
            targetRenderer.sharedMaterial;

        if (material.HasProperty(BaseColorID))
        {
            activeColorProperty = BaseColorID;
            normalColor =
                material.GetColor(BaseColorID);
        }
        else if (material.HasProperty(ColorID))
        {
            activeColorProperty = ColorID;
            normalColor =
                material.GetColor(ColorID);
        }
        else
        {
            Debug.LogWarning(
                $"{material.name} has no supported colour property.",
                this);
        }
    }

    private void ApplyReactionColor(float strength)
    {
        if (targetRenderer == null ||
            propertyBlock == null)
        {
            return;
        }

        Color finalColor = Color.Lerp(
            normalColor,
            reactionColor,
            Mathf.Clamp01(strength));

        targetRenderer.GetPropertyBlock(
            propertyBlock);

        propertyBlock.SetColor(
            activeColorProperty,
            finalColor);

        targetRenderer.SetPropertyBlock(
            propertyBlock);
    }

    private void OnDisable()
    {
        displayedStrength = 0f;
        ApplyReactionColor(0f);
    }

    private void OnValidate()
    {
        if (fullyFacingTarget <= facingFadeStart)
        {
            fullyFacingTarget =
                Mathf.Min(
                    1f,
                    facingFadeStart + 0.01f);
        }
    }

    public void ShowReaction()
    {
        manualStrength = 1f;
    }
    public void HideReaction()
    {
        manualStrength = 0f;
    }
}