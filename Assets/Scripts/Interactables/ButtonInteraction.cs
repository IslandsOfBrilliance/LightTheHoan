using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ButtonInteraction : Interaction
{
    public SpawnBulb bulby;
    public Animator animator;
    private bool isPressed;
    public ColorPalette buttonColor;
    public MeshRenderer meshrenderer;

    AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        ActivateEffect();
    }
    public override void Interact()
    {
        if (isPressed == false)
        {
            source.Play();
            isPressed = true;
            animator.SetTrigger("ButtonTrig");
            bulby.BulbSpawn(buttonColor);
            StartCoroutine(Pressed());
        }
    }
    IEnumerator Pressed()
    {
        yield return new WaitForSeconds(1);
        isPressed = false;
    }
    public void ActivateEffect()
    {
        LightColor _lightColor = ColorManager.GetLightColor(buttonColor);
        meshrenderer.material.color = _lightColor.lightColor;
        meshrenderer.material.SetColor("_EmissionColor", _lightColor.emissiveColor * _lightColor.brightness);
    }
}