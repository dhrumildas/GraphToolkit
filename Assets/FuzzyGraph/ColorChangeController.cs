using FuzzyGraph.Runtime;
using System;
using UnityEngine;

public class ColorChangeController : MonoBehaviour
{
    [Header("Context Driven")]
    [SerializeField] private bool useContextDrivenReaction = false;

    [Tooltip("Numeric context key driving the reaction.")]
    [SerializeField] private string reactionDriveKey;
    [SerializeField] private float driveStartsAt = 3f;
    [SerializeField] private float driveFullyActiveAt = 6f;

    [Tooltip("Optional numeric key to suppress the reaction.")]
    [SerializeField] private string reactionSuppressKey;
    [SerializeField] private float suppressionStartsAt = 5f;
    [SerializeField] private float suppressionFullyActiveAt = 8f;

    [Header("Conditions")]
    [SerializeField] private string requiredTrueKey;
    [SerializeField] private string[] suppressWhenTrueKeys;
    [SerializeField] private bool useProximity = true;

    private float manualStrength;

    [Header("References")]
    [SerializeField] private ProximitySensor proximitySensor;
    [SerializeField] private Renderer targetRenderer;
    [SerializeField] private Transform playerFacingTransform;

    [Header("Appearance")]
    [SerializeField] private Color reactionColor = new Color(1f, 0.12f, 0.22f, 1f);
    [SerializeField, Range(0f, 1f)] private float maximumStrength = 1f;
    [SerializeField, Min(0.01f)] private float fadeSpeed = 4f;

    [Header("Facing Behaviour")]
    [SerializeField] private bool useFacingSuppression = true;

    [Tooltip("Starts fading when observer faces the target.")]
    [SerializeField, Range(-1f, 1f)] private float facingFadeStart = 0.70f;

    [Tooltip("Completely hidden at this alignment.")]
    [SerializeField, Range(-1f, 1f)] private float fullyFacingTarget = 0.92f;

    [Header("Runtime")]
    [SerializeField, Range(0f, 1f)] private float displayedStrength;

    private static readonly int BaseColorID = Shader.PropertyToID("_BaseColor");
    private static readonly int ColorID = Shader.PropertyToID("_Color");

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
        if (playerFacingTransform != null) return;

