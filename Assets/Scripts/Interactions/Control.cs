using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Control : MonoBehaviour{
    public float sphereSize = 0.1f; //default
    public LayerMask interactableLayer;
    public Collider[] interactables;
    public OVRInput.Controller controller;
    void Start(){
        //Void Setup Here!!!
    }
    void Update(){
        transform.localPosition = OVRInput.GetLocalControllerPosition(controller);
        transform.localRotation = OVRInput.GetLocalControllerRotation(controller);
        interactables = Physics.OverlapSphere(transform.position, sphereSize, interactableLayer);
    }
}