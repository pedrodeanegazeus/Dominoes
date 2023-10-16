using System;
using Dominoes.Components;
using Dominoes.Core.Enums;
using Dominoes.Core.Interfaces.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Dominoes.Controllers.Lobby
{
    internal class LobbyCanvasController : MonoBehaviour
    {
        public event Action<GameType> GameTypeSelected;

        [SerializeField] private GameObject _jogatinaLogo;
        [SerializeField] private GameObject _defaultLogoPrefab;

        [Header("Buttons")]
        [SerializeField] private Button _singlePlayerButton;
        [SerializeField] private Button _multiplayerButton;
        [SerializeField] private Button _playWithFriendsButton;
        [SerializeField] private GameObject _vipButton;

        [Header("Provider")]
        [SerializeField] private DominoesServiceProvider _serviceProvider;

        private IVipService _vipService;

        #region Localize Prefab Event
        public void OnUpdateAsset()
        {
            Destroy(_defaultLogoPrefab);
        }
        #endregion

        #region Unity
        private void Awake()
        {
            _singlePlayerButton.onClick.AddListener(() => GameTypeSelected?.Invoke(GameType.SinglePlayer));
            _multiplayerButton.onClick.AddListener(() => GameTypeSelected?.Invoke(GameType.Multiplayer));
            _playWithFriendsButton.onClick.AddListener(() => GameTypeSelected?.Invoke(GameType.PlayWithFriends));
            _vipService = _serviceProvider.GetRequiredService<IVipService>();
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
    }
}
