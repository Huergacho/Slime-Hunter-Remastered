using System;
using System.Collections;
using System.Threading.Tasks;
using _Main.Scripts.Gun;
using Assets._Main.Scripts.Characters.ScriptableObjects.Actor;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

namespace Assets._Main.Scripts.Characters.Player
{
    [RequireComponent(typeof(WeaponHandler), typeof(LifeController), typeof(CharacterView))]
    [RequireComponent(typeof(Rigidbody))]
    public class CharacterModel : MonoBehaviour
    {

        [SerializeField] private Transform weaponAnchor;
        [SerializeField] private PlayerStatsSO stats;
        [SerializeField] private Transform mouseIndicator;
        [SerializeField] private bool isometricMovement;
        [SerializeField] private LayerMask layersToLook;
        #region Components
        private WeaponHandler _handler;
        private Camera _camera;
        private LifeController _lifeController;
        public Rigidbody _rb { get; private set;}
        private CharacterView _view;
        #endregion
        #region Public Fields
        public LifeController LifeController => _lifeController;
        public PlayerStatsSO Stats => stats;
        #endregion

        // private Dash _dash;

        public UnityEvent onDash;

        #region UnityMethods
        private void Awake()
        {
            _lifeController = GetComponent<LifeController>();
            _handler = GetComponent<WeaponHandler>();
            _rb = GetComponent<Rigidbody>();
            _handler.Initialize(weaponAnchor);
            _camera = Camera.main;
            _view = GetComponent<CharacterView>();
        }
        private void Start()
        {
            _handler.OnWeaponChange += EquipWeapon;
            _lifeController.AssignMaxLife(stats.MaxLife);
            _view.AssignProperties(this);
            SuscribeEvents();
        }
        #endregion
        private void SuscribeEvents()
        {
            _lifeController.OnModifyHealth += ModifyHealth;
            _lifeController.OnDie += DieActions;
        }
        #region MovementMethods
        public void Move(Vector3 dir, float desiredSpeed)
        {

            if (_rb.velocity.magnitude > stats.MaxSpeed || desiredSpeed == 0)
            {
                return;
            }
            if (!isometricMovement)
            {
                _rb.velocity = dir.normalized * desiredSpeed;
            }
            else
            {
                _rb.velocity = dir.normalized + GameUtilities.ToIso(dir.normalized) * desiredSpeed;
            }
            _view.MoveAnimation(_rb.velocity.normalized.magnitude);
        }
        public void Dash()
        {
            onDash?.Invoke();
            _view.MoveAnimation(_rb.velocity.normalized.magnitude);
        }
        #endregion
        #region ShootMethods
        public void Attack()
        {
            if (_handler.CurrentWeapon == null)
            {
                _view.EquipAnimation(false);
                return;
            }
            var currWp = _handler.CurrentWeapon.GetComponent<RangedWeapon>();
            _handler.CurrentWeapon.Attack();
            _view.AttackAnimation(true);
        }

        public void ResetShoot()
        {
            if (_handler.CurrentWeapon == null)
            {
                return;
            }
            _handler.CurrentWeapon.ResetShoot();
            _view.AttackAnimation(false);

        }

        public void Reload()
        {
            if (_handler.CurrentWeapon == null)
            {
                return;
            }
            var rangedWeapon = _handler.CurrentWeapon.GetComponent<RangedWeapon>();
            if (rangedWeapon != null)
            {
                rangedWeapon.Reload();
            }
        }
        #endregion
        public void RotateTowardMouse()
        {
            var mousePos = GameUtilities.GetMouseWorldPosition(_camera, layersToLook);
            mouseIndicator.transform.position = mousePos;
            mousePos.y = transform.position.y;
            transform.LookAt(mousePos);
        }
        private void DieActions()
        {
            gameObject.SetActive(false);
        }

        public void EquipWeapon(Weapon weapon)
        {
            if(weapon != null)
                _view.EquipAnimation(true);
        }
        private void ModifyHealth(float curr, float rest)
        {
        }
        
    }
}