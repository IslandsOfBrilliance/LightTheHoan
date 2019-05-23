using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEffect : MonoBehaviour
{
    [SerializeField] protected ColorPalette lightColor;
    protected Coroutine runningEffect;

    public virtual void StartEffect(List<MeshRenderer> lights)
    {
        runningEffect = StartCoroutine(ActivateEffect(lights));
    }

    public virtual void StopEffect()
    {
        if (runningEffect != null)
            StopCoroutine(runningEffect);
    }

    public virtual IEnumerator ActivateEffect(List<MeshRenderer> lights)
    {
        SetLightColor(lights, lightColor);
        yield break;
    }

    public virtual void SetLightColor(List<MeshRenderer> lights, ColorPalette color)
    {
        LightColor _lightColor = ColorManager.GetLightColor(color);
        foreach (MeshRenderer rend in lights)
        {
            rend.material.color = _lightColor.lightColor;
            rend.material.SetColor("_EmissionColor", _lightColor.emissiveColor * _lightColor.brightness);
        }
    }
}