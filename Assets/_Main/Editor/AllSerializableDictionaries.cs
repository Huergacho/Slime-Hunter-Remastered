using System;

namespace _Main.Editor
{
    public enum StatNames
    {
        FireRateF,
        DamageI,
        MaxHealthI,
        MoveSpeedF
    }

    public abstract class AllSerializableDictionaries
    {
    }

    [Serializable]
    public class StatIntDictionary : SerializableDictionary<StatNames, int>
    {
    }

    [Serializable]
    public class StatFloatDictionary : SerializableDictionary<StatNames, float>
    {
    }
}