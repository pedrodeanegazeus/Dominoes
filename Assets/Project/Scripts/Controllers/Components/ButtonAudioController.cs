using Gazeus.CoreMobile.SDK.Core.Extensions;
using Gazeus.CoreMobile.SDK.Core.Interfaces;
using Gazeus.Mobile.Domino.Core.Models;
using Gazeus.Mobile.Domino.Infrastructure.Repositories;
using Gazeus.Mobile.Domino.Managers;
using UnityEngine;
using UnityEngine.UI;
using static Gazeus.Mobile.Domino.AudioTheme;

namespace Gazeus.Mobile.Domino
{
    [RequireComponent(typeof(Button))]
    public class ButtonAudioController : MonoBehaviour
    {
        [SerializeField] private Audio _audio;

        private Button _button;

        private IGzLogger<ButtonAudioController> _logger;
        private GameConfigRepository _gameConfigRepository;

        #region Unity
        private void Awake()
        {
            _button = GetComponent<Button>();

            _logger = GameManager.ServiceProviderManager.GetService<IGzLogger<ButtonAudioController>>();
            _gameConfigRepository = GameManager.ServiceProviderManager.GetService<GameConfigRepository>();
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }
        #endregion

        #region Events
        private void OnButtonClick()
        {
            _logger.LogMethodCall(nameof(OnButtonClick));

            GameConfig gameConfig = _gameConfigRepository.GetGameConfig();
            if (gameConfig.IsAudioOn)
            {
                GameManager.AudioManager.PlayAudio(_audio);
            }
        }
        #endregion
    }
}
