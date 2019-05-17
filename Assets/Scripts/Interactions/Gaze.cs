using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gaze : MonoBehaviour
{
    public Image circle;
    public LayerMask interactionLayer;

    public float gazeDuration;
    public float gazeRadius;

    public bool IsActive { get; set; }

    Camera mainCamera;
    float timer;

    Coroutine gazing;
    Interactable currentInteractable;

    [HideInInspector]
    public List<string> interactionTags = new List<string>();

    private void OnEnable()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (!IsActive) return;
        RaycastHit hit;
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);

        if (Physics.SphereCast(ray, gazeRadius, out hit, Mathf.Infinity, interactionLayer))
        {
            if (interactionTags.Contains(hit.transform.tag))
            {
                Interactable interactionState = hit.transform.GetComponent<Interactable>();

                if (interactionState && !interactionState.IsInteractable)
                {
                    StopGaze();
                    return;
                }

                if (gazing == null)
                    gazing = StartCoroutine(Gazing(hit));
            }
            else
                StopGaze();
        }
        else
            StopGaze();
    }

    IEnumerator Gazing(RaycastHit hit)
    {
        timer = Time.time;
        circle.enabled = true;
        yield return new WaitUntil(CheckGazeDuration);

        if (currentInteractable)
            currentInteractable.OnDeselected();

        currentInteractable = hit.transform.GetComponent<Interactable>();

        if (currentInteractable)
            currentInteractable.OnSelected();

        FeatureManager f = GameObject.FindObjectOfType<FeatureManager>();
        if (f.moveType == FeatureManager.MoveType.Waypoint && hit.transform.tag.Equals("Waypoint"))
            f.waypointMovement.Move(hit);
    }
    
    bool CheckGazeDuration()
    {
        float gazeTime = Mathf.InverseLerp(0, 1, (Time.time - timer));
        circle.fillAmount = gazeTime;

        return (Time.time - timer) > gazeDuration;
    }

    void StopGaze()
    {
        if (gazing != null)
            StopCoroutine(gazing);

        gazing = null;
        circle.fillAmount = 0;
        circle.enabled = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, gazeRadius);
    }
}
