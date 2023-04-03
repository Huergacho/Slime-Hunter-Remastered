using System;
using _Main.Scripts.Gun;
using _Main.Scripts.Gun.GunTypes;
using _Main.Scripts.PickUps;
using _Main.Scripts.Utilities;
using Assets._Main.Scripts;
using Assets._Main.Scripts.Generic_Pool;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
#if UNITY_EDITOR

    enum WeaponTypes
    {
        Burst,
        Semi,
        Auto
    }

    public class GunGenerator : EditorWindow
    {
        private string weaponName = "";
        private float AttackRate = 1;
        private int Damage = 1;
        private float Range = 3;
        private LayerMask ContactLayers = 0;
        private float bulletSpeed = 10;
        private PoolObject bulletToSpawn;
        private int maxAmmo = 10;
        private int ammoCharges = 1;
        private int maxAmmoInChamber = 30;
        private GameObject objectToCreate;
        private bool createPrefab = false;
        private bool canPickUp = true;

        [MenuItem("Tools/Generator/Weapon")]
        public static void ShowWindow()
        {
            GetWindow(typeof(GunGenerator));
        }

        private void OnGUI()
        {
            GUILayout.Space(30);
            GUILayout.Label("Create New Weapon", EditorStyles.boldLabel);
            weaponName = EditorGUILayout.TextField("Weapon Name", weaponName);
            GUILayout.Space(10);
            ShowWeaponStats();
            GUILayout.Space(10);
            ShowBulletStats();
            GUILayout.Space(30);
            canPickUp = EditorGUILayout.Toggle("Can PickUp", canPickUp);
            createPrefab = EditorGUILayout.Toggle("Create Prefab", createPrefab);
            GUILayout.Space(30);
            SelectWeapon();
        }

        private void SelectWeapon()
        {
            if (GUILayout.Button("Automatic"))
            {
                CreateWeapon(WeaponTypes.Auto);
            }

            if (GUILayout.Button("Semi Automatic"))
            {
                CreateWeapon(WeaponTypes.Semi);
            }

            if (GUILayout.Button("Burst"))
            {
                CreateWeapon(WeaponTypes.Burst);
            }
        }

        private void ShowBulletStats()
        {
            GUILayout.Label("Bullet Stats", EditorStyles.boldLabel);
            LayerMask tempMask = EditorGUILayout.MaskField(
                InternalEditorUtility.LayerMaskToConcatenatedLayersMask(ContactLayers), InternalEditorUtility.layers);
            ContactLayers = InternalEditorUtility.ConcatenatedLayersMaskToLayerMask(tempMask);
            // Debug.Log(ContactLayers.value);
            bulletSpeed = EditorGUILayout.FloatField("Bullet Speed", bulletSpeed);
        }

        private void ShowWeaponStats()
        {
            GUILayout.Label("WeaponStats", EditorStyles.boldLabel);
            
            AttackRate = EditorGUILayout.Slider("Attack Rate", AttackRate,0.1f, 2f);
            Damage = EditorGUILayout.IntField("Damage", Damage);
            Range = EditorGUILayout.FloatField("Range", Range);
            maxAmmo = EditorGUILayout.IntField("Max Ammo", maxAmmo);
            ammoCharges = EditorGUILayout.IntSlider("Ammo Charges", ammoCharges, 1, 4);
            maxAmmoInChamber = EditorGUILayout.IntField("Max Ammo In Chamber", maxAmmoInChamber);
        }

        void CreateWeapon(WeaponTypes type)
        {
            objectToCreate = new GameObject(weaponName);
            Filter(type);
            var weaponComponent = objectToCreate.GetComponent<Weapon>();
            CreateBasics(weaponComponent);
            AssingStats();
            CreatePrefab(objectToCreate);
        }

        void Filter(WeaponTypes type)
        {
            switch (type)
            {
                case WeaponTypes.Burst:
                    objectToCreate.AddComponent<Burst>();
                    break;
                case WeaponTypes.Auto:
                    objectToCreate.AddComponent<Automatic>();
                    break;
                case WeaponTypes.Semi:
                    objectToCreate.AddComponent<SemiAutomatic>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private void CreateBasics(Weapon weaponClass)
        {

            if (canPickUp)
            {
                objectToCreate.layer = LayerMask.NameToLayer("ManualPickUpLayer");
                var pickable = objectToCreate.AddComponent<WeaponPickUp>();
                pickable.ChangePrefab(objectToCreate);
            }
            BoxCollider col = objectToCreate.AddComponent<BoxCollider>();
            col.isTrigger = true;
            GameObject visuals = new GameObject("Visuals");
            GameObject firePoint = new GameObject("Fire Point");
            visuals.transform.parent = objectToCreate.transform;
            
            firePoint.transform.parent = objectToCreate.transform;
            objectToCreate.AddComponent<RotateToTarget>();
            weaponClass.AssignAttackPoint(firePoint.transform);
        }

        private void AssingStats()
        {
            var newStats = CreateInstance<RangedWeaponStats>();
            newStats.AttackRate = AttackRate;
            newStats.Damage = Damage;
            newStats.Range = Range;
            newStats.ContactLayers = ContactLayers;
            newStats.bulletSpeed = bulletSpeed;
            newStats.maxAmmo = maxAmmo;
            newStats.bulletToSpawn = bulletToSpawn;
            newStats.maxAmmoInChamber = maxAmmoInChamber;
            newStats.ammoCharges = ammoCharges;
            objectToCreate.GetComponent<RangedWeapon>().baseStats = newStats;
            if (createPrefab)
            {
                SaveScriptableObj(newStats);
            }
        }

        private void CreatePrefab(GameObject obj)
        {
            if (!createPrefab)
            {
                return;
            }

            string localPath = "Assets/_Main/Prefabs/Weapons/" + obj.name + ".prefab";
            bool prefabSuccess;
            PrefabUtility.SaveAsPrefabAssetAndConnect(obj, localPath, InteractionMode.UserAction,
                out prefabSuccess);
            if (prefabSuccess)
            {
                Debug.Log("Prefab Created");
            }
            else
            {
                Debug.Log("Prefab Creation Failed");
            }
        }

        private void SaveScriptableObj(ScriptableObject obj)
        {
            string localPath = "Assets/_Main/Thecnical/ScriptabbleObjects/Weapons/" + weaponName + ".asset";
            AssetDatabase.CreateAsset(obj, localPath);
            AssetDatabase.SaveAssets();
        }
    }
        #endif
