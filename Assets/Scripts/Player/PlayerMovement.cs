using DG.Tweening;
using StarterAssets;
using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector3 direction;
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float smoothRotation;
    [SerializeField] private float jumpForce;
    [SerializeField] private float speedChangeRate;
    [SerializeField] private float JumpTimeout = 0.50f;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isFlipping;

    private float targetRotation = 0.0f;
    private float rotationVelocity;
    private float animationBlend;
    private float jumpTimeoutDelta;
    private float FPSdeltaTime = 0.0f;

    //Events
    public event Action<float> OnSpeedChangeAction;
    public event Action<float> OnAnimationBlendChangeAction;
    public event Action<bool> OnGroundStateChangeAction;
    public event Action<bool> OnJumpAction;
    public event Action<bool> OnFreeFallAction;
    public event Action<bool> OnFrontFlipAction;

    private Rigidbody rb;
    private GroundCheck groundCheck;
    private StarterAssetsInputs input;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        groundCheck = GetComponent<GroundCheck>();
    }

    private void Start()
    {
        input = GetComponent<StarterAssetsInputs>();
        groundCheck.OnIsGroundedChangeAction += GroundCheck_OnIsGroundedChangeAction;

        // reset our timeouts on start
        jumpTimeoutDelta = JumpTimeout;
    }

    private void GroundCheck_OnIsGroundedChangeAction(bool isGrounded)
    {
        this.isGrounded = isGrounded;
        OnGroundStateChangeAction?.Invoke(isGrounded);
    }

    private void Update()
    {
        FPSdeltaTime += (Time.unscaledDeltaTime - FPSdeltaTime) * 0.1f;
        Move();
        Jump();

        if (transform.position.y <= -4)
        {
            transform.position = Vector3.zero;
        }
    }



    private void Move()
    {
        float targetSpeed = maxSpeed;

        if (input.move == Vector2.zero) targetSpeed = 0.0f;

        // a reference to the players current horizontal velocity
        float currentHorizontalSpeed = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z).magnitude;

        float speedOffset = 0.1f;
        float inputMagnitude = input.analogMovement ? input.move.magnitude : 1f;

        // accelerate or decelerate to target speed
        if (currentHorizontalSpeed < targetSpeed - speedOffset ||
            currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            // creates curved result rather than a linear one giving a more organic speed change
            // note T in Lerp is clamped, so we don't need to clamp our speed
            speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                Time.deltaTime * speedChangeRate);

            // round speed to 3 decimal places
            speed = Mathf.Round(speed * 1000f) / 1000f;
        }
        else
        {
            speed = targetSpeed;
        }
        animationBlend = Mathf.Lerp(animationBlend, targetSpeed, Time.deltaTime * speedChangeRate);
        if (animationBlend < 0.01f) animationBlend = 0f;

        // normalise input direction
        Vector3 inputDirection = new Vector3(input.move.x, 0.0f, input.move.y).normalized;

        // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is a move input rotate player when the player is moving
        if (input.move != Vector2.zero)
        {
            targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                              Camera.main.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity,
                smoothRotation);

            // rotate to face input direction relative to camera position
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        //Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;

        Vector3 moveVector = inputDirection * speed;
        rb.velocity = new Vector3 (moveVector.x, rb.velocity.y, moveVector.z);

        OnSpeedChangeAction?.Invoke(speed);
        OnAnimationBlendChangeAction?.Invoke(inputMagnitude);
    }

    private void Jump()
    {
        if (isGrounded)
        {
            OnJumpAction?.Invoke(false);
            OnFreeFallAction?.Invoke(false);

            if (input.jump && jumpTimeoutDelta <= 0.0f)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                OnJumpAction?.Invoke(true);

                input.jump = false;
            }

            // jump timeout
            if (jumpTimeoutDelta >= 0.0f)
            {
                jumpTimeoutDelta -= Time.deltaTime;
            }
        }
        else
        {
            //if (isFlipping)
            //{
            //    // reset the jump timeout timer
            //    jumpTimeoutDelta = JumpTimeout;

            //    if (rb.velocity.y <= 0f)
            //    {
            //        OnFreeFallAction?.Invoke(true);
            //    }
            //}
            // reset the jump timeout timer
            jumpTimeoutDelta = JumpTimeout;

            if (rb.velocity.y <= 0f)
            {
                OnFreeFallAction?.Invoke(true);
            }

            input.jump = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("velocity:" + rb.velocity);
        if (other.gameObject.CompareTag("Finish"))
        {

            isFlipping = true;
            OnFrontFlipAction?.Invoke(isFlipping);
            rb.AddForce(Vector3.up * jumpForce * 1.2f, ForceMode.Impulse);
        }

    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            isFlipping = false;
            OnFrontFlipAction?.Invoke(isFlipping);
        }
    }

    private void OnGUI()
    {
        int width = Screen.width, height = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, width, height * 2 / 100);
        style.alignment = TextAnchor.UpperCenter;
        style.fontSize = height * 2 / 100;
        style.normal.textColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        float msec = FPSdeltaTime * 1000.0f;
        float fps = 1.0f / FPSdeltaTime;

        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
    }

}