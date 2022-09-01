using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class PlayerLocomotion : MonoBehaviour
{
    public float movementSpeed = 7;
    public float rotationSpeed = 15;

    Transform _cameraObject;
    InputManager _inputManager;
    Vector3 _movementVelocity;
    Rigidbody _playerRigidbody;

    void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        _playerRigidbody = GetComponent<Rigidbody>();
        Debug.Assert(Camera.main != null, "Camera.main != null");
        _cameraObject = Camera.main.transform;
    }

    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
    }

    void HandleMovement()
    {
        _movementVelocity = _cameraObject.forward * _inputManager.verticalInput;
        _movementVelocity += _cameraObject.right * _inputManager.horizontalInput;
        _movementVelocity.Normalize();
        _movementVelocity.y = 0;
        _movementVelocity *= movementSpeed;

        _playerRigidbody.velocity = _movementVelocity;
    }

    void HandleRotation()
    {
        var targetDirection = _cameraObject.forward * _inputManager.verticalInput;
        targetDirection += _cameraObject.right * _inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
            targetDirection = transform.forward;

        var targetRotation = Quaternion.LookRotation(targetDirection);
        var playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }
}