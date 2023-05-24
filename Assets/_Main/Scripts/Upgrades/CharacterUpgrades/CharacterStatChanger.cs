using _Main.Scripts.Player;
using Assets;
using Assets._Main.Scripts.Characters.Player;
using UnityEngine;

namespace _Main.Scripts.Upgrades
{
    public class CharacterStatChanger : BaseUpgrade
    {
        [SerializeField] private GlobalStats statToModify;
        [SerializeField] private Stats target;
        protected override void ActionToDo()
        {
            base.ActionToDo();
            //ChangeStat(target,statToModify);
        }

        public void ChangeStat()
        {
            target.ChangeStat(statToModify,CurrentValue);
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