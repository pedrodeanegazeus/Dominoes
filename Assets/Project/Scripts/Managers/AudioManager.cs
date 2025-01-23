using UnityEngine;
using static Gazeus.Mobile.Domino.AudioTheme;

namespace Gazeus.Mobile.Domino.Managers
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioTheme _audioTheme;

        private AudioSource _audioSource;

        #region Unity
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        #endregion

        public void PlayAudio(Audio audio)
        {
            AudioClip audioClip = _audioTheme.GetAudioClip(audio);

            _audioSource.PlayOneShot(audioClip);
        }
    }
}
