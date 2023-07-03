using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using _Main.Scripts.Gun;
using _Main.Scripts.Objects;
using _Main.Scripts.PickUps;
using Assets._Main.Scripts.Sounds;
using Assets._Main.Scripts.Spawners;
using Assets._Main.Thecnical.Scripts.Interactables;
using MyEngine;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class MysteryBox : MonoBehaviour, ISound, ISpawner
{
    [SerializeField] private Weapon[] weapons;
    [Space]
    [Header("Assigns")]
    [SerializeField] private WeaponPickUp pickUpPrefab;
    [SerializeField] private Transform weaponPickUpPos;
    [SerializeField] private GameObject displayGun;
    [Space]
    [Header("Properties")]
    [SerializeField] private float openTime = 2;
    [SerializeField] private int loops = 3;
    [SerializeField] private float weaponChangeIntervalTime = 1;
    [SerializeField] private bool isActive = true;

    private MysteryBoxVisuals _visuals;
    private bool _isOpen;
    private MeshFilter _displayGunMesh;
    private bool _canInstance = true;


    public event Action<bool> OnOpenBox;

    private void Awake()
    {
        _visuals = GetComponent<MysteryBoxVisuals>();
        _displayGunMesh = displayGun.GetComponent<MeshFilter>();
    }

    private void Start()
    {
        _visuals.SuscribeEvents(this);
        _isOpen = false;
    }

    private void ShuffleWeaponList()
    {
        var newArray = MyRandom.Shuffle(weapons);
        weapons = newArray;

    }

    private Weapon GetRandomWeapon()
    {
        Weapon weaponToGive = weapons[0];
        return weaponToGive;
    }

    private void InstanceNewWeapon(Weapon give)
    {
        var pickUpInstance = Instantiate(pickUpPrefab, weaponPickUpPos.position, Quaternion.identity);
        pickUpInstance.ChangePrefab(give.gameObject);
        GameManager.Instance.AudioManager.ReproduceOnce(AudioEnum.SFX,Sound);
    }

    public void OpenMysteryBox()
    {
        if (_isOpen)
        {
            return;
        }

        OnOpenBox?.Invoke(true);
        StartCoroutine(StartCountDown());
    }

    private IEnumerator StartCountDown()
    {
        _isOpen = true;
        displayGun.SetActive(true);
        var currLoops = loops;
        ShuffleWeaponList();
        for (int i = weapons.Length - 1; i >= 0; i--)
        {
            ChangeWeaponDisplay(i);
            yield return new WaitForSeconds(weaponChangeIntervalTime);
            if (i <= 0)
            {
                if (currLoops <= 0)
                {
                    _canInstance = true;
                    break;
                } 
                i = weapons.Length;
                currLoops--;
            }
        }
        yield return new WaitUntil(() => _canInstance);
        InstanceNewWeapon(GetRandomWeapon());
        displayGun.SetActive(false);
        
        OnOpenBox?.Invoke(false);
        yield return new WaitForSeconds(openTime);
        _canInstance = false;
        _isOpen = false;
    }
    private void ChangeWeaponDisplay(int index)
    {
        var newWeapon = weapons[index];
        var newMesh = newWeapon.GetComponentInChildren<MeshFilter>().sharedMesh;
        if (newMesh == _displayGunMesh.mesh)
        {
            return;
        }
        _displayGunMesh.mesh = newMesh;
    }


    public AudioClip Sound { get; set; }

    public void OnSpawn()
    {
        
    }
}
