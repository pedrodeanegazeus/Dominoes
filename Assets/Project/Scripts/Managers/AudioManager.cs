using Dominoes.Core.Enums;
using Dominoes.ScriptableObjects;
using Gazeus.CoreMobile.Commons.Core.Extensions;
using Gazeus.CoreMobile.Commons.Core.Interfaces;
using UnityEngine;

namespace Dominoes.Managers
{
    internal class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioTheme _theme;

        [SerializeField] private GameState _gameState;

        private IGzLogger<AudioManager> _logger;

        public void Initialize(IGzServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<IGzLogger<AudioManager>>();
        }

        public void Play(Audio audio)
        {
            _logger.Debug("CALLED: {method} - {audio}",
                          nameof(Play),
                          audio.ToString());

            if (_gameState.Audio && audio != Audio.None)
            {
                AudioClip audioClip = _theme.Audios[audio];
                _audioSource.PlayOneShot(audioClip);

                _logger.Info("Audio {audio} played", audio.ToString());
            }
        }
    }
}
