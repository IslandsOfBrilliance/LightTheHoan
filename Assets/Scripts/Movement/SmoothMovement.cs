using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothMovement : MonoBehaviour
{
    [HideInInspector]
    public float speed = 5;

    public bool IsActive { get; set; }
    Camera mainCamera;

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
        move = mainCamera.transform.TransformDirection(move);
        move.y = 0;
        transform.position += move.normalized * speed * Time.deltaTime;
    }
}
