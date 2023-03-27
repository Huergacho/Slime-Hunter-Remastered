using System.Collections.Generic;
using UnityEngine;

namespace Assets._Main.Scripts.Sounds
{
    [CreateAssetMenu(fileName = "AudioGallery", menuName = "Audio/Gallery", order = 0)]
    public class AudioGallerySo : ScriptableObject
    {
        [field: SerializeField] public AudioClip Audio { get; private set; }
    }
}