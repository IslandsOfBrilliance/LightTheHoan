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
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (NodeType == XRNode.LeftHand ? PlayerInput.LeftGrab : PlayerInput.RightGrab)
            animator.SetBool("IsGrabbing", true);
        else
            animator.SetBool("IsGrabbing", false);
    }
}
