using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapRotation : MonoBehaviour
{
    public float rotationDegree = 70f;
    public float rotationFrequency = .5f;

    public bool IsActive { get; set; }

    bool turning;

    private void Update()
    {
        if (!IsActive) return;
        Look();
    }

    void Look()
    {
        if (PlayerInput.LookRaw.x != 0 && !turning)
        {
            turning = true;

            int value = 0;

            if (PlayerInput.LookRaw.x > 0)
                value = 1;
            else
                value = -1;

            transform.rotation *= Quaternion.Euler(0, value * rotationDegree, 0);
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(rotationFrequency);
        turning = false;
    }
}
