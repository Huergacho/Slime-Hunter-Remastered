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
    public class CharacterModel : MonoBehaviour
    {
        [SerializeField] private Transform weaponAnchor;
        [SerializeField] private PlayerStatsSO stats;
        [SerializeField] private float healTime;
        #region Components
        private WeaponHandler _handler;
        private Camera _camera;
        private LifeController _lifeController;
        private Rigidbody2D _rb;
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
            _rb = GetComponent<Rigidbody2D>();
            _handler.Initialize(weaponAnchor);
            _camera = Camera.main;
            _view = GetComponent<CharacterView>();
        }
        private void Start()
        {
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
        public void Move(Vector2 dir, float desiredSpeed)
        {
            if (_rb.velocity.magnitude > stats.MaxSpeed)
            {
                return;
            }
            _rb.velocity = dir.normalized * desiredSpeed;
            _view.MoveAnimation(desiredSpeed);
        
        }
        public void Dash()
        {
            onDash?.Invoke();
            _view.OnRun();
            _view.MoveAnimation(_rb.velocity.normalized.magnitude);
        }
        #endregion
        #region ShootMethods
        public void Attack()
        {
            if (_handler.CurrentWeapon == null)
            {
                return;
            }

            var currWp = _handler.CurrentWeapon.GetComponent<RangedWeapon>();
            _handler.CurrentWeapon.Attack();
        }

        public void ResetShoot()
        {
            if (_handler.CurrentWeapon == null)
            {
                return;
            }
            _handler.CurrentWeapon.ResetShoot();
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
        public void RotateTowardMouse(Vector2 lookDir)
        {
            var mousePos=GameUtilities.LookTowardsMousePos(_camera,transform.position,lookDir);
            if (_handler.CurrentWeapon == null)
            {
                return;
            }
            _handler.CurrentWeapon.transform.right = mousePos;
        }
        private void DieActions()
        {
            gameObject.SetActive(false);
        }

        private void ModifyHealth(float curr, float rest)
        {
        }
        
    }
}