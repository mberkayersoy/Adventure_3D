using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private Vector2 JoystickSize = new Vector2(300, 300);
    [SerializeField] private FloatingJoystick Joystick;

    private Finger MovementFinger;
    private Vector2 movementDirection;

    // Events
    public event Action<Vector2> OnMovementAction;
    public event Action OnJumpAction;

    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }
    private void Start()
    {
        playerInput.actions["Jump"].performed +=OnJumpPerformed;
        playerInput.actions["Movement"].performed += ctx => OnMovementPerformed(ctx);
    }

    private void OnMovementPerformed(InputAction.CallbackContext ctx)
    {
        OnMovementAction?.Invoke(ctx.ReadValue<Vector2>());
    }
    private void OnJumpPerformed(InputAction.CallbackContext ctx)
    {
        OnJumpAction?.Invoke();
    }

    public Vector2 GetMovementVector()
    {
        Vector2 direction = playerInput.actions["Movement"].ReadValue<Vector2>();
        return direction;
    }


    private void OnEnable()
    {/*
        if (Application.isMobilePlatform || Application.isEditor)
        {
            EnhancedTouchSupport.Enable();
            ETouch.Touch.onFingerDown += HandleFingerDown;
            ETouch.Touch.onFingerUp += HandleLoseFinger;
            ETouch.Touch.onFingerMove += HandleFingerMove;
        }*/
    }

    private void OnDisable()
    {
       /* if (Application.isMobilePlatform || Application.isEditor)
        {
            ETouch.Touch.onFingerDown -= HandleFingerDown;
            ETouch.Touch.onFingerUp -= HandleLoseFinger;
            ETouch.Touch.onFingerMove -= HandleFingerMove;
            EnhancedTouchSupport.Disable();
        }*/
    }

    private void HandleFingerMove(Finger MovedFinger)
    {
        if (MovedFinger == MovementFinger)
        {
            Vector2 knobPosition;
            float maxMovement = JoystickSize.x / 2f;
            ETouch.Touch currentTouch = MovedFinger.currentTouch;

            if (Vector2.Distance(
                    currentTouch.screenPosition,
                    Joystick.RectTransform.anchoredPosition
                ) > maxMovement)
            {
                knobPosition = (
                    currentTouch.screenPosition - Joystick.RectTransform.anchoredPosition
                    ).normalized
                    * maxMovement;
            }
            else
            {
                knobPosition = currentTouch.screenPosition - Joystick.RectTransform.anchoredPosition;
            }

            Joystick.Knob.anchoredPosition = knobPosition;
            movementDirection = knobPosition / maxMovement;
        }
        OnMovementAction?.Invoke(movementDirection);
    }

    private void HandleLoseFinger(Finger LostFinger)
    {
        if (LostFinger == MovementFinger)
        {
            MovementFinger = null;
            Joystick.Knob.anchoredPosition = Vector2.zero;
            Joystick.gameObject.SetActive(false);
            movementDirection = Vector2.zero;
        }
        OnMovementAction?.Invoke(movementDirection);
    }

    private void HandleFingerDown(Finger TouchedFinger)
    {
        if (MovementFinger == null)
        {
            MovementFinger = TouchedFinger;
            movementDirection = Vector2.zero;
            Joystick.gameObject.SetActive(true);
            Joystick.RectTransform.anchoredPosition = TouchedFinger.screenPosition;
        }
    
    }
    private void OnGUI()
    {
        GUIStyle labelStyle = new GUIStyle()
        {
            fontSize = 24,
            normal = new GUIStyleState()
            {
                textColor = Color.white
            }
        };
        if (MovementFinger != null)
        {
            GUI.Label(new Rect(10, 35, 500, 20), $"Finger Start Position: {MovementFinger.currentTouch.startScreenPosition}", labelStyle);
            GUI.Label(new Rect(10, 65, 500, 20), $"Finger Current Position: {MovementFinger.currentTouch.screenPosition}", labelStyle);
        }
        else
        {
            GUI.Label(new Rect(10, 35, 500, 20), "No Current Movement Touch", labelStyle);
        }

        GUI.Label(new Rect(10, 10, 500, 20), $"Screen Size ({Screen.width}, {Screen.height})", labelStyle);
    }
}