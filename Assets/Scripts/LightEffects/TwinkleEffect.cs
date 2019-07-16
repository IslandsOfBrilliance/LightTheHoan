using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwinkleEffect : MonoBehaviour
{
    public int count;
    public float minBrightness;
    public float maxBrightness;
    public float frequency;

    private void Start()
    {
        StartCoroutine(LightBridge());
    }

    IEnumerator LightBridge()
    {
        for(; ; )
        {
            foreach(MeshRenderer light in LightManager.Instance.lights)
            {
                LightColor _lightColor = ColorManager.GetLightColor(LightManager.Instance.currentEffect.lightColor);

                light.material.color = _lightColor.lightColor;
                light.material.SetColor("_EmissionColor", _lightColor.emissiveColor * Random.Range(minBrightness, maxBrightness));
            }

            yield return new WaitForSeconds(frequency);
        }
    }
}
