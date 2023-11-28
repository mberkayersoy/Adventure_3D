using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTest : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.2f;

    private Rigidbody enemyRigidbody;
    private bool isGrounded;

    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //MoveToTarget();
        //CheckGrounded();
        //JumpIfNeeded();
        //CheckFront();
    }

    private void FixedUpdate()
    {
        MoveToTarget();
    }

    void MoveToTarget()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            //enemyRigidbody.velocity = new Vector3(direction.x * moveSpeed, enemyRigidbody.velocity.y, direction.z * moveSpeed);
            //enemyRigidbody.MovePosition(target.position * moveSpeed * Time.fixedDeltaTime);
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }

    void CheckGrounded()
    {
        // Use raycasting to check if the enemy is grounded
        isGrounded = Physics.CheckSphere(transform.position - Vector3.up, groundCheckDistance, groundLayer);
    }

    void JumpIfNeeded()
    {
        // Check jump conditions here (e.g., if the player is nearby or if a certain event occurred)
        // For simplicity, let's say the enemy jumps when the space key is pressed
        if (isGrounded)
        {
            enemyRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    void CheckFront()
    {
        if (!Physics.Raycast(transform.forward / 2 + (transform.position - Vector3.up), Vector3.down, groundCheckDistance, groundLayer))
        {
            JumpIfNeeded();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position - Vector3.up, groundCheckDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.forward / 2 + (transform.position - Vector3.up), Vector3.down);
    }
}
