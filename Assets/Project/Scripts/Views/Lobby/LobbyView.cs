using System;
using Dominoes.Core.Enums;
using Dominoes.Core.Interfaces.Services;
using Dominoes.Managers;
using Gazeus.CoreMobile.Commons.Core.Extensions;
using Gazeus.CoreMobile.Commons.Core.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Dominoes.Views.Lobby
{
    internal class LobbyView : MonoBehaviour
    {
        public event Action<GameType> GameTypeSelected;

        [SerializeField] private GameObject _jogatinaLogo;
        [SerializeField] private GameObject _defaultLogoPrefab;

        [Header("Buttons")]
        [SerializeField] private Button _singlePlayerButton;
        [SerializeField] private Button _multiplayerButton;
        [SerializeField] private Button _playWithFriendsButton;
        [SerializeField] private GameObject _vipButton;

        private IGzLogger<LobbyView> _logger;
        private IVipService _vipService;

        #region Unity
        private void Awake()
        {
            _multiplayerButton.onClick.AddListener(() => GameTypeSelected?.Invoke(GameType.Multiplayer));
            _playWithFriendsButton.onClick.AddListener(() => GameTypeSelected?.Invoke(GameType.PlayWithFriends));
            _singlePlayerButton.onClick.AddListener(() => GameTypeSelected?.Invoke(GameType.SinglePlayer));
        }

        private void OnDestroy()
        {
            _multiplayerButton.onClick.RemoveAllListeners();
            _playWithFriendsButton.onClick.RemoveAllListeners();
            _singlePlayerButton.onClick.RemoveAllListeners();
        }

        private void Start()
        {
            Destroy(_defaultLogoPrefab);
            SetVip(_vipService.IsVip);
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
            _logger = GameManager.ServiceProvider.GetRequiredService<IGzLogger<LobbyView>>();
            _vipService = GameManager.ServiceProvider.GetRequiredService<IVipService>();
        }

        public void Show()
        {
            _logger.Debug("CALLED: {method}",
                          nameof(Show));

            gameObject.SetActive(true);
        }

        private void SetVip(bool isVip)
        {
            _jogatinaLogo.SetActive(isVip);
            _vipButton.SetActive(!isVip);
        }
    }
}
