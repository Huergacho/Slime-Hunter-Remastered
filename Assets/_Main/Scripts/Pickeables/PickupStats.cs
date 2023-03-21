using UnityEngine;

namespace Assets._Main.Scripts.Pickeables
{
    [CreateAssetMenu(fileName = "Base PickUP", menuName = "PickUp/Base", order = 0)]
    public class PickupStats : ScriptableObject
    {
        [field: SerializeField]  public bool magnetize { get; private set; }
        [field: SerializeField]  public LayerMask contactLayer{ get; private set; }
        [field: SerializeField]  public float magnetizeSpeed{ get; private set; }
        [field: SerializeField]  public float pickUpRadius{ get; private set; }
        [field: SerializeField]  public float lifeTime{ get; private set; }
    }
}