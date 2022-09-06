using UnityEngine;
using UnityEngine.Serialization;

public class InputManager : MonoBehaviour
{
    [Header("Player actions inputs")]
    public bool sprint;
    public bool walk;
    
    [Header("Player movement inputs")]
    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;
    
    [Header("Camera movement inputs")]
    public float cameraInputX;
    public float cameraInputY;
    
    public Vector2 movementInput;
    public Vector2 cameraInput;

    AnimatorManager _animatorManager;
    PlayerControls _playerControls;
    PlayerLocomotion _playerLocomotion;

    void Awake()
    {
        _animatorManager = GetComponent<AnimatorManager>();
        _playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    void OnEnable()
    {
        if (_playerControls == null)
        {
            _playerControls = new PlayerControls();

            _playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            _playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

            _playerControls.PlayerActions.Sprint.performed += i => sprint = true;
            _playerControls.PlayerActions.Sprint.canceled += i => sprint = false;
            _playerControls.PlayerActions.Walk.performed += i => walk = true;
            _playerControls.PlayerActions.Walk.canceled += i => walk = false;
        }

        _playerControls.Enable();
    }

    void OnDisable()
    {
        _playerControls.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleActionInput();
    }

    void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputY = cameraInput.y;
        cameraInputX = cameraInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));

        if (walk && moveAmount != 0)
            moveAmount = 0.49f;
        
        _animatorManager.UpdateAnimatorValues(0, moveAmount);
    }

    void HandleActionInput()
    {
        if (sprint && moveAmount > 0.5f)
            _playerLocomotion.isSprinting = true;
        else
            _playerLocomotion.isSprinting = false;

        _playerLocomotion.isWalking = moveAmount < 0.5f;
    }
}