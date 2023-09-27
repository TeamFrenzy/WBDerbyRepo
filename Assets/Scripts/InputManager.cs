using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

[DefaultExecutionOrder(-1)]
public class InputManager : Singleton<InputManager>
{
    #region Events

    public delegate void StartTouchPrimary(Vector2 position, float time);

    public event StartTouchPrimary OnStartTouchPrimary;

    public delegate void EndTouchPrimary(Vector2 position, float time);

    public event EndTouchPrimary OnEndTouchPrimary;

    public delegate void TapPrimary(Vector2 position, float time);

    public event TapPrimary OnTapPrimary;

    public delegate void HoldPrimary(Vector2 position, float time);

    public event HoldPrimary OnHoldPrimary;

    #endregion
    
    private InputActions playerControls;

    private void Awake()
    {
        playerControls = new InputActions();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Start()
    {
        playerControls.Touch.PrimaryTap.started += ctx =>
        {
            if (ctx.interaction is SlowTapInteraction)
            {
                StartTouchPrimaryMethod(ctx);
            }
        };
        //SlowTapInteraction
        playerControls.Touch.PrimaryTap.performed += ctx =>
        {
            if (ctx.interaction is SlowTapInteraction)
            {
                EndTouchPrimaryMethod(ctx);
            }
            else
            {
                TapPrimaryMethod(ctx);
            }
        };

        playerControls.Touch.PrimaryTap.canceled += ctx =>
        {
            if (ctx.interaction is SlowTapInteraction)
            {
                EndTouchPrimaryMethod(ctx);
            }
        };
    }

    private void StartTouchPrimaryMethod(InputAction.CallbackContext context)
    {
        if (OnStartTouchPrimary != null) OnStartTouchPrimary(PrimaryPosition(), (float)context.startTime);
    }

    private void EndTouchPrimaryMethod(InputAction.CallbackContext context)
    {
        if (OnEndTouchPrimary != null) OnEndTouchPrimary(PrimaryPosition(), (float)context.time);
    }

    private void TapPrimaryMethod(InputAction.CallbackContext context)
    {
        if (OnTapPrimary != null) OnTapPrimary(PrimaryPosition(), (float)context.startTime);
    }

    public Vector2 PrimaryPosition()
    {
        return playerControls.Touch.PrimaryPosition.ReadValue<Vector2>();
    }

    public void ButtonCheck()
    {
        Debug.Log("Button Has Been Touched");
    }
}