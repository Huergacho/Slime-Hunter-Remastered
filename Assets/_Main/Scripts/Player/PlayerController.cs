using _Main.Scripts.PickUps;
using _Main.Scripts.Player.Player_States;
using Assets._Main.Scripts.Characters.Player;
using Assets._Main.Scripts.Characters.Player.Player_States;
using States;
using UnityEngine;

namespace _Main.Scripts.Player
{
    [RequireComponent(typeof(PlayerModel),typeof(PlayerInputs),typeof(PickUpDetector))]
    public class PlayerController : MonoBehaviour,IFSM
    {
        private PlayerInputs _inputs;
        public  PlayerInputs Inputs => _inputs;
        private PlayerModel _model;
        public PlayerModel Model => _model;
        private FSM<PlayerStates> _fsm;
        private PickUpDetector _pickUpDetector;
        private void Awake()
        {
            _pickUpDetector = GetComponent<PickUpDetector>();
            _model = GetComponent<PlayerModel>();
            _inputs = GetComponent<PlayerInputs>();
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
            _model.RotateTowardMouse();
        }
        private void MoveCommand(Vector3 dir, float desiredSpeed)
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
}