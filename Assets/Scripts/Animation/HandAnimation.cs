using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandAnimation : MonoBehaviour
{
    public XRNode NodeType;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        OVRInput.Update();
        if (NodeType == XRNode.LeftHand ? OVRInput.Get(OVRInput.Button.PrimaryHandTrigger) : OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
        {
            animator.SetBool("IsPointing", false);
            animator.SetBool("IsGrabbing", true);
        }
        else
            animator.SetBool("IsGrabbing", false);

        if (NodeType == XRNode.LeftHand ? OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) : OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
        {
            animator.SetBool("IsGrabbing", false);
            animator.SetBool("IsPointing", true);
        } 
        else
            animator.SetBool("IsPointing", false);
    }
}
