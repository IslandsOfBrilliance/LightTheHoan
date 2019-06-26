using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEffect : MonoBehaviour
{
    public ColorPalette lightColor;
    protected Coroutine runningEffect;

    public bool displayEffectOnInteractable;
    [ConditionalHide("displayEffectOnInteractable", true)]
    public List<MeshRenderer> interactableMeshRenderers;

    private void Start()
    {
        if(displayEffectOnInteractable)
            StartCoroutine(ActivateEffect(interactableMeshRenderers));
    }

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
        LightColor _lightColor = ColorManager.GetLightColor(lightColor);
        foreach (MeshRenderer rend in lights)
        {
            rend.material.color = _lightColor.lightColor;
            rend.material.SetColor("_EmissionColor", _lightColor.emissiveColor * _lightColor.brightness);
        }
        yield break;
    }


}