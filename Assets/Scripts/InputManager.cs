using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls _playerControls;
    public Vector2 movementInput;

    void OnEnable()
    {
        if (_playerControls == null)
        {
            _playerControls = new PlayerControls();

            _playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
        }

        _playerControls.Enable();
    }
    
    void OnDisable()
    {
        _playerControls.Disable();
    }
}
