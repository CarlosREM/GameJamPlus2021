using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraEffects : MonoBehaviour
{
    Volume volume;
    VolumeProfile volumeProfile;


    void Awake()
    {
        volume = GetComponent<Volume>();
    }

    void Start()
    {
        volumeProfile = volume.profile;
    }


    public void SetDepth(float value, float transitionDuration)
    {
        float blur = Mathf.Clamp(value, 50, 300);

        DepthOfField effectDoF;
        if (volumeProfile.TryGet(out effectDoF))
        {
            StartCoroutine(DepthCoroutine(effectDoF, value, transitionDuration));
        }
    }

    IEnumerator DepthCoroutine(DepthOfField effectDoF, float newValue, float duration)
    {
        float oldValue = (float) effectDoF.focalLength;
        float currentValue;
        float alpha = 0, t;

        while (alpha < duration)
        {
            t = alpha / duration;
            t = t * t * (3f - 2f * t);

            currentValue = Mathf.Lerp(oldValue, newValue, t);
            effectDoF.focalLength.Override(currentValue);
            alpha += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
        effectDoF.focalLength.Override(newValue);

        yield return null;
    }
}
