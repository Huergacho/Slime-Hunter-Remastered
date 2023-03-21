using Assets._Main.Scripts.Pickeables;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Main.Scripts.Gun
{
    [CreateAssetMenu(fileName = "WeaponStats", menuName = "Stats/WeaponStats/Base", order = 0)]
    public class WeaponStats : ScriptableObject
    {
        [SerializeField] public float AttackRate;
        [SerializeField] public int Damage;
        [SerializeField] public float Range;
        [SerializeField] public LayerMask ContactLayers;
        [field:SerializeField] public AudioClip Sound { get; private set; }

    }
}