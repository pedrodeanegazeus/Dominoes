using System;
using Dominoes.Core.Enums;
using Dominoes.Core.Interfaces.Services;
using Dominoes.Managers;
using Dominoes.ScriptableObjects;
using Gazeus.CoreMobile.Commons.Core.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Dominoes.Views.Lobby
{
    internal class LobbyView : MonoBehaviour
    {
        public event Action GameTypeSelected;

        [SerializeField] private GameState _gameState;

        [Space]
        [SerializeField] private GameObject _jogatinaLogo;
        [SerializeField] private GameObject _defaultLogoPrefab;

        [Header("Buttons")]
        [SerializeField] private Button _singlePlayerButton;
        [SerializeField] private Button _multiplayerButton;
        [SerializeField] private Button _playWithFriendsButton;
        [SerializeField] private GameObject _vipButton;

        private IGzLogger<LobbyView> _logger;
        private IVipService _vipService;

        #region Localize Prefab Event
        public void OnUpdateAsset()
        {
            _logger.Debug("CALLED: {method}",
                          nameof(OnUpdateAsset));

            Destroy(_defaultLogoPrefab);
        }
        #endregion

        public void Hide()
        {
            _logger.Debug("CALLED: {method}",
                          nameof(Hide));

            gameObject.SetActive(false);
        }

        public void Initialize()
        {
            _logger = ServiceProviderManager.Instance.GetRequiredService<IGzLogger<LobbyView>>();
            _vipService = ServiceProviderManager.Instance.GetRequiredService<IVipService>();
        }

        public void Show()
        {
            _logger.Debug("CALLED: {method}",
                          nameof(Show));

            gameObject.SetActive(true);
        }

        #region Unity
        private void Awake()
        {
            _multiplayerButton.onClick.AddListener(() => TriggerGameTypeSelected(GameType.Multiplayer));
            _playWithFriendsButton.onClick.AddListener(() => TriggerGameTypeSelected(GameType.PlayWithFriends));
            _singlePlayerButton.onClick.AddListener(() => TriggerGameTypeSelected(GameType.SinglePlayer));
        }

        private void OnDestroy()
        {
            _multiplayerButton.onClick.RemoveAllListeners();
            _playWithFriendsButton.onClick.RemoveAllListeners();
            _singlePlayerButton.onClick.RemoveAllListeners();
        }

        private void Start()
        {
            SetVip(_vipService.IsVip);
        }
        #endregion

        private void SetVip(bool isVip)
        {
            _jogatinaLogo.SetActive(isVip);
            _vipButton.SetActive(!isVip);
        }

        private void TriggerGameTypeSelected(GameType gameType)
        {
            _gameState.GameType = gameType;
            GameTypeSelected?.Invoke();
        }
    }
}
