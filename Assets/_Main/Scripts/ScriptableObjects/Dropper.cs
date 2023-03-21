
using System.Collections.Generic;
using Assets._Main.Scripts.Generic_Pool;
using UnityEngine;

namespace Assets._Main.Scripts.Characters.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Dropper", menuName = "Dropper", order = 0)]
    [System.Serializable]
    public class Dropper : ScriptableObject
    {
        [SerializeField]private List<PoolObject> objectsToDrop;
        [SerializeField]private List<float> probablities;

        public Dictionary<PoolObject, float> Align { get; private set; } = new Dictionary<PoolObject, float>();

        private void OnEnable()
        {
            WarmUp();
        }

        public void WarmUp()
        {
            Align = new Dictionary<PoolObject, float>();
            for (int i = 0; i < objectsToDrop.Count; i++)
            {
                Align.Add(objectsToDrop[i],probablities[i]);
            }
        }

        [ContextMenu("Print")]
        public void PrintDictionary()
        {
            Debug.Log(Align.Keys.Count);
        }

    }
}