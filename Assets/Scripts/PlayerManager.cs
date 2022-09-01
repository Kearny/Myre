using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    CameraManager _cameraManager;
    InputManager _inputManager;
    PlayerLocomotion _playerLocomotion;

    void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        _playerLocomotion = GetComponent<PlayerLocomotion>();
        _cameraManager = FindObjectOfType<CameraManager>();
    }

    void Update()
    {
        _inputManager.HandleAllInputs();
    }

    void FixedUpdate()
    {
        _playerLocomotion.HandleAllMovement();
    }

    void LateUpdate()
    {
        _cameraManager.HandleAllCameraMovement();
    }
}