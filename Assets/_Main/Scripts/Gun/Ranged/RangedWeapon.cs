using System;
using Assets._Main.Scripts;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using Utilities;

namespace _Main.Scripts.Gun
{
    [RequireComponent(typeof(EntitySpawner))]
    public class RangedWeapon : Weapon
    {
        [SerializeField]public RangedWeaponStats baseStats;
        private EntitySpawner _bulletSpawner;
        private int _currentAmmo;
        public int CurrentAmmo => _currentAmmo;
        private int _currentAmmoInChamber;
        public int CurrentAmmoInChamber => _currentAmmoInChamber;
        [SerializeField]protected CinemachineImpulseSource Source;

        [SerializeField]private ParticleSystem shootParticles;

        // [SerializeField] private string bulletToSpawn = "bullet";

        private void Awake()
        {
            _bulletSpawner = GetComponent<EntitySpawner>();
            _bulletSpawner.objectToSpawn = baseStats.BulletToSpawn;
            _currentAmmo = baseStats.MaxAmmo;
            baseStats.maxAmmoInChamber = baseStats.MaxAmmo * baseStats.AmmoCharges;
            _currentAmmoInChamber = baseStats.maxAmmoInChamber;
        }

        private void Start()
        {
            Sound = baseStats.Sound;
        }

        public void AddAmmo(int quantity)
        {
            if (_currentAmmoInChamber >=  baseStats.maxAmmoInChamber)
            {
                return;
            }

            if ((_currentAmmoInChamber + quantity) >=  baseStats.maxAmmoInChamber)
            {
                _currentAmmoInChamber =  baseStats.maxAmmoInChamber;
                return;
            }

            _currentAmmoInChamber += quantity;
            return;

        }
        private bool CheckForAmmo()
        {
            if (_currentAmmo <= 0)
            {
                NoAmmoSound();
                return false;
            }
            _currentAmmo--;
            return true;
        }

        public void Reload()
        {
            if (_currentAmmo == baseStats.MaxAmmo)
            {
                return;
            }
            if (_currentAmmoInChamber <= 0)
            {
                NoAmmoSound();
                return;
            }

            var currAmmoValue = baseStats.MaxAmmo - _currentAmmo;
            if (_currentAmmoInChamber > currAmmoValue)
            {
                _currentAmmo += currAmmoValue;
                _currentAmmoInChamber -= currAmmoValue;

            }else if (currAmmoValue >= _currentAmmoInChamber)
            {
                _currentAmmo += _currentAmmoInChamber;
                _currentAmmoInChamber = 0;
            }
            _currentAmmo = Mathf.Clamp(_currentAmmo, 0, baseStats.MaxAmmo);

        }
        private void OnDrawGizmosSelected()
        {
            if (baseStats == null)
            {
                return;
            }
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, baseStats.Range * 2);
        }
        // ReSharper disable Unity.PerformanceAnalysis
        protected override void RealizeAttack()
        {
            if (!CheckForAmmo())
            {
                return;
            }
            var newProj = _bulletSpawner.SpawnObject(attackPoint);
           var bullet = newProj.GetComponent<Bullet>();
            shootParticles?.Play();
            Source.GenerateImpulse();
            bullet.InitializeStats(baseStats.BulletSpeed,baseStats.Damage,baseStats.Range,baseStats.ContactLayers,attackPoint.forward,owner);
            MakeSound();
        }

        public int[]  GetCurrentAmmoInfo()
        {
            int[] values = new[] { _currentAmmo, _currentAmmoInChamber };
            return values;
        }

        public void ResetAmmo()
        {
            _currentAmmoInChamber = baseStats.maxAmmoInChamber * baseStats.ammoCharges;
        }

        public void NoAmmoSound()
        {
            GameManager.Instance.AudioManager.ReproduceOnce(AudioEnum.Shoots,baseStats.noAmmoSound);
        }
    }
}
