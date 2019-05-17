using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIInteractable : Interactable
{
    public float speed = 5f;
    public GameObject environment;
    public GameObject[] pointsOfInterest;

    Animator animator;
    Collider interactionCollider;

    bool insidePOI;
    float lerpSpeed;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        interactionCollider = GetComponent<Collider>();

    }

    public override void OnSelected()
    {
        base.OnSelected();
        interactionCollider.enabled = false;
    }

    public override void OnDeselected()
    {
        base.OnSelected();
        interactionCollider.enabled = true;
    }
}
