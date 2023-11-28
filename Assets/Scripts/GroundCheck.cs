using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundRadius;
    private bool isGrounded;

    public event Action<bool> OnIsGroundedChangeAction;

    private void FixedUpdate()
    {
        CheckGround();
    }
    private void CheckGround()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundRadius, groundLayer);
        OnIsGroundedChangeAction?.Invoke(isGrounded);
    }

    private void OnDrawGizmos()
    {
        if (isGrounded)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color= Color.red;
        }
        Gizmos.DrawSphere(transform.position, groundRadius);
    }

}
