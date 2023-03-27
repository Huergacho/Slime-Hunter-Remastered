using UnityEngine;

namespace _Main.Scripts.PickUps
{
    [CreateAssetMenu(fileName = "PickUpDetectorProperties", menuName = "PickUp/Detector", order = 1)]
    public class PickUpDetectorProperties : ScriptableObject
    {
        [field: SerializeField] public LayerMask AutomaticPickUpLayer { get; private set; }
        [field: SerializeField] public LayerMask ManualPickUpLayer { get; private set; }
        [field: SerializeField] public float PickUpRadius { get; private set; }
    }
}