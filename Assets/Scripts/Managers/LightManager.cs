using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public static LightManager Instance;
    //Reference all of the lights
    public List<MeshRenderer> lights;
    public LightEffect currentEffect;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if(currentEffect)
            ChangeLightEffect(currentEffect);
    }

    public void ChangeLightEffect(LightEffect effect)
    {
        currentEffect = effect;
        LightColor _lightColor = ColorManager.GetLightColor(currentEffect.lightColor);
        foreach (MeshRenderer rend in lights)
        {
            rend.material.color = _lightColor.lightColor;
            rend.material.SetColor("_EmissionColor", _lightColor.emissiveColor * _lightColor.brightness);
        }
    }
}