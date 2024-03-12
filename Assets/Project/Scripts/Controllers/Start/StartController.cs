using System.Collections;
using System.Threading.Tasks;
using DG.Tweening;
using Dominoes.Core.Enums;
using Dominoes.Core.Extensions;
using Dominoes.Core.Interfaces.Services;
using Dominoes.Managers;
using Dominoes.ScriptableObjects;
using Dominoes.Views.Start;
using Gazeus.CoreMobile.Commons;
using Gazeus.CoreMobile.Commons.Core.Extensions;
using Gazeus.CoreMobile.Commons.ScriptableObjects;
using Gazeus.CoreMobile.SDK.Core.Interfaces;
using Gazeus.CoreMobile.SDK.ScriptableObjects;
using UnityEngine;

namespace Dominoes.Controllers
{
    internal class StartController : MonoBehaviour
    {
        [Header("DEBUG")]
        [SerializeField] private GameState _gameState;

        [Space]
        [SerializeField] private GameManager _gameManager;

        [Header("Configurations")]
        [SerializeField] private LogConfiguration _logConfiguration;

        [Header("Gazeus SDK")]
        [SerializeField] private AppConfiguration _appConfiguration;
        [SerializeField] private AppEnvironment _appEnvironment;

        [Header("Views")]
        [SerializeField] private JogatinaView _jogatinaView;
        [SerializeField] private LoadingView _loadingView;

        #region Unity
        private void Start()
        {
            _ = StartCoroutine(StartGame());
        }
        #endregion

        private IEnumerator StartGame()
        {
            // statics
            GzLogger.Initialize(_logConfiguration);
            DOTween.Init();

            // objects
            _gameManager.Initialize();

            // views
            _jogatinaView.Initialize();
            _loadingView.Initialize();
            _loadingView.Animate();
            Task animationTask = _jogatinaView.AnimateAsync();

            // singletons
            IGazeusSDK gazeusSDK = GameManager.ServiceProvider.GetRequiredService<IGazeusSDK>();
            gazeusSDK.Initialize(_appConfiguration, _appEnvironment);

            IProfileService profileService = GameManager.ServiceProvider.GetRequiredService<IProfileService>();
            profileService.Initialize();

            IVipService vipService = GameManager.ServiceProvider.GetRequiredService<IVipService>();
            vipService.Initialize();

            yield return Task
                .WhenAll(animationTask)
                .WaitTask();

            GameManager.Scene.LoadScene(DominoesScene.Lobby, false);
        }
    }
}
