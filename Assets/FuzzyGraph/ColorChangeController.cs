using FuzzyGraph.Runtime;
using System;
using UnityEngine;

public class ColorChangeController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ProximitySensor proximitySensor;
    [SerializeField] private Renderer blushRenderer;
    [SerializeField]
    private Transform playerFacingTransform;

    [Header("Blush Appearance")]
    [SerializeField]
    private Color blushColor =
        new Color(1f, 0.12f, 0.22f, 1f);

    [SerializeField, Range(0f, 1f)]
    private float maximumBlush = 1f;

    [SerializeField, Min(0.01f)]
    private float fadeSpeed = 4f;

    [Header("Player Facing Guard")]
    [Tooltip("Blush begins fading when the Player faces roughly toward the Guard.")]
    [SerializeField, Range(-1f, 1f)]
    private float facingFadeStart = 0.70f;

    [Tooltip("Blush is completely hidden when the Player faces this directly toward the Guard.")]
    [SerializeField, Range(-1f, 1f)]
    private float fullyFacingGuard = 0.92f;

    //[Tooltip("Blush is completely hidden at this camera alignment.")]
    //[SerializeField, Range(-1f, 1f)]
    //private float fullyLookingAt = 0.90f;

    [Header("Runtime")]
    [SerializeField, Range(0f, 1f)]
    private float displayedBlush;

    private static readonly int BaseColorID =
        Shader.PropertyToID("_BaseColor");

    private static readonly int ColorID =
        Shader.PropertyToID("_Color");

    private MaterialPropertyBlock propertyBlock;
    private Color normalColor = Color.white;
    private int activeColorProperty = BaseColorID;

    private void Reset()
    {
        blushRenderer = GetComponent<Renderer>();
    }

    private void Awake()
    {
        propertyBlock = new MaterialPropertyBlock();

        ResolvePlayerFacingTransform();

        CacheNormalColour();
        ApplyBlushColour(0f);
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

        float targetBlush = CalculateTargetBlush();

        displayedBlush = Mathf.MoveTowards(
            displayedBlush,
            targetBlush,
            fadeSpeed * Time.deltaTime);

        ApplyBlushColour(displayedBlush);
    }

    private float CalculateTargetBlush()
    {
        if (proximitySensor == null ||
            blushRenderer == null ||
            FuzzyGraphGameService.Instance == null)
        {
            return 0f;
        }

        // The guard should not react before meeting the player.
        if (!ReadContextBool("Guard.HasMetPlayer"))
            return 0f;

        // Picking flowers or reaching a final guard outcome
        // immediately removes the blush.
        if (ReadContextBool("Player.HasFlowers") ||
            ReadContextBool("Guard.Charmed") ||
            ReadContextBool("Player.Arrested"))
        {
            return 0f;
        }

        float proximityStrength =
            proximitySensor.CurrentProximity;

        float facingSuppression = CalculateFacingSuppression();

        return Mathf.Clamp01(proximityStrength * (1f - facingSuppression) * maximumBlush);
    }

    private float CalculateFacingSuppression()
    {
        if (playerFacingTransform == null ||
            blushRenderer == null)
        {
            return 0f;
        }

        Vector3 directionToGuard =
            blushRenderer.bounds.center -
            playerFacingTransform.position;

        // Ignore vertical height difference.
        // We only care about the Player's horizontal rotation.
        directionToGuard.y = 0f;

        if (directionToGuard.sqrMagnitude < 0.001f)
            return 1f;

        directionToGuard.Normalize();

        Vector3 playerForward =
            playerFacingTransform.forward;

        playerForward.y = 0f;

        if (playerForward.sqrMagnitude < 0.001f)
            return 0f;

        playerForward.Normalize();

        float alignment = Vector3.Dot(
            playerForward,
            directionToGuard);

        return Mathf.InverseLerp(
            facingFadeStart,
            fullyFacingGuard,
            alignment);
    }

    private bool ReadContextBool(string key)
    {
        PersistentContext context =
            FuzzyGraphGameService.Instance.Context;

        bool found = context.TryGet(
            key,
            out FuzzyValue value);

        return found &&
               value.type == FuzzyValueType.Bool &&
               value.boolVal;
    }

    private void CacheNormalColour()
    {
        if (blushRenderer == null ||
            blushRenderer.sharedMaterial == null)
        {
            return;
        }

        Material material =
            blushRenderer.sharedMaterial;

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

    private void ApplyBlushColour(float strength)
    {
        if (blushRenderer == null ||
            propertyBlock == null)
        {
            return;
        }

        Color finalColor = Color.Lerp(
            normalColor,
            blushColor,
            Mathf.Clamp01(strength));

        blushRenderer.GetPropertyBlock(
            propertyBlock);

        propertyBlock.SetColor(
            activeColorProperty,
            finalColor);

        blushRenderer.SetPropertyBlock(
            propertyBlock);
    }

    private void OnDisable()
    {
        displayedBlush = 0f;
        ApplyBlushColour(0f);
    }

    private void OnValidate()
    {
        if (fullyFacingGuard <= facingFadeStart)
        {
            fullyFacingGuard =
                Mathf.Min(
                    1f,
                    facingFadeStart + 0.01f);
        }
    }
}