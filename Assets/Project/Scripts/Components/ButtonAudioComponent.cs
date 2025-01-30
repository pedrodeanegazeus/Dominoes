using Gazeus.Mobile.Domino.Core.Models;
using Gazeus.Mobile.Domino.Infrastructure.Repositories;
using Gazeus.Mobile.Domino.Managers;
using UnityEngine;
using UnityEngine.UI;
using static Gazeus.Mobile.Domino.AudioTheme;

namespace Gazeus.Mobile.Domino.Components
{
    [RequireComponent(typeof(Button))]
    public class ButtonAudioComponent : MonoBehaviour
    {
        [SerializeField] private Audio _audio;

        private Button _button;

        private GameStateRepository _gameStateRepository;

        #region Unity
        private void Awake()
        {
            _button = GetComponent<Button>();

            _gameStateRepository = GameManager.ServiceProviderManager.GetService<GameStateRepository>();
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
            GameState gameState = _gameStateRepository.GetGameState();
            if (gameState.IsAudioOn)
            {
                GameManager.AudioManager.PlayAudio(_audio);
            }
        }
        #endregion
    }
}
