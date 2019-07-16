using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftingLights : MonoBehaviour
{
    public float lightSpeed;

    private void Start()
    {
        StartCoroutine(LightBridge());
    }

    IEnumerator LightBridge()
    {
        for(int i = 0; i < LightManager.Instance.lights.Count; i++)
        {
            LightColor _lightColor = ColorManager.GetLightColor(LightManager.Instance.currentEffect.lightColor);
            LightManager.Instance.lights[i].material.color = _lightColor.lightColor;
            LightManager.Instance.lights[i].material.SetColor("_EmissionColor", _lightColor.emissiveColor * _lightColor.brightness);
            
            yield return new WaitForSeconds(lightSpeed);
        }
    }
}
