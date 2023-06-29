using System;
using _Main.Scripts.Hud.UI;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using static GameManager;

namespace _Main.Scripts.Gun
{
    public class WeaponHandler : MonoBehaviour
    {
        [SerializeField]private Weapon currentWeapon;
        public Weapon CurrentWeapon => currentWeapon;

        private Weapon holdedWeapon;

        private Transform _anchorPoint;
        public event Action<Weapon> OnWeaponChange;
        public void Initialize(Transform anchorPoint)
        {
            _anchorPoint = anchorPoint;
            if (currentWeapon == null)
            {
                return;
            }
            holdedWeapon = currentWeapon;
            currentWeapon.transform.SetParent(_anchorPoint);
            currentWeapon.transform.SetPositionAndRotation(_anchorPoint.position,_anchorPoint.rotation);
        }

        public void ChangeWeapon(Weapon newWeapon)
        {
            holdedWeapon = newWeapon;
            if (currentWeapon != null)
            {
                currentWeapon.LeftDown();
            }
            currentWeapon = holdedWeapon;
            OnWeaponChange?.Invoke(currentWeapon);
            currentWeapon.transform.SetParent(_anchorPoint);
            currentWeapon.transform.SetPositionAndRotation(_anchorPoint.position,_anchorPoint.rotation);
        }
        
    }
}