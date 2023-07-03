using System;
using System.Collections;
using _Main.Scripts.Gun;
using _Main.Scripts.Objects;
using UnityEngine;

namespace _Main.Scripts.PickUps
{
    [RequireComponent(typeof(ObjectDisguiser))]
    public class WeaponPickUp : Pickeable
    {
        [SerializeField] private GameObject prefabToInstance;
        private MonoBehaviour _model;
        private ObjectDisguiser _disguiser;
        protected override void Awake()
        {
            base.Awake();
            _disguiser = GetComponent<ObjectDisguiser>();
        }

        protected override void Start()
        {
            _disguiser.ChangeSkin(prefabToInstance.GetComponent<ObjectDisguiser>());
            base.Start();

        }
        public void ChangePrefab(GameObject newPrefab)
        {
            prefabToInstance = newPrefab;
            _disguiser.ChangeSkin(prefabToInstance.GetComponent<ObjectDisguiser>());
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
            GameManager.Instance.AudioManager.ReproduceOnce(AudioEnum.SFX,Sound);
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