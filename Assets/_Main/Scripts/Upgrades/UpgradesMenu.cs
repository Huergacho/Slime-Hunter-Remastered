using System;
using _Main.Scripts.Hud.UI;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets
{
    public class UpgradesMenu : MonoBehaviour
    {
        [SerializeField] private BaseUpgrade[] upgrades;
        [SerializeField] private GameObject visuals;
        private void Start()
        {
            for (int i = 0; i < upgrades.Length; i++)
            {
                upgrades[i].SetController(this);
            }
        }

        public void OnShowMenu()
        {
            GameManager.Instance.PauseGame(false);
            visuals.SetActive(true);
            for (int i = 0; i < upgrades.Length; i++)
            {
                if (i >= 3)
                {
                    return;
                }
                upgrades[i].gameObject.SetActive(true);
            }
        }

        public void SelectedUpdate()
        {
            OnSelectedUpdate();
        }
        private void OnSelectedUpdate()
        {
            GameManager.Instance.PauseGame(false);
            for (int i = 0; i < upgrades.Length; i++)
            {
                if (i >= 3)
                {
                    return;
                }

                upgrades[i].gameObject.SetActive(false);
            }
            visuals.SetActive(false);

            MyEngine.MyRandom.Shuffle(upgrades);
        }
    }
}
