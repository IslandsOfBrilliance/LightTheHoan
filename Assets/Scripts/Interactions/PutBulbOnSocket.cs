using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutBulbOnSocket : MonoBehaviour
{
    [SerializeField] Transform bulbPosition;
    [SerializeField] Grab Left;
    [SerializeField] Grab Right;
    LightEffect off;

    private void Start()
    {
        off = GetComponent<LightEffect>();
    }

    void OnTriggerEnter(Collider bulb)
    {
        if (bulb.tag == "Grabbable")
        {
            Left.holding = false;
            Right.holding = false;
            bulb.transform.position = bulbPosition.position;
            bulb.transform.parent = bulbPosition;
            bulb.transform.rotation = Quaternion.identity; // resets rotation
            Rigidbody body = bulb.GetComponent<Rigidbody>();
            body.useGravity = false;
            body.isKinematic = true;

            LightManager.Instance.ChangeLightEffect(bulb.GetComponent<LightEffect>());
        }
    }

    void OnTriggerExit(Collider bulb)
    {
        if (bulb.tag == "Grabbable")
        {
            LightManager.Instance.ChangeLightEffect(off);
        }
    }
}
