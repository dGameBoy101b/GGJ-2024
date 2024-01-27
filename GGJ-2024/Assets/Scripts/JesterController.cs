using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine;

public class JesterController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float movementForce;
    [SerializeField] private float hoverForce;
    [SerializeField] private Transform hip;
    
    private Vector3 direction;
    private void FixedUpdate()
    {
        rb.AddForce(direction * movementForce, ForceMode.Force);
        rb.AddForce( 0, (1.5f - hip.position.y) * hoverForce, 0, ForceMode.Force);
    }


    private void OnMove(InputValue movementValue)
    {
        direction.x = movementValue.Get<Vector2>().x;
        direction.z = movementValue.Get<Vector2>().y;
    }
}
