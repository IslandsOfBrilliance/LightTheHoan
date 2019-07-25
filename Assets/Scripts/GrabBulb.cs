using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabBulb : MonoBehaviour
{
    public Transform bulbParent;

    public void Grab()
    {
        if (PutBulbOnSocket.Instance.oldBulb)
        {
            print("Grabbing");
            PutBulbOnSocket.Instance.oldBulb.transform.parent = (bulbParent.transform);
        }
    }

    public void DestroyBulb()
    {
        PutBulbOnSocket.Instance.ResetLights();
        Destroy(PutBulbOnSocket.Instance.oldBulb);
    }
}
