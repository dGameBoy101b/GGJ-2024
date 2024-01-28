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
    [SerializeField] private float turnTorque;
    [SerializeField] private Transform hip;
    [SerializeField] private float hipHeight;

    [SerializeField] private Animator animator;
    [SerializeField] private GameObject HeadbutCollider;
    
    private Vector3 direction;
    private void FixedUpdate()
    {
        rb.AddForce(direction * movementForce, ForceMode.Force);
        rb.AddForce( 0, (hipHeight - hip.position.y) * hoverForce, 0, ForceMode.Force);
        
        Vector2 forward = new Vector2(0,0);
        forward.x = rb.transform.forward.x;
        forward.y = rb.transform.forward.z;

        Vector2 target = forward;
        target.x = direction.x;
        target.y = direction.z;
        
        float torque = Vector2.SignedAngle(target, forward);
        
        rb.AddTorque(0,torque * turnTorque,0, ForceMode.Force);
    }


    private void OnMove(InputValue movementValue)
    {
        direction.x = movementValue.Get<Vector2>().x;
        direction.z = movementValue.Get<Vector2>().y;
    }

    private void OnJump(InputValue movementValue)
    {
        if (movementValue.isPressed)
        {
            animator.SetTrigger("Headbut");
            StartCoroutine(Headbut());
        }
    }

    IEnumerator Headbut()
    {
        yield return new WaitForSeconds(0.25f);
        HeadbutCollider.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        HeadbutCollider.SetActive(false);
    }
}
