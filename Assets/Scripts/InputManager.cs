using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float verticalInput;
    public float horizontalInput;
    public float cameraInputX;
    public float cameraInputY;
    public Vector2 movementInput;
    public Vector2 cameraInput;
    AnimatorManager _animatorManager;

    float _moveAmount;
    PlayerControls _playerControls;

    void Awake()
    {
        _animatorManager = GetComponent<AnimatorManager>();
    }

    void OnEnable()
    {
        if (_playerControls == null)
        {
            _playerControls = new PlayerControls();

            _playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            _playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
        }

        _playerControls.Enable();
    }

    void OnDisable()
    {
        _playerControls.Disable();
    }

    void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputY = cameraInput.y;
        cameraInputX = cameraInput.x;

        _moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        _animatorManager.UpdateAnimatorValues(0, _moveAmount);
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
    }
}