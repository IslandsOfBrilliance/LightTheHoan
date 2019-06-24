using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ButtonInteraction : Interaction{
    private Animator animator;
    private bool isPressed;
    private void Start(){
        animator = GetComponent<Animator>();
    }
    public override void Interact(){
        if (isPressed == false)
        {
            isPressed = true;
            animator.SetTrigger("ButtonTrig");
            StartCoroutine(Pressed());
        }
    }
    IEnumerator Pressed(){
        yield return new WaitForSeconds(1);
        isPressed = false;
    }
}
