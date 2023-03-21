using System.Collections;
using UnityEngine;

namespace _Main.Scripts.Gun.GunTypes
{
    public class Burst : RangedWeapon
    {
        [SerializeField] private int bulletPerBurst = 3;
        protected override void RealizeAttack()
        {
            StartCoroutine(BurstInstance());
        }

        private IEnumerator BurstInstance()
        {
            for (int i = 0; i < bulletPerBurst; i++)
            {
                base.RealizeAttack();
                yield return new WaitForSeconds(0.03f);
            }
            StopCoroutine(BurstInstance());
        }
    }
}