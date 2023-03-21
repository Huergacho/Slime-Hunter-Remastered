using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace _Main.Scripts.Gun.Meele
{
    public class BaseMelee : Weapon
    {
        [SerializeField] protected WeaponStats _stats;
        protected override void RealizeAttack()
        {
            Collider2D data =  Physics2D.OverlapCircle(attackPoint.position, _stats.Range,_stats.ContactLayers);
            MakeSound();
            if (data != null)
            {
               data.gameObject.GetComponent<LifeController>().TakeDamage(_stats.Damage);
            }
        }

        protected override async void WaitForNextAttack()
        {
            canAttack = false;
            await Task.Delay(TimeSpan.FromSeconds(_stats.AttackRate));
            canAttack = true;
        }

        private void OnDrawGizmosSelected()
        {
            if (_stats == null)
            {
                return;
            }
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _stats.Range);
        }
    }
}