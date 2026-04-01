using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class NetworkedMultiAnimatorSliderDriver : UdonSharpBehaviour
{
    [Header("References")]
    public Animator targetAnimator;
    public Slider[] uiSliders;

    [Header("Animator Float Parameters")]
    public string[] floatParameterNames;

    [Header("Synced Values")]
    [UdonSynced] private float[] syncedValues;

    private int[] floatParameterHashes;

    private void Start()
    {
        int sliderCount = uiSliders != null ? uiSliders.Length : 0;
        int parameterCount = floatParameterNames != null ? floatParameterNames.Length : 0;

        if (sliderCount == 0 || parameterCount == 0 || sliderCount != parameterCount)
        {
            Debug.LogError("[NetworkedMultiAnimatorSliderDriver] Sliders and parameter names must both exist and have the same length.");
            return;
        }

        floatParameterHashes = new int[floatParameterNames.Length];
        for (int i = 0; i < floatParameterNames.Length; i++)
        {
            floatParameterHashes[i] = Animator.StringToHash(floatParameterNames[i]);
        }

        if (syncedValues == null || syncedValues.Length != uiSliders.Length)
        {
            syncedValues = new float[uiSliders.Length];
        }

        for (int i = 0; i < uiSliders.Length; i++)
        {
            if (uiSliders[i] != null)
            {
                uiSliders[i].minValue = 0f;
                uiSliders[i].maxValue = 1f;
            }
        }

        ApplyAllValues(true);
    }

    public void HandleSliderChanged(int sliderIndex)
    {
        if (targetAnimator == null || uiSliders == null || syncedValues == null)
        {
            return;
        }

        if (sliderIndex < 0 || sliderIndex >= uiSliders.Length)
        {
            return;
        }

        VRCPlayerApi localPlayer = Networking.LocalPlayer;
        if (localPlayer == null)
        {
            return;
        }

        if (!Networking.IsOwner(gameObject))
        {
            Networking.SetOwner(localPlayer, gameObject);
        }

        Slider slider = uiSliders[sliderIndex];
        if (slider == null)
        {
            return;
        }

        syncedValues[sliderIndex] = Mathf.Clamp01(slider.value);

        ApplySingleValue(sliderIndex, syncedValues[sliderIndex], false);
        RequestSerialization();
    }

    public override void OnDeserialization()
    {
        ApplyAllValues(true);
    }

    private void ApplyAllValues(bool updateSliderVisuals)
    {
        if (syncedValues == null)
        {
            return;
        }

        for (int i = 0; i < syncedValues.Length; i++)
        {
            ApplySingleValue(i, syncedValues[i], updateSliderVisuals);
        }
    }

    private void ApplySingleValue(int index, float value, bool updateSliderVisual)
    {
        if (index < 0 || index >= floatParameterHashes.Length)
        {
            return;
        }

        float clampedValue = Mathf.Clamp01(value);

        if (targetAnimator != null)
        {
            targetAnimator.SetFloat(floatParameterHashes[index], clampedValue);
        }

        if (updateSliderVisual && uiSliders != null && index < uiSliders.Length && uiSliders[index] != null)
        {
            uiSliders[index].SetValueWithoutNotify(clampedValue);
        }
    }
}