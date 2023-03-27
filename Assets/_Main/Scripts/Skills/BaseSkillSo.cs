using UnityEngine;

namespace Assets._Main.Scripts.Skills
{
    [CreateAssetMenu(fileName = "BaseSkill", menuName = "Skills/Base", order = 0)]
    public class BaseSkillSo : ScriptableObject
    {
        
        [field: SerializeField] public float HabilityDuration{ get; private set; } 

        [field: SerializeField] public float HablityCooldown { get; private set; } 
    }
}