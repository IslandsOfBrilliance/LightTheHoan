using System.Collections;
using UnityEngine;

public class DashMovement : MonoBehaviour
{
    public float speed = 2f;
    public float stoppingDistance = .1f;

    public float distance = 2f;
    public LayerMask environmentLayer;

    public bool IsActive { get; set; }

    Camera mainCamera;

    bool dashing;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (!IsActive) return;
        Move();
    }

    void Move()
    {
        Vector3 move = new Vector3(PlayerInput.Move.x, 0, PlayerInput.Move.y);

        if (move != Vector3.zero && !dashing)
        {
            dashing = true;
            move = mainCamera.transform.TransformDirection(move);
            move.y = 0;

            Vector3 destination = Utility.GetPoint(transform.position, move, distance, environmentLayer);
            StartCoroutine(CheckPosition(destination));
        }
    }

    IEnumerator CheckPosition(Vector3 destination)
    {
        yield return new WaitUntil(() => InPosition(destination));
        dashing = false;
    }

    bool InPosition(Vector3 destination)
    {
        transform.position = Vector3.Lerp(transform.position, destination, speed * Time.deltaTime);
        return Utility.CheckDistance(transform.position, destination) <= stoppingDistance;
    }
}
