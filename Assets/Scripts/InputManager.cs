using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float verticalInput;
    public float horizontalInput;

    Vector2 _movementInput;
    PlayerControls _playerControls;

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
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
    }
}