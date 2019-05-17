using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class FreeGrab : MonoBehaviour
{
    public Vector3 offset;
    public float grabRadius;
    public XRNode device;

    [Space]
    public float lerpSpeed;
    public Transform raycastPosition;
    public Transform handPoint;
    public LayerMask interactionLayer;
    public LineRenderer line;

    public bool IsActive { get; set; }
    public bool DistanceGrab { get; set; }
    public bool PointGrab { get; set; }

    GameObject itemInHand;

    bool grabbing;

    private void Update()
    {
        transform.localPosition = InputTracking.GetLocalPosition(device);
        transform.localRotation = InputTracking.GetLocalRotation(device);

        if (!IsActive) return;
      
            if (DistanceGrab)
                DistGrab();
            else
                Grab();

        if (device == XRNode.LeftHand ? !PlayerInput.LeftGrab : !PlayerInput.RightGrab)
        {
            if(itemInHand)
            {
                itemInHand.transform.parent = null;
                Interactable interactable = itemInHand.GetComponent<Interactable>();

                if (interactable)
                    interactable.OnDeselected();

                itemInHand = null;
            }
        }
    }

    void Grab()
    {
        if (device == XRNode.LeftHand ? PlayerInput.LeftGrab : PlayerInput.RightGrab)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position + offset, grabRadius);

            foreach (Collider col in colliders)
            {
                if (col.tag.Equals("Grab"))
                {
                    itemInHand = col.gameObject;
                    itemInHand.transform.parent = transform;

                    Interactable interactable = itemInHand.GetComponent<Interactable>();

                    if (interactable)
                    {
                        if (PointGrab)
                        {
                            print("Point Grabbing");
                            itemInHand.transform.localRotation = interactable.grabPoint.localRotation;
                            itemInHand.transform.position = handPoint.position - (interactable.grabPoint.position - itemInHand.transform.position);
                        }

                        interactable.OnSelected();
                    }
                    break;
                }
            }
        }
    }

    void DistGrab()
    {
        if (itemInHand) return;
        RaycastHit hit;
        Ray ray = new Ray(raycastPosition.position, raycastPosition.forward);

        line.enabled = true;
        line.SetPosition(0, raycastPosition.position);

        if (Physics.SphereCast(ray, grabRadius, out hit, Mathf.Infinity, interactionLayer))
        {
            if (hit.transform.tag.Equals("Grab"))
            {
                line.SetPosition(1, hit.point);
                if (device == XRNode.LeftHand ? PlayerInput.LeftGrab : PlayerInput.RightGrab)
                {
                    if (!grabbing)
                    {
                        grabbing = true;
                        StartCoroutine(MoveTo(hit));
                    }
                }
            }
        }
        else
            line.SetPosition(1, ray.GetPoint(20));
    }

    IEnumerator MoveTo(RaycastHit hit)
    {
        yield return new WaitUntil(() => CheckPosition(hit));

        itemInHand = hit.transform.gameObject;
        itemInHand.transform.parent = handPoint;
        hit.transform.localPosition = Vector3.zero;

        Interactable interactable = itemInHand.GetComponent<Interactable>();

        if (interactable)
        {
            itemInHand.transform.localRotation = interactable.grabPoint.localRotation;
            itemInHand.transform.position = handPoint.position - (interactable.grabPoint.position - itemInHand.transform.position);
            interactable.OnSelected();
        }

        grabbing = false;
    }

    bool CheckPosition(RaycastHit hit)
    {
        hit.transform.position = Vector3.Lerp(hit.transform.position, handPoint.position, lerpSpeed * Time.deltaTime);
        return Utility.CheckDistance(handPoint.position, hit.transform.position) < .2f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + offset, grabRadius);
    }
}
