using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR; //needs to be UnityEngine.VR in version before 2017.2

public class HandGrabbing : MonoBehaviour
{
    public XRNode NodeType;
    public float GrabDistance = 0.1f;
    public string GrabTag = "Grab";
    public float ThrowMultiplier = 1.5f;
    public AudioClip grabSound;

    public Transform _currentObject;
    private Vector3 _lastFramePosition;

    Animator animator;

    void Start()
    {
        _currentObject = null;
        _lastFramePosition = transform.position;
    }

    void Update()
    {
        //update hand position and rotation
        transform.localPosition = InputTracking.GetLocalPosition(NodeType);
        transform.localRotation = InputTracking.GetLocalRotation(NodeType);

        //if we don't have an active object in hand, look if there is one in proximity
        if (_currentObject == null)
        {
            //check for colliders in proximity
            Collider[] colliders = Physics.OverlapSphere(transform.position, GrabDistance);
            if (colliders.Length > 0)
            {
                //if there are colliders, take the first one if we press the grab button and it has the tag for grabbing
                if (NodeType == XRNode.LeftHand ? PlayerInput.LeftGrab : PlayerInput.RightGrab)
                {
                    if(colliders[0].transform.CompareTag(GrabTag))
                    {
                        OVRHapticsClip clip = new OVRHapticsClip(grabSound);

                        if (NodeType == XRNode.LeftHand)
                            OVRHaptics.LeftChannel.Queue(clip);
                        else
                            OVRHaptics.RightChannel.Queue(clip);

                        //set current object to the object we have picked up
                        _currentObject = colliders[0].transform;

                        //parent it to hand
                        colliders[0].transform.SetParent(transform);

                        //if there is no rigidbody to the grabbed object attached, add one
                        if (_currentObject.GetComponent<Rigidbody>() == null)
                        {
                            _currentObject.gameObject.AddComponent<Rigidbody>();
                        }

                        //set grab object to kinematic (disable physics)
                        _currentObject.GetComponent<Rigidbody>().isKinematic = true;
                    }
                }
            }

        }
        else
        //we have object in hand
        {
            //if we we release grab button, release current object
            if (NodeType == XRNode.LeftHand ? !PlayerInput.LeftGrab : !PlayerInput.RightGrab)
            {
                if(_currentObject)
                {
                    //set grab object to non-kinematic (enable physics)
                    Rigidbody _objectRGB = _currentObject.GetComponent<Rigidbody>();
                    _objectRGB.isKinematic = false;

                    //do continuous collision detection so dropped object doesn't fall through ground
                    _objectRGB.collisionDetectionMode = CollisionDetectionMode.Continuous;

                    //calculate the hand's current velocity
                    Vector3 CurrentVelocity = (transform.position - _lastFramePosition) / Time.deltaTime;

                    //set the grabbed object's velocity to the current velocity of the hand
                    _objectRGB.velocity = CurrentVelocity * ThrowMultiplier;

                    //unparent the object
                    _currentObject.SetParent(null);

                    //release the reference
                    _currentObject = null;
                }
            }
        }
        //save the current position for calculation of velocity in next frame
        _lastFramePosition = transform.position;
    }
}