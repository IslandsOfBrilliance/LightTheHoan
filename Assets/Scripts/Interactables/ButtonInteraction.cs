using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ButtonInteraction : Interaction
{
    public SpawnBulb bulby;
    public Animator animator;
    private bool isPressed;
    public override void Interact()
    {
        if (isPressed == false)
        {
            isPressed = true;
            animator.SetTrigger("ButtonTrig");
            bulby.BulbSpawn();
            StartCoroutine(Pressed());
        }
    }
    IEnumerator Pressed()
    {
        yield return new WaitForSeconds(1);
        isPressed = false;
    }
}