using UnityEngine;

namespace Assets._Main.Scripts.Characters.ScriptableObjects.Actor
{
    public abstract class BaseActorStats : ScriptableObject
    {
        [field: SerializeField] public int MaxLife { get; private set; }
        [field:SerializeField] public int MaxSpeed{ get; private set; }
    }
}