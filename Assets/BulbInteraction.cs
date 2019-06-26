using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulbInteraction : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ground")
        {
            Destroy(gameObject, 0.2f);
        }
    }

}
