using System.Collections;
using _Main.Scripts.Interactables.Buy_Items;
using UnityEngine;

namespace Assets._Main.Thecnical.Scripts.Interactables.Buy_Items
{
    public class TemporalBuy : BuyItem
    {
        private bool canBuy = true;
        [SerializeField] private float interactInterval = 0.5f;
        public override void OnInteract(MonoBehaviour model)
        {
            if (!canBuy)
            {
                return;
            }
            base.OnInteract(model);
        }

        protected override void BuyActions()
        {
            base.BuyActions();
            StartCoroutine(EnterInCooldown());
        }

        IEnumerator EnterInCooldown()
        {
            canBuy = false;
            _showOutline = false;
            yield return new WaitForSeconds(interactInterval);
            _showOutline = true;
            canBuy = true;
        }
    }
}