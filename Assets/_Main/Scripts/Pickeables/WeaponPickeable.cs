using System;
using _Main.Scripts.Gun;
using Assets._Main.Scripts.Sounds;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets._Main.Scripts.Pickeables
{
    public class WeaponPickeable : Pickeable
    {
        [SerializeField] private GameObject prefabToInstance;
        private MonoBehaviour _model;

        protected override void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected override void Start()
        {
            if (_spriteRenderer != null)
            {
                _spriteRenderer.sprite = prefabToInstance.GetComponentInChildren<SpriteRenderer>().sprite;
            }
            base.Start();

        }

        public void ChangePrefab(GameObject newPrefab)
        {
            prefabToInstance = newPrefab;
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