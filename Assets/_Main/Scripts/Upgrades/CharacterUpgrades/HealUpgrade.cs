using _Main.Scripts.Player;
using Assets;
using Assets._Main.Scripts.Characters.Player;
using UnityEngine;

namespace _Main.Scripts.Upgrades
{
    public class HealUpgrade : BaseUpgrade
    {
        protected override void ActionToDo()
        {
            base.ActionToDo();
            PlayerHeal();
        }
        
        public void PlayerHeal()
        {
            GameManager.Instance.Player.GetComponent<PlayerModel>().LifeController.Heal(CurrentValue);
        }

        public void MovementUpgrade()
        {
            GameManager.Instance.Player.GetComponent<PlayerModel>().Stats.ChangeStat(GlobalStats.MoveSpeed,Mathf.Round(CurrentValue));
        }
    }
}