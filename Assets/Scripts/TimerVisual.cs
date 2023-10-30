using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TimerVisual : MonoBehaviour
{
    [SerializeField] private Volume volume;

    [SerializeField] private Color beginningColor;
    [SerializeField] private Color endColor;

    

    private ClampedFloatParameter vignetteIntensityParameter = new ClampedFloatParameter(0, 0, 1);

    [SerializeField] private AnimationCurve vignetteIntensityCurve;


    public void UpdateVolumeValues(float normalizedTimeLeft)
    {
        if (volume.profile.TryGet(out ColorAdjustments colorAdjustments))
        {
            Color actualColor = Color.Lerp(beginningColor, endColor, normalizedTimeLeft);
            colorAdjustments.colorFilter.value = actualColor;
        }

        if (volume.profile.TryGet(out Vignette vignette))
        {
            float valueWithCurve = vignetteIntensityCurve.Evaluate(normalizedTimeLeft);
            vignetteIntensityParameter.value = valueWithCurve;
            vignette.intensity.SetValue(vignetteIntensityParameter);
        }
    }
}
