using Assets._Main.Scripts.Generic_Pool;
using UnityEngine;

namespace _Main.Scripts.Gun
{
    [CreateAssetMenu(fileName = "Ranged Weapon", menuName = "Stats/WeaponStats/Ranged", order = 0)]
    public class RangedWeaponStats : WeaponStats
    {
        [SerializeField] public float bulletSpeed;
        public float BulletSpeed => bulletSpeed;
        [SerializeField] public PoolObject bulletToSpawn;
        public PoolObject BulletToSpawn => bulletToSpawn;
        [SerializeField] public int maxAmmo;
        public int MaxAmmo => maxAmmo;

        public int maxAmmoInChamber;

        [SerializeField] public int ammoCharges;
        public int AmmoCharges => ammoCharges;

        [field: SerializeField] public AudioClip noAmmoSound { get; private set; }
    }
}