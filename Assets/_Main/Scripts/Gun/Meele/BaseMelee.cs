using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace _Main.Scripts.Gun.Meele
{
    public class BaseMelee : Weapon
    {
        protected override void RealizeAttack()
        {
            Collider[] data =  Physics.OverlapSphere(attackPoint.position, stats.Range,stats.ContactLayers);
            MakeSound();
            foreach (var col in data)
            {
                if (col != null)
                {
                    col.gameObject.GetComponent<LifeController>().TakeDamage(stats.Damage);
                }
            }

        }

        protected override async void WaitForNextAttack()
        {
            canAttack = false;
            await Task.Delay(TimeSpan.FromSeconds(stats.AttackRate));
            canAttack = true;
        }

        private void OnDrawGizmosSelected()
        {
            if (stats == null)
            {
                return;
            }
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, stats.Range);
        }
    }
}