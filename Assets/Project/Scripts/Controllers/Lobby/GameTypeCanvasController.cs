using System;
using System.Collections;
using System.Threading.Tasks;
using Dominoes.Components;
using Dominoes.Core.Enums;
using Dominoes.Core.Interfaces.Services;
using Dominoes.Core.Models.Services.GazeusServicesService;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using UnityEngine.UI;

namespace Dominoes.Controllers.Lobby
{
    internal class GameTypeCanvasController : MonoBehaviour
    {
        [SerializeField] private GameObject _jogatinaLogo;

        [Header("Titles")]
        [SerializeField] private GameObject _singlePlayerTitle;
        [SerializeField] private GameObject _multiplayerTitle;
        [SerializeField] private GameObject _playWithFriendsTitle;

        [Header("Buttons")]
        [SerializeField] private Button _drawButton;
        [SerializeField] private Button _blockButton;
        [SerializeField] private Button _allFivesButton;
        [SerializeField] private Button _turboButton;
        [SerializeField] private GameObject _promotional;
        [SerializeField] private GameObject _vipButton;

        [Header("Texts")]
        [SerializeField] private LocalizeStringEvent _drawPlayersOnlineText;
        [SerializeField] private LocalizeStringEvent _blockPlayersOnlineText;
        [SerializeField] private LocalizeStringEvent _allFivesPlayersOnlineText;
        [SerializeField] private LocalizeStringEvent _turboPlayersOnlineText;

        [Header("Provider")]
        [SerializeField] private DominoesServiceProvider _serviceProvider;

        private IGazeusServicesService _gazeusServicesService;
        private IVipService _vipService;

        public void SetGameType(GameType gameType)
        {
            switch (gameType)
            {
                case GameType.Multiplayer:
                    SetMultiplayerGameType();
                    break;
                case GameType.PlayWithFriends:
                    SetPlayWithFriendsGameType();
                    break;
                case GameType.SinglePlayer:
                    SetSinglePlayerGameType();
                    break;
            }
        }

        #region Unity
        private void Awake()
        {
            _gazeusServicesService = _serviceProvider.GetRequiredService<IGazeusServicesService>();
            _vipService = _serviceProvider.GetRequiredService<IVipService>();
        }

        private void Start()
        {
            SetVip(_vipService.IsVip);
        }
        #endregion

        private void SetMultiplayerGameType()
        {
            _multiplayerTitle.SetActive(true);
            _playWithFriendsTitle.SetActive(false);
            _singlePlayerTitle.SetActive(false);

            _allFivesPlayersOnlineText.gameObject.SetActive(true);
            _blockPlayersOnlineText.gameObject.SetActive(true);
            _drawPlayersOnlineText.gameObject.SetActive(true);
            _turboPlayersOnlineText.gameObject.SetActive(true);

            Task<PlayersOnline> playersOnlineTask = _gazeusServicesService.GetPlayersOnlineAsync();
            _ = StartCoroutine(WaitForTaskCompleteRoutine(playersOnlineTask, task =>
            {
                PlayersOnline playersOnline = (task as Task<PlayersOnline>).Result;
                (_allFivesPlayersOnlineText.StringReference["count"] as IntVariable).Value = playersOnline.AllFives;
                (_blockPlayersOnlineText.StringReference["count"] as IntVariable).Value = playersOnline.Block;
                (_drawPlayersOnlineText.StringReference["count"] as IntVariable).Value = playersOnline.Draw;
                (_turboPlayersOnlineText.StringReference["count"] as IntVariable).Value = playersOnline.Turbo;
            }));
        }

        private void SetPlayWithFriendsGameType()
        {
            _multiplayerTitle.SetActive(false);
            _playWithFriendsTitle.SetActive(true);
            _singlePlayerTitle.SetActive(false);

            _allFivesPlayersOnlineText.gameObject.SetActive(false);
            _blockPlayersOnlineText.gameObject.SetActive(false);
            _drawPlayersOnlineText.gameObject.SetActive(false);
            _turboPlayersOnlineText.gameObject.SetActive(false);
        }

        private void SetSinglePlayerGameType()
        {
            _multiplayerTitle.SetActive(false);
            _playWithFriendsTitle.SetActive(false);
            _singlePlayerTitle.SetActive(true);

            _allFivesPlayersOnlineText.gameObject.SetActive(false);
            _blockPlayersOnlineText.gameObject.SetActive(false);
            _drawPlayersOnlineText.gameObject.SetActive(false);
            _turboPlayersOnlineText.gameObject.SetActive(false);
        }

        private void SetVip(bool isVip)
        {
            _jogatinaLogo.SetActive(isVip);
            _promotional.SetActive(!isVip);
            _vipButton.SetActive(!isVip);
        }

        private IEnumerator WaitForTaskCompleteRoutine(Task task, Action<Task> taskCompleted)
        {
            while (!task.IsCompleted)
            {
                yield return null;
            }
            taskCompleted?.Invoke(task);
        }
    }
}
