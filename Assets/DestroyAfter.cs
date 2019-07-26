using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 4);
    }
}
