using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Main.Scripts.Upgrades
{
    [System.Serializable]
    public class StatsInfo
    {
        [field:SerializeField]public GlobalStats statType;
        [field:SerializeField]public float statValue;
    }
    [CreateAssetMenu(fileName = "BaseStat", menuName = "Stats", order = 0)]
    public class Stats : ScriptableObject
    {
        [SerializeField]private List<StatsInfo> statsInfo = new List<StatsInfo>();
        private Hashtable statHash;
        
        #region Dictionaries

                private Dictionary<GlobalStats, float> _statsValues = new Dictionary<GlobalStats, float>();
                
        public void SetUpDictionaries()
        {
            foreach (var item in statsInfo)
            {
                _statsValues.Add(item.statType,item.statValue);
            }
            Debug.Log("Diccionario Seteado Para" + name);
        }
        public float GetStat(GlobalStats stat)
        {
            if(_statsValues.TryGetValue(stat,out float value))
            {
                return value;
            }
            else
            {
             Debug.Log("No existe valor para "+ stat +" En "+ this.name);
             return 0;
            }
        }

        public void ChangeStat(GlobalStats stat, float amount)
        {
            if(_statsValues.TryGetValue(stat,out float value))
            {
                _statsValues[stat] += amount;
            }
            else
            {
                Debug.Log("No existe valor para "+ stat +" En "+ this.name);
            }
        }

        #endregion
        private void OnEnable()
        {
            SetUpDictionaries();
        }


        // public float GetStat(GlobalStats stat)
        // {
        //     foreach (var item in statsInfo)
        //     {
        //         if (item.statType == stat)
        //         {
        //             return item.statValue;
        //             break;
        //         }
        //     }
        //     Debug.Log("No existe valor para "+ stat +" En "+ this.name);
        //     return 0;
        // }
        //
        // public void ChangeStat(GlobalStats stat, float amount)
        // {
        //     foreach (var item in statsInfo)
        //     {
        //         if (item.statType == stat)
        //         { 
        //             item.statValue += amount;
        //             break;
        //         }
        //     }
        // }
    }
}