        TPP_Controller controller = FindAnyObjectByType<TPP_Controller>();
        if (controller != null) playerFacingTransform = controller.transform;
    }

    private void Update()
    {
        ResolvePlayerFacingTransform();

        float contextDrive = CalculateContextReactionDrive();
        float targetStrength = CalculateTargetStrength(contextDrive);
        float effectiveFadeSpeed = fadeSpeed;

        if (useContextDrivenReaction)
        {
            // higher drive = faster reaction.
            effectiveFadeSpeed = Mathf.Lerp(fadeSpeed * 0.75f, fadeSpeed * 2f, contextDrive);
        }

        displayedStrength = Mathf.MoveTowards(displayedStrength, targetStrength, effectiveFadeSpeed * Time.deltaTime);

        ApplyReactionColor(displayedStrength);
    }

    private float CalculateTargetStrength()
    {
        if (targetRenderer == null) return 0f;

        // check required context.
        if (!string.IsNullOrWhiteSpace(requiredTrueKey))
        {
            if (FG2GameServices.Instance == null || !ReadContextBool(requiredTrueKey)) return 0f;
        }

        // check suppression context.
        if (suppressWhenTrueKeys != null)
        {
            foreach (string key in suppressWhenTrueKeys)
            {
                if (string.IsNullOrWhiteSpace(key)) continue;
                if (FG2GameServices.Instance == null) return 0f;
                if (ReadContextBool(key)) return 0f;
            }
        }

        float sourceStrength = useProximity && proximitySensor != null
            ? proximitySensor.CurrentProximity
            : manualStrength;

        float facingSuppression = useFacingSuppression ? CalculateFacingSuppression() : 0f;

        return Mathf.Clamp01(sourceStrength * (1f - facingSuppression) * maximumStrength);
    }

    private float CalculateTargetStrength(float contextDrive)
    {
        if (targetRenderer == null) return 0f;

        // check required context.
        if (!string.IsNullOrWhiteSpace(requiredTrueKey))
        {
            if (FG2GameServices.Instance == null || !ReadContextBool(requiredTrueKey)) return 0f;
        }

        // check suppression context.
        if (suppressWhenTrueKeys != null)
        {
            foreach (string key in suppressWhenTrueKeys)
            {
                if (string.IsNullOrWhiteSpace(key)) continue;
                if (FG2GameServices.Instance == null) return 0f;
                if (ReadContextBool(key)) return 0f;
            }
        }

        float sourceStrength = useProximity && proximitySensor != null
            ? proximitySensor.CurrentProximity
            : manualStrength;

        if (useContextDrivenReaction)
        {
            // no drive = no reaction.
            if (contextDrive <= 0f) return 0f;

            // higher drive expands effective visual range.
            float proximityExponent = Mathf.Lerp(1.4f, 0.55f, contextDrive);
            sourceStrength = Mathf.Pow(Mathf.Clamp01(sourceStrength), proximityExponent);

            // higher drive strengthens colour.
            sourceStrength *= Mathf.Lerp(0.4f, 1f, contextDrive);
        }

        float facingSuppression = useFacingSuppression ? CalculateFacingSuppression() : 0f;

        return Mathf.Clamp01(sourceStrength * (1f - facingSuppression) * maximumStrength);
    }

    private float CalculateFacingSuppression()
    {
        if (playerFacingTransform == null || targetRenderer == null) return 0f;

        Vector3 directionToTarget = targetRenderer.bounds.center - playerFacingTransform.position;

        // ignore vertical height.
        directionToTarget.y = 0f;

        if (directionToTarget.sqrMagnitude < 0.001f) return 1f;
        directionToTarget.Normalize();

        Vector3 playerForward = playerFacingTransform.forward;
        playerForward.y = 0f;

        if (playerForward.sqrMagnitude < 0.001f) return 0f;
        playerForward.Normalize();

        float alignment = Vector3.Dot(playerForward, directionToTarget);

        return Mathf.InverseLerp(facingFadeStart, fullyFacingTarget, alignment);
    }

    private bool TryReadContextNumber(string key, out float number)
    {
        number = 0f;

        if (FG2GameServices.Instance == null || string.IsNullOrWhiteSpace(key)) return false;

        PersistentContext context = FG2GameServices.Instance.Context;
        if (!context.TryGet(key, out FuzzyValue value)) return false;

        if (value.type == FuzzyValueType.Float)
        {
            number = value.floatVal;
            return true;
        }

        if (value.type == FuzzyValueType.Int)
        {
            number = value.intVal;
            return true;
        }

        return false;
    }

    private float CalculateContextReactionDrive()
    {
        if (!useContextDrivenReaction) return 1f;
        if (!TryReadContextNumber(reactionDriveKey, out float driveValue)) return 0f;

        // map drive value to 0-1.
        float drive = Mathf.InverseLerp(driveStartsAt, driveFullyActiveAt, driveValue);

        // optional suppression.
        if (!string.IsNullOrWhiteSpace(reactionSuppressKey) && TryReadContextNumber(reactionSuppressKey, out float suppressValue))
        {
            float suppression = Mathf.InverseLerp(suppressionStartsAt, suppressionFullyActiveAt, suppressValue);
            drive *= (1f - suppression);
        }

        return Mathf.Clamp01(drive);
    }

    private bool ReadContextBool(string key)
    {
        if (FG2GameServices.Instance == null) return false;

        PersistentContext context = FG2GameServices.Instance.Context;
        bool found = context.TryGet(key, out FuzzyValue value);

        return found && value.type == FuzzyValueType.Bool && value.boolVal;
    }

    private void CacheNormalColour()
    {
        if (targetRenderer == null || targetRenderer.sharedMaterial == null) return;

        Material material = targetRenderer.sharedMaterial;

        if (material.HasProperty(BaseColorID))
        {
            activeColorProperty = BaseColorID;
            normalColor = material.GetColor(BaseColorID);
        }
        else if (material.HasProperty(ColorID))
        {
            activeColorProperty = ColorID;
            normalColor = material.GetColor(ColorID);
        }
        else
        {
            Debug.LogWarning($"{material.name} has no supported colour property.", this);
        }
    }

    private void ApplyReactionColor(float strength)
    {
        if (targetRenderer == null || propertyBlock == null) return;

        Color finalColor = Color.Lerp(normalColor, reactionColor, Mathf.Clamp01(strength));

        targetRenderer.GetPropertyBlock(propertyBlock);
        propertyBlock.SetColor(activeColorProperty, finalColor);
        targetRenderer.SetPropertyBlock(propertyBlock);
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
            fullyFacingTarget = Mathf.Min(1f, facingFadeStart + 0.01f);
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