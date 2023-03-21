using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace _Main.Scripts.Gun.GunTypes
{
    public class Automatic : RangedWeapon
    {
        protected override async void WaitForNextAttack()
        {
            canAttack = false;
            await Task.Delay(TimeSpan.FromSeconds(baseStats.AttackRate));
            canAttack = true;
        }
    }
}