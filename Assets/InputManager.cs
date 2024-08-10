using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading.Tasks;

[DefaultExecutionOrder(-1)]
public class InputManager : Singleton<InputManager>
{
    #region Events 
    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;
    #endregion

    private TouchGestures touchGestures;
    private Camera mainCamera;

    private void Awake()
    {
        Debug.Log("InputManager Awake called.");
        touchGestures = new TouchGestures();
        if (touchGestures == null)
        {
            Debug.LogError("TouchGestures initialization failed.");
        }
        else
        {
            Debug.Log("TouchGestures initialized successfully.");
        }
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        Debug.Log("InputManager OnEnable called.");
        if (touchGestures != null)
        {
            touchGestures.Enable();
            Debug.Log("TouchGestures enabled.");
        }
        else
        {
            Debug.LogError("TouchGestures instance is null.");
        }
    }

    private void OnDisable()
    {
        if (touchGestures != null)
        {
            touchGestures.Disable();
            Debug.Log("TouchGestures disabled.");
        }
    }

    void Start()
    {
        touchGestures.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        touchGestures.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
    }

    private async void StartTouchPrimary(InputAction.CallbackContext context)
    {
        await Task.Delay(50);

        Vector2 touchPosition = touchGestures.Touch.PrimaryPosition.ReadValue<Vector2>();

        Debug.Log("Raw touch position: " + touchPosition);

        if (touchPosition.x < 0 || touchPosition.x > Screen.width ||
            touchPosition.y < 0 || touchPosition.y > Screen.height ||
            touchPosition.y < 0 || touchPosition.y > Screen.height ||
            float.IsInfinity(touchPosition.x) || float.IsInfinity(touchPosition.y) ||
            float.IsNaN(touchPosition.x) || float.IsNaN(touchPosition.y))
        {
            Debug.LogWarning("Touch position out of screen bounds or invalid: " + touchPosition);
            return;
        }

        if (OnStartTouch != null)
            OnStartTouch?.Invoke(Utils.ScreenToWorld(mainCamera, touchPosition), (float)context.startTime);
    }


    private void EndTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnEndTouch != null) OnEndTouch(Utils.ScreenToWorld(mainCamera, touchGestures.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.time);
    }

    public Vector2 PrimaryPosition() {
        return Utils.ScreenToWorld(mainCamera, touchGestures.Touch.PrimaryPosition.ReadValue<Vector2>());
    }
}
