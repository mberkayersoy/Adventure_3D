using StarterAssets;
using System;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerConfigSO playerMovementSO;
    [SerializeField] private bool _isGrounded;

    [Header("Listen to Event Channels")]

    private float _speed;
    private float _targetRotation = 0.0f;
    private float _rotationVelocity;
    private float _animationBlend;
    private float _jumpTimeoutDelta;

    // Events for AnimationManager
    public event Action<float> OnSpeedChangeAction;
    public event Action<float> OnAnimationBlendChangeAction;
    public event Action<bool> OnGroundStateChangeAction;
    public event Action<bool> OnJumpAction;
    public event Action<bool> OnFreeFallAction;

    private Rigidbody _rb;
    private GroundCheck _groundCheck;
    private StarterAssetsInputs _input;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _groundCheck = GetComponent<GroundCheck>();
    }

    private void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
        _groundCheck.OnIsGroundedChangeAction += GroundCheck_OnIsGroundedChangeAction;

        // reset our timeouts on start
        _jumpTimeoutDelta = playerMovementSO.jumpTimeout;
    }

    private void GroundCheck_OnIsGroundedChangeAction(bool isGrounded)
    {
        _isGrounded = isGrounded;
        OnGroundStateChangeAction?.Invoke(isGrounded);
    }

    private void Update()
    {
        Move();
        Jump();

        if (transform.position.y <= -4)
        {
            transform.position = Vector3.zero;
        }
    }

    private void Move()
    {
        float targetSpeed = playerMovementSO.maxSpeed;

        if (_input.move == Vector2.zero) targetSpeed = 0.0f;

        // a reference to the players current horizontal velocity
        float currentHorizontalSpeed = new Vector3(_rb.velocity.x, 0.0f, _rb.velocity.z).magnitude;

        float speedOffset = 0.1f;
        float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

        // accelerate or decelerate to target speed
        if (currentHorizontalSpeed < targetSpeed - speedOffset ||
            currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            // creates curved result rather than a linear one giving a more organic speed change
            // note T in Lerp is clamped, so we don't need to clamp our speed
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                Time.deltaTime * playerMovementSO.speedChangeRate);

            // round speed to 3 decimal places
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }
        _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * playerMovementSO.speedChangeRate);
        if (_animationBlend < 0.01f) _animationBlend = 0f;

        // normalise input direction
        Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

        // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is a move input rotate player when the player is moving
        if (_input.move != Vector2.zero)
        {
            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                              Camera.main.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                playerMovementSO.smoothRotation);

            // rotate to face input direction relative to camera position
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

        Vector3 moveVector = inputDirection * _speed;
        _rb.velocity = new Vector3 (moveVector.x, _rb.velocity.y, moveVector.z);

        OnSpeedChangeAction?.Invoke(_speed);
        OnAnimationBlendChangeAction?.Invoke(inputMagnitude);
    }

    private void Jump()
    {
        if (_isGrounded)
        {
            OnJumpAction?.Invoke(false);
            OnFreeFallAction?.Invoke(false);

            if (_input.jump && _jumpTimeoutDelta <= 0.0f)
            {
                Debug.Log("jump");
                _rb.AddForce(Vector3.up * playerMovementSO.jumpForce, ForceMode.Impulse);
                OnJumpAction?.Invoke(true);

                _input.jump = false;
            }

            // jump timeout
            if (_jumpTimeoutDelta >= 0.0f)
            {
                _jumpTimeoutDelta -= Time.deltaTime;
            }
        }
        else
        {
            // reset the jump timeout timer
            _jumpTimeoutDelta = playerMovementSO.jumpTimeout;

            if (_rb.velocity.y <= 0f)
            {
                OnFreeFallAction?.Invoke(true);
            }

            _input.jump = false;
        }
    }
}