using System;
using System.Collections.Generic;
using Dominoes.Core.Enums;
using UnityEngine;

namespace Dominoes.ScriptableObjects
{
    [CreateAssetMenu(fileName = "AudioTheme", menuName = "Dominoes/AudioTheme")]
    internal class AudioTheme : ScriptableObject
    {
        [SerializeField] private List<AudioEntry> _audioEntries;

        internal List<AudioEntry> AudioEntries => _audioEntries;
    }

    [Serializable]
    internal class AudioEntry
    {
        public AudioClip AudioClip;
        public Audio Audio;
    }
}
