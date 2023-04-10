using UnityEngine;

namespace _Main.Scripts.Interactables.Buy_Items
{
    [CreateAssetMenu(fileName = "BuyItem", menuName = "Stats/BuyItem", order = 0)]
    public class BuyItemStats : ScriptableObject
    {
        [field:SerializeField] public int ItemValue { get; private set; }
        [field:SerializeField] public LayerMask ContactLayers { get; private set;}
    }
}