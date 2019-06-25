using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    Control control;
    bool holding = false;
    Collider held;
    bool trigger;
    Rigidbody body;
    [SerializeField] float throwStrength;
    [SerializeField] float rotationStrength = .75f;
    Quaternion lastRotation, currentRotation;

    private void Start()
    {
        control = GetComponent<Control>();
    }

    void Update()
    {
        if (holding)
        {
            lastRotation = currentRotation;
            currentRotation = held.transform.rotation;
        }
        if (control.touching.Length > 0 && control.touching[0].tag == "Grabbable")
        {
            OVRInput.Update();
            bool trigger = control.controller == OVRInput.Controller.LTouch
                    ? OVRInput.Get(OVRInput.Button.PrimaryHandTrigger)
                    : OVRInput.Get(OVRInput.Button.SecondaryHandTrigger);
            if (!holding && trigger) // pick up
            {
                holding = true;
                held = control.touching[0];
                held.transform.position = transform.position;
                held.transform.parent = transform;
                body = held.GetComponent<Rigidbody>();
                body.useGravity = false;
                body.isKinematic = true;
            } else if (holding && !trigger) // drop
            {
                holding = false;
                held.transform.parent = null;
                body.useGravity = true;
                body.isKinematic = false;
                body.velocity = OVRInput.GetLocalControllerVelocity(control.controller) * throwStrength;
                body.angularVelocity = GetAngularVelocity();
                body = null;
            }
        }
    }

    Vector3 GetAngularVelocity()
    {
        Quaternion deltaRotation = currentRotation * Quaternion.Inverse(lastRotation);
        return new Vector3(Mathf.DeltaAngle(0, deltaRotation.eulerAngles.x), Mathf.DeltaAngle(0, deltaRotation.eulerAngles.y), Mathf.DeltaAngle(0, deltaRotation.eulerAngles.z)) * rotationStrength;
    }
}