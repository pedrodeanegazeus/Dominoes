using Dominoes.Core.Enums;
using Dominoes.Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Dominoes.Components
{
    internal class ButtonAudio : UIBehaviour
    {
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private Button _button;
        [SerializeField] private Audio _audio;

        #region Unity
        protected override void Awake()
        {
            _button.onClick.AddListener(PlayAudio);
        }

        protected override void Reset()
        {
            _audioManager = FindObjectOfType<AudioManager>();
            _button = GetComponent<Button>();
        }
        #endregion

        private void PlayAudio()
        {
            _audioManager.Play(_audio);
        }
    }
}
