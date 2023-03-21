using Assets._Main.Scripts.Characters.ScriptableObjects.Actor;
using UnityEngine;

namespace Characters.Enemy
{
    [CreateAssetMenu(fileName = "EnemyStats", menuName = "Stats/Actor/EnemyStats", order = 0)]
    public class EnemyStatsSO : BaseActorStats
    {
        [field:SerializeField] public int PerRoundLifeUpgrade { get; private set; }

        [field:SerializeField] public int PointValue{ get; private set; }
        [field:SerializeField] public float DistanceToAttack{ get; private set; }
    }
}