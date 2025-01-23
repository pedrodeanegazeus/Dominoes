using System;
using UnityEngine;

namespace Gazeus.Mobile.Domino
{
    [CreateAssetMenu(fileName = "AudioTheme", menuName = "Dominoes/AudioTheme")]
    public class AudioTheme : ScriptableObject
    {
        public enum Audio
        {
            ButtonClick,
        }

        [SerializeField] private AudioClip _buttonClick;

        public AudioClip GetAudioClip(Audio audio)
        {
            AudioClip audioClip = audio switch
            {
                Audio.ButtonClick => _buttonClick,
                _ => throw new NotImplementedException($"Audio '{audio}' not implemented"),
            };

            return audioClip;
        }
    }
}
