using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothRotation : MonoBehaviour
{
    [HideInInspector]
    public float rotationSpeed = 100;
    public bool IsActive { get; set; }

    private void Update()
    {
        if (!IsActive) return;
        Look();
    }

    void Look()
    {
        transform.Rotate(Vector3.up * PlayerInput.Look.x * rotationSpeed * Time.deltaTime);
    }
}
