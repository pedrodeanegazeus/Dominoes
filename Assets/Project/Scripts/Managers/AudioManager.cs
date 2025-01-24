using Gazeus.CoreMobile.SDK.Core.Extensions;
using Gazeus.CoreMobile.SDK.Core.Interfaces;
using UnityEngine;
using static Gazeus.Mobile.Domino.AudioTheme;

namespace Gazeus.Mobile.Domino.Managers
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioTheme _audioTheme;

        private IGzLogger<AudioManager> _logger;
        private AudioSource _audioSource;

        #region Unity
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        #endregion

        public void Initialize()
        {
            _logger = GameManager.ServiceProviderManager.GetService<IGzLogger<AudioManager>>();
            _logger.Info("Initialized");
        }

        public void PlayAudio(Audio audio)
        {
            _logger.LogMethodCall(nameof(PlayAudio),
                                  audio);

            AudioClip audioClip = _audioTheme.GetAudioClip(audio);

            _audioSource.PlayOneShot(audioClip);
        }
    }
}
