using UnityEngine;

namespace _Main.Scripts.Upgrades
{
    [CreateAssetMenu(fileName = "BaseUpgradeSO", menuName = "Upgrades/Base", order = 0)]
    public class BaseUpgradeSo : ScriptableObject
    {
        [field:SerializeField] public int MaxValue { get; private set; }
        [field:SerializeField] public int MinValue { get; private set; }
        [field:SerializeField, Multiline]public string Description { get; private set; }
        [field:SerializeField]public Sprite Image { get; private set; }
    }
}