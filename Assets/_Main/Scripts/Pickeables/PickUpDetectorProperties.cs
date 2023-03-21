using UnityEngine;

namespace Assets._Main.Scripts.Pickeables
{
    [CreateAssetMenu(fileName = "PickUpDetectorProperties", menuName = "Pickeables/Managers/PickUpDetectorPT", order = 0)]
    public class PickUpDetectorProperties : ScriptableObject
    {
        [field: SerializeField] public LayerMask AutomaticPickUpLayer { get; private set; }
        [field: SerializeField] public LayerMask ManualPickUpLayer { get; private set; }
        [field: SerializeField] public float PickUpRadius { get; private set; }
    }
}