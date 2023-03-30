using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assets._Main.Scripts.Sounds;
using UnityEngine;

namespace Assets._Main.Scripts.Skills
{
    public class Skill : MonoBehaviour,ISound
    {
        private float _currentCooldown;
        [SerializeField]protected CanvasFiller _canvasFiller;
        private bool _canShow = false;
        //private Coroutine cooldownRoutine;
        private bool _habilityUsed;
        [SerializeField] protected BaseSkillSo skillStats;
        [SerializeField]protected bool hasToRecharge;
        public virtual void Initialize(MonoBehaviour owner)
        {
        }
        private void Update()
        {
            if (_habilityUsed && !GameManager.Instance.IsPaused)
            {
                if (_canvasFiller == null || !hasToRecharge)
                {
                    return;
                }
                if (_currentCooldown < skillStats.HablityCooldown)
                {
                    _currentCooldown += Time.deltaTime;
                    _canvasFiller.UpdateCanvas(_currentCooldown, skillStats.HablityCooldown);
                    return;
                }
                
                Recharge();

            }
        }
        public void ActivateSkill()
        {
            if (!_habilityUsed)
            {
                _currentCooldown = 0;
                SkillAction();
                EnterInCooldown();
            }
        }

        protected virtual void SkillAction()
        {
        
        }
        
    
        private async void EnterInCooldown()
        {
                _habilityUsed = true;
                await Task.Delay(TimeSpan.FromSeconds(skillStats.HabilityDuration));
                StopAction();
                // await Task.Delay(TimeSpan.FromSeconds(skillStats.HablityCooldown));
                // StartCoroutine(CheckForPause());

        }

        protected void Recharge()
        {
            if (Sound != null)
            {
                GameManager.Instance.AudioManager.ReproduceOnce(Sound);
            }

            _habilityUsed = false;
        }
        protected virtual void StopAction()
        {
        
        }
        [field:SerializeField]public AudioClip Sound { get; set; }
    }
}

