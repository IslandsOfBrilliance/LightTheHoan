using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEffect : MonoBehaviour
{
    [SerializeField] ColorPalette lightColor;

    public virtual IEnumerator ActivateEffect(List<MeshRenderer> lights)
    {
        SetLightColor(lights);
        yield break;
    }

    public virtual void SetLightColor(List<MeshRenderer> lights)
    {
        LightColor _lightColor = ColorManager.GetLightColor(lightColor);
        foreach (MeshRenderer rend in lights)
        {
            rend.material.color = _lightColor.lightColor;
            rend.material.SetColor("_EmissionColor", _lightColor.emissiveColor * _lightColor.brightness);
        }
    }
}