using System;
using UnityEngine;
using CharacterController = _Main.Scripts.Player.CharacterController;

namespace Assets._Main.Scripts.Characters.Player.Player_States
{
    public class IdleState<T> : State<T>
    {
        T _walkInput;
        private Action<Vector3,float> _onIdle;
        private CharacterController _controller;
        Action _onAttack;
        Action _animation;
        Action _onPickUp;

        public IdleState(T walkInput, Action<Vector3, float> onIdle, Action onAttack,Action onPickUp, CharacterController controller, Action animation = null)
        {
            _walkInput = walkInput;
            _onIdle = onIdle;
            _controller = controller;
            _onAttack = onAttack;
            _onPickUp = onPickUp;
            _animation = animation;
        }

        public override void Execute()
        {

            if (_controller.Inputs.IsMoving)
            { 
                _parentFSM.Transition(_walkInput);
                return;
            }
            if (_controller.Inputs.IsShooting)
            {
                _onAttack?.Invoke();
            }else
            {
                _controller.Model.ResetShoot();
            }

            if (_controller.Inputs.IsPicking)
            {
                _onPickUp?.Invoke();
            }
            if (!_controller.Inputs.IsShooting && _controller.Inputs.IsReloading)
            {
                _controller.Model.Reload();
            }
            _onIdle?.Invoke(Vector3.zero, 0);
            _animation?.Invoke();
        }
    }
}