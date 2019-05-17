using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class WaypointMovement : MonoBehaviour
{
    public bool IsActive { get; set; }

    public float movementSpeed = 10;
    public float rotationSpeed = 200;

    public Transform lhRaycastPosition;
    public Transform rhRaycastPosition;
    public LineRenderer lhLine;
    public LineRenderer rhLine;
    public Animator lhAnimator;
    public Animator rhAnimator;

    bool moving;
    Transform raycastPosition;
    LineRenderer line;
    Animator handAnimator;
    XRNode device;

    public void Move(RaycastHit hit)
    {
        if (!IsActive) return;
        if (!moving)
        {
            moving = true;
            StartCoroutine(Move(hit.transform.position, hit.transform.rotation));
        }
    }

    IEnumerator Move(Vector3 position, Quaternion rotation)
    {
        yield return new WaitUntil(() => InPosition(position, rotation));

        transform.position = position;
        transform.rotation = rotation;
        moving = false;
    }

    bool InPosition(Vector3 position, Quaternion rotation)
    {
        transform.position = Vector3.Lerp(transform.position, position, movementSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        return Utility.CheckDistance(transform.position, position) < .1f;
    }

    public void SetHandedness(bool rightHanded)
    {
        if (rightHanded)
        {
            raycastPosition = rhRaycastPosition;
            device = UnityEngine.XR.XRNode.RightHand;
            handAnimator = rhAnimator;
            line = rhLine;
        }
        else
        {
            raycastPosition = lhRaycastPosition;
            device = UnityEngine.XR.XRNode.LeftHand;
            handAnimator = lhAnimator;
            line = lhLine;
        }
    }
}
