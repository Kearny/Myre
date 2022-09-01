using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float verticalInput;
    public float horizontalInput;
    AnimatorManager _animatorManager;

    float _moveAmount;
    Vector2 _movementInput;
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

            _playerControls.PlayerMovement.Movement.performed += i => _movementInput = i.ReadValue<Vector2>();
        }

        _playerControls.Enable();
    }

    void OnDisable()
    {
        _playerControls.Disable();
    }

    void HandleMovementInput()
    {
        verticalInput = _movementInput.y;
        horizontalInput = _movementInput.x;
        _moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        _animatorManager.UpdateAnimatorValues(0, _moveAmount);
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
    }
}