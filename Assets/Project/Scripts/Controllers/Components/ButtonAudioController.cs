using Gazeus.Mobile.Domino.Core.Models;
using Gazeus.Mobile.Domino.Infrastructure.Repositories;
using Gazeus.Mobile.Domino.Managers;
using UnityEngine;
using UnityEngine.UI;
using static Gazeus.Mobile.Domino.AudioTheme;

namespace Gazeus.Mobile.Domino.Controllers.Components
{
    [RequireComponent(typeof(Button))]
    public class ButtonAudioController : MonoBehaviour
    {
        [SerializeField] private Audio _audio;

        private Button _button;

        private GameConfigRepository _gameConfigRepository;

        #region Unity
        private void Awake()
        {
            _button = GetComponent<Button>();

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
            GameConfig gameConfig = _gameConfigRepository.GetGameConfig();
            if (gameConfig.IsAudioOn)
            {
                GameManager.AudioManager.PlayAudio(_audio);
            }
        }
        #endregion
    }
}
