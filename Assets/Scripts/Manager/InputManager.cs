using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnTouchStart;
    public delegate void CancelTouch(Vector2 position, float time);
    public event CancelTouch OnTouchCancel;
    
    [SerializeField] private InputAction touchContact;
    [SerializeField] private InputAction touchPosition;
    
    private InputAction moveUp;
    private InputAction moveDown;
    
    private Camera mainCamera;
    
    void Awake()
    {
         mainCamera = Camera.main;
         
         touchContact.started += ctx => StartTouchInput(ctx);
         touchContact.canceled += ctx => CancelTouchInput(ctx);
    }
    
    public void DisableGestures()
    {
        touchContact.Disable();
        touchPosition.Disable();
    }
    
    void OnEnable()
    {
        touchContact.Enable();
        touchPosition.Enable();
    }
    
    void OnDisable()
    {
        touchContact.Disable();
        touchPosition.Disable();
    }
    
    private Vector3 ScreenToWorld(Vector3 position)
    {
        position.z = mainCamera.nearClipPlane;
        return mainCamera.ScreenToWorldPoint(position);
    }
    
    public void StartTouchInput(InputAction.CallbackContext context)
    {
        if(OnTouchStart != null)
            OnTouchStart(ScreenToWorld(touchPosition.ReadValue<Vector2>()), (float) context.startTime);
    }
    
    public void CancelTouchInput(InputAction.CallbackContext context)
    {
        if(OnTouchCancel != null)
            OnTouchCancel(ScreenToWorld(touchPosition.ReadValue<Vector2>()), (float) context.time);
    }
}