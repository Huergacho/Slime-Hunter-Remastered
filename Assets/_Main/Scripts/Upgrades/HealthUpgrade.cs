using Assets;
using Assets._Main.Scripts.Characters.Player;
using UnityEngine;

namespace _Main.Scripts.Upgrades
{
    public class HealthUpgrade : BaseUpgrade
    {
        protected override void SetVisuals()
        {
            RandomStat();
            description = description + " " + Mathf.Round(currentValue);
            base.SetVisuals();
        }

        public void PlayerHeal()
        {
            GameManager.Instance.Player.GetComponent<PlayerModel>().Stats.ChangeStat(GlobalStats.Health,Mathf.Round(currentValue));
        }

        public void MovementUpgrade()
        {
            GameManager.Instance.Player.GetComponent<PlayerModel>().Stats.ChangeStat(GlobalStats.MoveSpeed,Mathf.Round(currentValue));
        }
    }
}