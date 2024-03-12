using Dominoes.Core.Enums;
using Dominoes.Core.Interfaces.Repositories;
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

        private IGameConfigRepository _gameConfigRepository;
        private IGzLogger<AudioManager> _logger;

        public void Initialize(IGzServiceProvider serviceProvider)
        {
            _gameConfigRepository = serviceProvider.GetRequiredService<IGameConfigRepository>();
            _logger = serviceProvider.GetRequiredService<IGzLogger<AudioManager>>();
        }

        public void Play(Audio audio)
        {
            _logger.Debug("CALLED: {method} - {audio}",
                          nameof(Play),
                          audio.ToString());

            if (_gameConfigRepository.GameConfig.Audio && audio != Audio.None)
            {
                AudioClip audioClip = _theme.Audios[audio];
                _audioSource.PlayOneShot(audioClip);

                _logger.Info("Audio {audio} played", audio.ToString());
            }
        }
    }
}
