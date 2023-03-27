using Unity.VisualScripting;
using UnityEngine;

namespace _Main.Scripts.PickUps
{
    [CreateAssetMenu(fileName = "Base PickUP", menuName = "PickUp/Base", order = 0)]
    public class PickupStats : ScriptableObject
    {
        [field:Header("Magnetize Stats")]

        [field: SerializeField]  public bool magnetize { get; private set; }
        [field: SerializeField]  public float magnetizeSpeed{ get; private set;}
        
        [field:Header("Pick Up Stats")]
        [field: SerializeField]  public LayerMask contactLayer{ get; private set; }
        [field: SerializeField, Range(0.1f,3f)]  public float pickUpRadius{ get; private set; }

        [field: Header("Disappear Stats")]
        [field: SerializeField]
        public bool canDisappear { get; private set; } = true;
        
        [field: SerializeField]  public float lifeTime{ get; private set; }
        
    }
}