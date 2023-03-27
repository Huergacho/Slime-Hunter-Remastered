using System;
using _Main.Scripts.Gun;
using TMPro;
using UnityEngine;

namespace _Main.Scripts.Hud.UI
{
    public class WeaponHud : MonoBehaviour
    {   
        [SerializeField] private TextMeshProUGUI ammoText;
        //   [SerializeField] private Image gunImage;
        [SerializeField] private Color[] ammoColor;
        [SerializeField]private WeaponHandler currentHandler;
        private RangedWeapon currWeaponToCheck;
        private bool canUpdate;
        private void Start()
        {
            currentHandler.OnWeaponChange += UpdateCurrentGun;
            CheckForRangedWeapon(currWeaponToCheck);
        }

        private void Update()
        {
            if (!canUpdate)
            {
                return;
            }
            UpdateAmmoText();
        }

        public void SetAmmoTextToMeele()
        {
            ammoText.text = "- / -";

        }
        public void UpdateAmmoText()
        {
            int currentAmmo = currWeaponToCheck.CurrentAmmo;
            int currentAmmoInChamber = currWeaponToCheck.CurrentAmmoInChamber;
            ammoText.text = "" + currentAmmo + "/ " + currentAmmoInChamber;

            if (currentAmmoInChamber == 0 && currentAmmo == 0)
            {
                ammoText.color = ammoColor[1];
            }
            else
            {
                ammoText.color = ammoColor[0];
            }
        }
        void CheckForRangedWeapon(Weapon currentWeapon)
        {
            if (currentWeapon is RangedWeapon)
            {
                var ranged = currentWeapon.GetComponent<RangedWeapon>();
                currWeaponToCheck = ranged;
                canUpdate = true;
                return;
            }

            canUpdate = false;
            SetAmmoTextToMeele();
        }
        public void UpdateCurrentGun(Weapon currWeapon)
        {
            CheckForRangedWeapon(currWeapon);
        }
    }
}