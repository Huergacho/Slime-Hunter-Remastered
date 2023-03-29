using _Main.Scripts.Gun;
using UnityEngine;

namespace _Main.Scripts.PickUps
{
    public class WeaponPickUp : Pickeable
    {
        [SerializeField] private GameObject prefabToInstance;
        private MonoBehaviour _model;
        private MeshFilter _filter;
        protected override void Awake()
        {
            _filter = GetComponent<MeshFilter>();
        }

        protected override void Start()
        {
            if (_filter != null)
            {
                _filter.mesh = prefabToInstance.GetComponentInChildren<MeshFilter>().sharedMesh;
            }
            base.Start();

        }

        public void ChangePrefab(GameObject newPrefab)
        {
            prefabToInstance = newPrefab;
        }

        protected override void ActionBeforeDisappear()
        {
            
        }

        protected override void ActionsOnPickUp()
        {            
            var newWeapon = Instantiate(prefabToInstance);
            var handler = _model.GetComponent<WeaponHandler>();
            if (handler == null)
            {
                return;
            }
            var weaponToChange = newWeapon.GetComponent<Weapon>();
            if (weaponToChange == null)
            {
                print("Falta el componente de Weapon");
                return;
            }
            weaponToChange.SetOwner(handler.gameObject);
            handler.ChangeWeapon(weaponToChange);
            GameManager.Instance.AudioManager.ReproduceOnce(Sound);
            base.ActionsOnPickUp();
        }
        protected override void DisappearAction()
        {
            Destroy(gameObject);
        }

        public override void OnInteract(MonoBehaviour model)
        {
            _model = model;
            ActionsOnPickUp();
        }
    }
}