using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    //Reference all of the lights
    public List<MeshRenderer> lights;
    public LightEffect currentEffect;

    void Start()
    {
        ChangeLightEffect(currentEffect);
    }

    public void ChangeLightEffect(LightEffect effect)
    {
        currentEffect = effect;
        StartCoroutine(currentEffect.ActivateEffect(lights));
    }
}