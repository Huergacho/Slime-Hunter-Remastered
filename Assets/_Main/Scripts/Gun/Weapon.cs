using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assets._Main.Scripts;
using Assets._Main.Scripts.Sounds;
using UnityEngine;
using Utilities;

namespace _Main.Scripts.Gun
{
    public class Weapon : MonoBehaviour,ISound
    {
        [SerializeField] protected WeaponStats stats;
        public WeaponStats Stats => stats;

        protected bool canAttack = true;
        [SerializeField]protected Transform attackPoint;
        private bool _hasShooted;
        protected GameObject owner;
        

        public virtual void AssignAttackPoint(Transform firepoint)
        {
            attackPoint = firepoint;
        }
        public virtual void Attack()
        {
            if (!canAttack || GameManager.Instance.IsPaused)
            {
                return;
            }

            _hasShooted = true;
            EnterInCooldown();
            RealizeAttack();

        }

        protected virtual void MakeSound()
        {
            if(Sound != null)
                GameManager.Instance.AudioManager.ReproduceOnce(AudioEnum.Shoots,Sound);
        }
        public virtual void ResetShoot()
        {
            _hasShooted = false;
        }

        protected void EnterInCooldown()
        {
            WaitForNextAttack();
        }
        protected virtual void RealizeAttack()
        {
            //Declaro como quiero que se haga el ataque
        }
        protected virtual async void WaitForNextAttack()
        {
            canAttack = false;
            while (_hasShooted)
            {
                await Task.Yield();
            }
            canAttack = true;
        }

        public void SetOwner(GameObject newOwner)
        {
            owner = newOwner;
        }

        public void LeftDown()
        {
            Destroy(gameObject);
        }
        public AudioClip Sound { get; set; }
    }
}