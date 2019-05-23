using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public bool grabbable;
    public Transform grabPoint;
    public bool Selected { get; protected set; }
    public bool IsInteractable { get; protected set; }
    public bool resetsOnDeselected;

    Vector3 initialPosition;
    Transform parent;
    public UnityEvent interactionEvent;

    protected virtual void Start()
    {
        initialPosition = transform.localPosition;
        parent = transform.parent;
        ChangeInteraction(true);
    }

    public virtual void OnSelected()
    {
        print("Selected");
        Selected = true;
        interactionEvent?.Invoke();
    }

    public virtual void OnDeselected()
    {
        Selected = false;

        if (grabbable)
        {
            transform.parent = parent;
            if(resetsOnDeselected)
                transform.localPosition = initialPosition;
        }
    }

    public void ChangeInteraction(bool state)
    {
        IsInteractable = state;
    }
}
