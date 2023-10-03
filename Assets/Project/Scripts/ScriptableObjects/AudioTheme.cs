using System.Collections.Generic;
using Dominoes.Core.Enums;
using UnityEngine;

namespace Dominoes.ScriptableObjects
{
    [CreateAssetMenu(fileName = "AudioTheme", menuName = "Dominoes/AudioTheme")]
    internal class AudioTheme : ScriptableObject
    {
        [SerializeField] private AudioClip _buttonClick;

        public Dictionary<Audio, AudioClip> Audios { get; private set; }

        #region Unity
        private void OnValidate()
        {
            Audios = new Dictionary<Audio, AudioClip>()
            {
                { Audio.ButtonClick, _buttonClick },
            };
        }
        #endregion
    }
}
