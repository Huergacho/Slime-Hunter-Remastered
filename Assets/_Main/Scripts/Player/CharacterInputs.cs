using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInputs : MonoBehaviour
{
    private static InputManager _inputs;
    public  Vector2 MovementAxis { get; private set; }
    public Vector2 LookDirection { get; private set; }
    public bool IsShooting { get; private set; }
    public bool IsDashing { get; private set; }

    public bool IsMoving { get; private set; }
    
    public bool IsReloading { get; private set; }
    
    public bool IsPicking { get; private set; }
    private void Awake()
    {
        _inputs = new InputManager();
        _inputs.Player.Shooting.started += ctx => IsShooting = true;
        _inputs.Player.Shooting.canceled += ctx => IsShooting = false;
        _inputs.Player.Movement.performed += ctx => MovementAxis = ctx.ReadValue<Vector2>();
        _inputs.Player.Movement.performed += ctx => IsMoving = true;
        _inputs.Player.Movement.canceled += ctx => MovementAxis = Vector2.zero;
        _inputs.Player.Movement.canceled += ctx => IsMoving = false;
        _inputs.Player.Look.performed += ctx => LookDirection = ctx.ReadValue<Vector2>();
        _inputs.Player.Dash.started += ctx => IsDashing = true;
        _inputs.Player.Dash.performed += ctx => IsDashing = false;
        _inputs.Player.Dash.canceled += ctx => IsDashing = false;        
        _inputs.Player.Reload.started += ctx => IsReloading = true;
        _inputs.Player.Reload.performed += ctx => IsReloading = false;
        _inputs.Player.Reload.canceled += ctx => IsReloading = false;
        _inputs.Player.PickUp.started += ctx => IsPicking = true;
        _inputs.Player.PickUp.performed += ctx => IsPicking = false;
        _inputs.Player.PickUp.canceled += ctx => IsPicking = false;

    }
    private void OnEnable()
    {
        _inputs.Enable();
    }

    private void OnDisable()
    {
        _inputs.Disable();
    }
}