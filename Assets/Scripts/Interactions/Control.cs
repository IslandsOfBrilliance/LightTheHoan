using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    public OVRInput.Controller controller;
    [SerializeField] float collisionSize = .1f;
    [SerializeField] LayerMask interactable;
    public Collider[] touching;

    void Update()
    {
        transform.localPosition = OVRInput.GetLocalControllerPosition(controller);
        transform.localRotation = OVRInput.GetLocalControllerRotation(controller);
        touching = Physics.OverlapSphere(transform.position, collisionSize, interactable);
    }
}