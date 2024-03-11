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
using UnityEngine;

namespace Dominoes.Controllers
{
    internal class StartController : MonoBehaviour
    {
        [Header("DEGUBBER")]
        [SerializeField] private GameState _gameState;

        [Space]
        [SerializeField] private GameManager _gameManager;

        [Header("Configurations")]
        [SerializeField] private LogConfiguration _logConfiguration;

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

            // singletons
            _gameManager.Initialize();

            // views
            _jogatinaView.Initialize();
            _loadingView.Initialize();

            Task animationTask = _jogatinaView.AnimateAsync();
            _loadingView.Animate();


            // maybe will drop
            _gameState.Initialize();
            _gameState.Load();


            // singleton services
            IProfileService profileService = GameManager.ServiceProvider.GetRequiredService<IProfileService>();
            IVipService vipService = GameManager.ServiceProvider.GetRequiredService<IVipService>();
            Task profileServiceInitializeTask = Task.Run(() => profileService.Initialize());
            Task vipServiceInitializeTask = Task.Run(() => vipService.Initialize());

            yield return Task
                .WhenAll(animationTask,
                         profileServiceInitializeTask,
                         vipServiceInitializeTask)
                .WaitTask();

            GameManager.Scene.LoadScene(DominoesScene.Lobby, false);
        }
    }
}
