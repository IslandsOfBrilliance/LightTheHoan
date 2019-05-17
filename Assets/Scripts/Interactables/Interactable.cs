using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool grabbable;
    public Transform grabPoint;
    public bool Selected { get; protected set; }
    public bool IsInteractable { get; protected set; }

    Vector3 initialPosition;
    Transform parent;

    protected virtual void Start()
    {
        initialPosition = transform.localPosition;
        parent = transform.parent;
        ChangeInteraction(true);
    }

    public virtual void OnSelected()
    {
        Selected = true;
    }

    public virtual void OnDeselected()
    {
        Selected = false;

        if (grabbable)
        {
            transform.parent = parent;
            transform.localPosition = initialPosition;
        }
    }

    public void ChangeInteraction(bool state)
    {
        IsInteractable = state;
    }
}
