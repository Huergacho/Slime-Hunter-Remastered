using System.Collections.Generic;
using _Main.Scripts.Upgrades;
using UnityEngine;

namespace Assets._Main.Scripts.Characters.ScriptableObjects.Actor
{
    [CreateAssetMenu(fileName = "PlayerStats", menuName = "Stats/Actor/Player", order = 0)]
    public class PlayerStatsSO : BaseActorStats
    {
        [field:SerializeField]public float DashTime { get; private set; }
        [field:SerializeField]public float DashSpeed { get; private set; }
        [field:SerializeField]public float DashCooldown{ get; private set; }
        
        
    }
}