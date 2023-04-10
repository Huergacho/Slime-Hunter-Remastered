using _Main.Scripts.Interactables.Buy_Items;
using UnityEngine;

namespace Assets._Main.Thecnical.Scripts.Interactables.Buy_Items
{
    public class PermanentBuy : BuyItem
    {
        protected override void BuyActions()
        {
            base.BuyActions();
            Destroy(gameObject);
        }
    }
}