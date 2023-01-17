using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayerNetwork : NetworkBehaviour
{
    [SerializeField] private float moveSpeed = 1.0f;
    private Vector2 moveDir;

    private void Update()
    {
        if (!IsOwner) return;

        transform.position += new Vector3(moveDir.x, moveDir.y, 0) * moveSpeed * Time.deltaTime;
    }

    private void OnMove(InputValue inputDirection)
    {
        moveDir = inputDirection.Get<Vector2>();
    }
}
