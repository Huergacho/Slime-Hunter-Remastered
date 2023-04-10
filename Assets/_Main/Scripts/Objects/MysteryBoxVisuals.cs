using System;
using UnityEngine;

namespace _Main.Scripts.Objects
{
    public class MysteryBoxVisuals : MonoBehaviour
    {
        private Animator _animator;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            
        }

        public void SuscribeEvents(MysteryBox controller)
        {
            controller.OnOpenBox += OpenStatus;
        }

        private void OpenStatus(bool status)
        {
            _animator.SetBool("Open", status);
        }
    }
}