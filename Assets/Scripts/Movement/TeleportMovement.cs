using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class TeleportMovement : MonoBehaviour
{
    public bool IsActive { get; set; }
    public bool WithRotation { get; set; }

    public float movementSpeed;
    public float moveDistance;

    public float rotationSpeed = 200f;

    public GameObject teleportPointer;
    public LayerMask groundLayer;
    public Transform lhRaycastPosition;
    public Transform rhRaycastPosition;
    public LineRenderer lhLine;
    public LineRenderer rhLine;
    public Animator lhAnimator;
    public Animator rhAnimator;

    Vector3 location;
    Quaternion rotation;

    Transform raycastPosition;
    Animator handAnimator;
    LineRenderer line;
    XRNode device;

    bool selectingLocation;
    bool atDestination;

    private void Start()
    {
        atDestination = true;
    }

    private void Update()
    {
        if (!IsActive) return;

        if(PlayerInput.Move != Vector2.zero && atDestination)
        {
            selectingLocation = true;
            SelectLocation();
        }
        else
        {
            if(selectingLocation)
            {
                atDestination = false;
                selectingLocation = false;
                StartCoroutine(Teleport());
            }
        }
    }

    void SelectLocation()
    {
        RaycastHit hit;
        Ray ray = new Ray(raycastPosition.position, raycastPosition.forward);

        line.SetPosition(0, ray.origin);

        if (Physics.Raycast(ray, out hit, moveDistance, groundLayer))
        {
            line.SetPosition(1, hit.point);
            handAnimator.SetBool("IsMoving", true);

            location = hit.point;

            if (WithRotation)
                rotation = Quaternion.LookRotation(new Vector3(PlayerInput.Move.x, 0, PlayerInput.Move.y));

            teleportPointer.SetActive(true);
            teleportPointer.transform.position = hit.point;
            if (WithRotation)
                teleportPointer.transform.rotation = rotation;
        }
        else
        {
            teleportPointer.SetActive(false);
            line.SetPosition(1, ray.GetPoint(moveDistance));
        }

        line.enabled = true;
    }

    IEnumerator Teleport()
    {
        if (line.enabled)
            line.enabled = false;

        teleportPointer.SetActive(false);

        handAnimator.SetBool("IsMoving", false);
        yield return new WaitUntil(AtDestination);
        atDestination = true;
    }

    bool AtDestination()
    {
        transform.position = Vector3.Lerp(transform.position, location, movementSpeed * Time.deltaTime);

        if (WithRotation)
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        return Utility.CheckDistance(transform.position, location) <= .2f;
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
