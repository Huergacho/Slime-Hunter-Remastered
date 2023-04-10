using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using _Main.Scripts.Tools;
using Assets._Main.Thecnical.Scripts.Interactables.Buy_Items;
using UnityEditor.AI;
using UnityEngine;
using UnityEngine.AI;
using NavMeshBuilder = UnityEngine.AI.NavMeshBuilder;

namespace _Main.Scripts.Interactables.Buy_Items
{
    public class BuyableDoor : PermanentBuy
    {
        private NavMeshData _navMeshBuilder;
        // The center of the build
        public Transform m_Tracked;

        // The size of the build bounds
        public Vector3 m_Size = new Vector3(80.0f, 20.0f, 80.0f);

        NavMeshData m_NavMesh;
        AsyncOperation m_Operation;
        NavMeshDataInstance m_Instance;
        List<NavMeshBuildSource> m_Sources = new List<NavMeshBuildSource>();

        protected override void BuyActions()
        {
            base.BuyActions();
            UpdateNavMesh();
            Destroy(gameObject);
        }
        
        
        void UpdateNavMesh(bool asyncUpdate = false)
        {
            NavMeshSourceTag.Collect(ref m_Sources);
            var defaultBuildSettings = NavMesh.GetSettingsByID(0);
            var bounds = QuantizedBounds();

            if (asyncUpdate)
                m_Operation = NavMeshBuilder.UpdateNavMeshDataAsync(m_NavMesh, defaultBuildSettings, m_Sources, bounds);
            else
                NavMeshBuilder.UpdateNavMeshData(m_NavMesh, defaultBuildSettings, m_Sources, bounds);
        }

        static Vector3 Quantize(Vector3 v, Vector3 quant)
        {
            float x = quant.x * Mathf.Floor(v.x / quant.x);
            float y = quant.y * Mathf.Floor(v.y / quant.y);
            float z = quant.z * Mathf.Floor(v.z / quant.z);
            return new Vector3(x, y, z);
        }

        Bounds QuantizedBounds()
        {
            // Quantize the bounds to update only when theres a 10% change in size
            var center = m_Tracked ? m_Tracked.position : transform.position;
            return new Bounds(Quantize(center, 0.1f * m_Size), m_Size);
        }

    }
}