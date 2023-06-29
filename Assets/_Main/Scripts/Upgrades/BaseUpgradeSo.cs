using UnityEngine;

namespace _Main.Scripts.Upgrades
{
    [CreateAssetMenu(fileName = "BaseUpgradeSO", menuName = "Upgrades/Base", order = 0)]
    public class BaseUpgradeSo : ScriptableObject
    {
        [field:SerializeField] public float MaxValue { get; private set; }
        [field:SerializeField] public float MinValue { get; private set; }
        [field:SerializeField, Multiline]public string Description { get; private set; }
        [field:SerializeField]public Sprite Image { get; private set; }
    }
}