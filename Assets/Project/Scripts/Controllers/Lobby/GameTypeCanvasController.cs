using System;
using Dominoes.Components;
using Dominoes.Core.Enums;
using Dominoes.Core.Interfaces.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Dominoes.Controllers.Lobby
{
    internal class GameTypeCanvasController : MonoBehaviour
    {
        public event Action BackButtonClicked;

        [SerializeField] private GameObject _jogatinaLogo;

        [Space]
        [SerializeField] private Button _backButton;
        [SerializeField] private GameObject _singlePlayerTitle;
        [SerializeField] private GameObject _multiplayerTitle;
        [SerializeField] private GameObject _playWithFriendsTitle;
        [SerializeField] private Button _drawButton;
        [SerializeField] private Button _blockButton;
        [SerializeField] private Button _allFivesButton;
        [SerializeField] private Button _turboButton;
        [SerializeField] private GameObject _promotional;
        [SerializeField] private GameObject _vipButton;

        [Space]
        [SerializeField] private DominoesServiceProvider _serviceProvider;

        private IVipService VipService { get; set; }

        public void SetGameType(GameType gameType)
        {
            switch (gameType)
            {
                case GameType.Multiplayer:
                    _singlePlayerTitle.SetActive(false);
                    _multiplayerTitle.SetActive(true);
                    _playWithFriendsTitle.SetActive(false);
                    break;
                case GameType.PlayWithFriends:
                    _singlePlayerTitle.SetActive(false);
                    _multiplayerTitle.SetActive(false);
                    _playWithFriendsTitle.SetActive(true);
                    break;
                case GameType.SinglePlayer:
                    _singlePlayerTitle.SetActive(true);
                    _multiplayerTitle.SetActive(false);
                    _playWithFriendsTitle.SetActive(false);
                    break;
            }
        }

        #region Unity
        private void Awake()
        {
            _backButton.onClick.AddListener(() => BackButtonClicked?.Invoke());
            VipService = _serviceProvider.GetRequiredService<IVipService>();
        }

        private void Start()
        {
            SetVip(VipService.IsVip);
        }
        #endregion

        private void SetVip(bool isVip)
        {
            _jogatinaLogo.SetActive(isVip);
            _promotional.SetActive(!isVip);
            _vipButton.SetActive(!isVip);
        }
    }
}
