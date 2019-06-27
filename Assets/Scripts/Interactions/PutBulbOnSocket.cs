﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutBulbOnSocket : MonoBehaviour
{
    public static PutBulbOnSocket Instance;

    [SerializeField] Transform bulbPosition;
    [SerializeField] Grab Left;
    [SerializeField] Grab Right;
    LightEffect off;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        off = GetComponent<LightEffect>();
    }

    void OnTriggerEnter(Collider bulb)
    {
        LightEffect lightEffect = bulb.GetComponent<LightEffect>();
        if (bulb.tag == "Grabbable" && lightEffect)
        {
            Left.holding = false;
            Right.holding = false;
            bulb.transform.position = bulbPosition.position;
            bulb.transform.parent = bulbPosition;
            bulb.transform.rotation = Quaternion.identity; // resets rotation
            Rigidbody body = bulb.GetComponent<Rigidbody>();
            body.useGravity = false;
            body.isKinematic = true;

            LightManager.Instance.ChangeLightEffect(lightEffect);
        }
    }

    void OnTriggerExit(Collider bulb)
    {
        if (bulb.tag == "Grabbable")
        {
            ResetLights();
        }
    }

    public void ResetLights()
    {
        LightManager.Instance.ChangeLightEffect(off);
    }
}
