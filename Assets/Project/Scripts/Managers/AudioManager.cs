using Dominoes.Core.Enums;
using Dominoes.ScriptableObjects;
using UnityEngine;

namespace Assets.Project.Scripts.Managers
{
    internal class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;
        [SerializeField] private AudioTheme _theme;

        public void Play(Audio audio)
        {
            if (audio != Audio.None)
            {
                _source.clip = _theme.Audios[audio];
                _source.Play();
            }
        }
    }
}
