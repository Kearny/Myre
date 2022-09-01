using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager _inputManager;
    PlayerLocomotion _playerLocomotion;

    void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        _playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    void Update()
    {
        _inputManager.HandleAllInputs();
    }

    void FixedUpdate()
    {
        _playerLocomotion.HandleAllMovement();
    }
}