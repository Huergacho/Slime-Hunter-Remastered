using System;
using UnityEngine;
using Utilities;

namespace _Main.Scripts.Utilities
{
    public class RotateToTarget : MonoBehaviour
    {
        private Camera _camera;
        public LayerMask _layerMaskToLook;
        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            var mousePos = GameUtilities.GetMouseWorldPosition(_camera, _layerMaskToLook);
            if (mousePos != Vector3.zero)
            {
                transform.LookAt(mousePos);
            }
            mousePos.y = transform.position.y;
            transform.LookAt(mousePos);

        }
    }
}