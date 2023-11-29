using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    public float speed;
    public float smoothRotation;
    public float moveStoppingDistance;
    public float rotationStopDistance;
    private Vector3 direction;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Rotate();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Rotate()
    {
        // Rotate towards the movement direction using Quaternion.LookRotation
        if (direction != Vector3.zero)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            // Check if the enemy is far enough from the target to continue rotating
            if (distanceToTarget > rotationStopDistance)
            {
                Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, Time.deltaTime * smoothRotation);
            }
        }
    }
    private void Move()
    {
        // Ensure there is a target to move towards
        if (target != null)
        {
            // Calculate the direction from the enemy to the target
            direction = (target.position - transform.position).normalized;

            // Check the distance to the target
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            // Set the velocity based on the calculated direction and speed
            rb.velocity = direction * speed * Time.fixedDeltaTime;

            // Stop updating the position when close enough to the target
            if (distanceToTarget < moveStoppingDistance)
            {
                rb.velocity = Vector3.zero;
            }
        }
    }

}
