using System;
using UnityEngine;
using Utilities;

namespace _Main.Scripts.Utilities
{
    public class RotateToTarget : MonoBehaviour
    {
        private Camera _camera;
        [SerializeField] private LayerMask _layerMaskToLook;
        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            transform.LookAt(GameUtilities.GetMouseWorldPosition(_camera,_layerMaskToLook));
        }
    }
}