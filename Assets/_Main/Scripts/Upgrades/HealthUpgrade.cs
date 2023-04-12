using Assets._Main.Scripts.Characters.Player;
using UnityEngine;

namespace _Main.Scripts.Upgrades
{
    public class HealthUpgrade : MonoBehaviour
    {
        public void PlayerHeal()
        {
            GameManager.Instance.Player.GetComponent<PlayerModel>().Stats.ChangeStat(GlobalStats.Health,10);
        }

        public void MovementUpgrade()
        {
            GameManager.Instance.Player.GetComponent<PlayerModel>().Stats.ChangeStat(GlobalStats.MoveSpeed,10);
        }
    }
}