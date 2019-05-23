using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class InteractionController : MonoBehaviour
{
    public LayerMask interactionLayer;
    public Transform raycastPosition;
    public float pointRadius = .25f;
    public XRNode device;

    public bool IsActive { get; set; }

    LineRenderer line;
    Animator handAnimator;
    Interactable interactable;

    public List<string> interactionTags = new List<string>();

    private void OnEnable()
    {
        line = GetComponent<LineRenderer>();
        line.enabled = false;

        handAnimator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (!IsActive) return;
        RaycastHit hit;
        Ray ray = new Ray(raycastPosition.position, raycastPosition.forward);
        if(Physics.SphereCast(ray, pointRadius, out hit, Mathf.Infinity, interactionLayer))
        {
            
            if(interactionTags.Contains(hit.transform.tag))
            {
               
                Interactable interactionState = hit.transform.GetComponent<Interactable>();

                if (interactionState && !interactionState.IsInteractable)
                    return;
                
                line.SetPosition(0, ray.origin);
                line.SetPosition(1, hit.point);
                line.enabled = true;

                handAnimator.SetBool("IsPointing", true);

                if (device == XRNode.LeftHand ? PlayerInput.LeftGrab : PlayerInput.RightGrab)
                {
                    print(hit.transform.name);
                    if (interactable)
                        interactable.OnDeselected();

                    interactable = hit.transform.GetComponent<Interactable>();
                    print(interactable.name);
                    if (interactable)
                        interactable.OnSelected();

                    if (FeatureManager.Instance.moveType == FeatureManager.MoveType.Waypoint && hit.transform.tag.Equals("Waypoint"))
                        FeatureManager.Instance.waypointMovement.Move(hit);
                }
            }
            else
            {
                if (line.enabled)
                    line.enabled = false;

                handAnimator.SetBool("IsPointing", false);
            }
        }
        else
        {
            if (line.enabled)
                line.enabled = false;

            handAnimator.SetBool("IsPointing", false);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, pointRadius);
    }
}
