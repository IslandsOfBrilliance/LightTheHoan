using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutBulbOnSocket : MonoBehaviour
{
    [SerializeField] Transform bulbPosition;
    [SerializeField] Grab Left;
    [SerializeField] Grab Right;
    private void OnTriggerEnter(Collider bulb)
    {
        if (bulb.tag == "Grabbable")
        {
            Left.holding = false;
            Right.holding = false;
            bulb.transform.position = bulbPosition.position;
            bulb.transform.parent = bulbPosition;
            bulb.transform.rotation = Quaternion.identity;
            Rigidbody body = bulb.GetComponent<Rigidbody>();
            body.useGravity = false;
            body.isKinematic = true;
        }
    }
}
