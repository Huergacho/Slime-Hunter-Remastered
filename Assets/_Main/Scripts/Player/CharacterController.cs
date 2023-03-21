using System;
using _Main.Scripts.Player.Player_States;
using Assets._Main.Scripts.Characters.Player;
using Assets._Main.Scripts.Characters.Player.Player_States;
using States;
using UnityEngine;

public class CharacterController : MonoBehaviour,IFSM
{
    private CharacterInputs _inputs;
    public  CharacterInputs Inputs => _inputs;
    private CharacterModel _model;
    public CharacterModel Model => _model;
    private FSM<PlayerStates> _fsm;
    private PickUpDetector _pickUpDetector;
    private void Awake()
    {
        _pickUpDetector = GetComponent<PickUpDetector>();
        _model = GetComponent<CharacterModel>();
        _inputs = GetComponent<CharacterInputs>();
    }

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetPlayer(gameObject);
        }
        InitFsm();
    }

    private void Update()
    {
        Rotate();
        _fsm.UpdateState();
    }
    private void Rotate()
    {
        _model.RotateTowardMouse(_inputs.LookDirection);
    }
    private void MoveCommand(Vector2 dir, float desiredSpeed)
    {
        _model.Move(dir,desiredSpeed);
    }

    private void ShootCommand()
    {
        _model.Attack();
    }
    private void DashCommand()
    {
        _model.Dash();
        //_model.Dash(_inputs.MovementAxis);
    }

    private void PickUp()
    {
        _pickUpDetector.ManualPickUp();
    }
    public void InitFsm()
    {
        var idle = new IdleState<PlayerStates>(PlayerStates.Move, MoveCommand, ShootCommand, PickUp, this);
        var move = new MoveState<PlayerStates>(PlayerStates.Idle, MoveCommand, ShootCommand,DashCommand,PickUp, this,_model.Stats.MaxSpeed);
        
        idle.AddTransition(PlayerStates.Move,move);
        
        move.AddTransition(PlayerStates.Idle,idle);
        _fsm = new FSM<PlayerStates>(idle);
    }
}