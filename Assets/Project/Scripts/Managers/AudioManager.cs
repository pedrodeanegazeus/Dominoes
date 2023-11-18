using Dominoes.Core.Enums;
using Dominoes.ScriptableObjects;
using Gazeus.CoreMobile.Commons.Core.Interfaces;
using UnityEngine;

namespace Dominoes.Managers
{
    internal class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;
        [SerializeField] private AudioTheme _theme;

        private IGzLogger<AudioManager> _logger;

        public void Play(Audio audio)
        {
            _logger.Debug("CALLED: {method} - {audio}",
                          nameof(Play),
                          audio);

            if (audio != Audio.None)
            {
                _source.clip = _theme.Audios[audio];
                _source.Play();
            }
        }

        #region Unity
        private void Awake()
        {
            _logger = ServiceProvider.GetRequiredService<IGzLogger<AudioManager>>();
        }
        #endregion
    }
}
