using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    PlayerMovementInput playerMovementInput;
    Vector3 direction;
    [SerializeField] private float speed;
    [SerializeField] private float smoothRotation;

    public event Action<float> OnSpeedChangeAction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        playerMovementInput = GetComponent<PlayerMovementInput>();
        playerMovementInput.OnMovementAction += PlayerMovementInput_OnMovementAction;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void PlayerMovementInput_OnMovementAction(Vector2 movementDirection)
    {
        direction = new Vector3(movementDirection.x, 0f, movementDirection.y);
    }

    private void Move()
    {
        if (direction != Vector3.zero)
        {
            // Move
            rb.velocity = direction * speed * Time.fixedDeltaTime;
            //Set direction
            Rotate();
        }
        else
        {
            // Reset speed if not moving
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, Time.fixedDeltaTime);
        }
        OnSpeedChangeAction?.Invoke(rb.velocity.magnitude);
    }

    private void Rotate()
    {
        // Change player's direction to 'direction' vector
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        // Perform the rotation smoothly
        rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, smoothRotation * Time.fixedDeltaTime));
    }
}