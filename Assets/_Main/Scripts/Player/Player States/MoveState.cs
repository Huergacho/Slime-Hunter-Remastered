using System;
using UnityEngine;

namespace _Main.Scripts.Player.Player_States
{
    public class MoveState<T> : State<T>
    {
        T _idleInput;
        Action _onShoot;
        Action _onDash;
        Action<Vector3,float> _onRun;
        PlayerController _controller;
        private float _desiredSpeed;
        Action _animation;
        private Action _onPickUp;
        public MoveState(T idleInput, Action<Vector3, float> onRun, Action onShoot, Action onDash, Action onPickUp, PlayerController controller, float desiredSpeed, Action animation = null)
        {
            _idleInput = idleInput;
            _onRun = onRun;
            _onShoot = onShoot;
            _onDash = onDash;
            _onPickUp = onPickUp;
            _controller = controller;
            _desiredSpeed = desiredSpeed;
            _animation = animation;
        }
        public override void Execute()
        {
            if (!_controller.Inputs.IsMoving|| GameManager.Instance.IsPaused)
            {
                _parentFSM.Transition(_idleInput);
                Debug.Log("Paso a Idle");
                return;
            }

            if (_controller.Inputs.IsDashing)
            {
                _onDash?.Invoke();
            }
            if (_controller.Inputs.IsShooting)
            {
                _onShoot?.Invoke();
            }
            else
            {
                _controller.Model.ResetShoot();
            }
            if (!_controller.Inputs.IsShooting && _controller.Inputs.IsReloading)
            {
                _controller.Model.Reload();
            }

            if (_controller.Inputs.IsPicking)
            {
                _onPickUp?.Invoke();
            }
            _onRun?.Invoke(new Vector3(_controller.Inputs.MovementAxis.x,0,_controller.Inputs.MovementAxis.z), _desiredSpeed);
            _animation?.Invoke();
        }
    }
}