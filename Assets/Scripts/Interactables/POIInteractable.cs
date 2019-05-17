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

    private void Update()
    {
        if(insidePOI) return;
        if(Selected)
        {
            lerpSpeed = Mathf.Lerp(lerpSpeed, 1, speed * Time.deltaTime);
            animator.SetFloat("Distance", lerpSpeed);

            if(lerpSpeed >= .5f)
            {
                environment.SetActive(false);

                foreach (GameObject go in pointsOfInterest)
                    if (go != gameObject)
                        go.SetActive(false);
            }

            if (lerpSpeed >= .95f)
            {
                animator.SetFloat("Distance", 1);

                FeatureManager.Instance.SetInteractionState(true);
                FeatureManager.Instance.SetMoveActive(true);
                FeatureManager.Instance.SetRotationActive(true);
                insidePOI = true;
            }
        }
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
