using Dominoes.Components;
using Dominoes.Core.Enums;
using Dominoes.Core.Interfaces.Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Dominoes.Controllers
{
    internal class LobbyController : MonoBehaviour
    {
        [Header("Canvas")]
        [SerializeField] private Canvas _lobbyCanvas;
        [SerializeField] private Canvas _gameTypeCanvas;

        [Header("Lobby")]
        [SerializeField] private Button _singlePlayerButton;
        [SerializeField] private Button _multiplayerButton;
        [SerializeField] private Button _playWithFriendsButton;
        [SerializeField] private GameObject _defaultLogoPrefab;
        [SerializeField] private GameObject _vipButton;

        [Header("Game Type Lobby")]
        [SerializeField] private Button _backButton;

        [Space]
        [SerializeField] private DominoesServiceProvider _serviceProvider;

        private IVipService VipService { get; set; }

        #region Localize Prefab Event
        public void OnUpdateAsset()
        {
            Destroy(_defaultLogoPrefab);
        }
        #endregion

        #region Unity
        private void Awake()
        {
            // debug helper
            if (!DominoesServiceProvider.IsBuilt)
            {
                _ = SceneManager.LoadSceneAsync(nameof(DominoesScene.Start));
                return;
            }

            _backButton.onClick.AddListener(BackButtonClicked);
            _singlePlayerButton.onClick.AddListener(SinglePlayerButtonClicked);
            VipService = _serviceProvider.GetRequiredService<IVipService>();
        }

        private void Start()
        {
            _vipButton.SetActive(!VipService.IsVip);
        }
        #endregion

        private void BackButtonClicked()
        {
            _lobbyCanvas.gameObject.SetActive(true);
            _gameTypeCanvas.gameObject.SetActive(false);
        }

        private void SinglePlayerButtonClicked()
        {
            _lobbyCanvas.gameObject.SetActive(false);
            _gameTypeCanvas.gameObject.SetActive(true);
        }
    }
}
