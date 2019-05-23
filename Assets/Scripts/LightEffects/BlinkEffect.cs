using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkEffect : LightEffect
{
    [SerializeField] float blinkRate = 0.25f;

    public override IEnumerator ActivateEffect(List<MeshRenderer> lights)
    {
        for (; ; )
        {
            SetLightColor(lights, lightColor);
            yield return new WaitForSeconds(blinkRate);
            SetLightColor(lights, ColorPalette.Black);
            yield return new WaitForSeconds(blinkRate);
        }
    }
}