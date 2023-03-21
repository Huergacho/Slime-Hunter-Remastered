using System.Collections.Generic;
using UnityEngine;

namespace Assets._Main.Scripts.Sounds
{
    public interface ISound
    {
        [field:SerializeField]public AudioClip Sound { get; set; }

    }
}