using DG.Tweening;
using Dominoes.Controllers;
using Dominoes.Core.Enums;
using Gazeus.CoreMobile.Commons.Core.Extensions;
using Gazeus.CoreMobile.Commons.Core.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dominoes.Managers
{
    internal class GameSceneManager : MonoBehaviour
    {
        [SerializeField] private TransitionController _transitionController;

        private IGzLogger<GameSceneManager> _logger;
        private DominoesScene _dominoesScene;

        #region Unity
        private void Awake()
        {
            _transitionController.Completed += TransitionController_Completed;
        }
        #endregion

        public void Initialize(IGzServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<IGzLogger<GameSceneManager>>();
            _transitionController.Initialize(serviceProvider);
        }

        public void LoadScene(DominoesScene scene, bool useTransition = true)
        {
            _logger.Debug("CALLED: {method} - {scene} - {useTransition}",
                          nameof(LoadScene),
                          scene.ToString(),
                          useTransition);

            _ = DOTween.KillAll();
            _dominoesScene = scene;
            if (useTransition)
            {
                _transitionController.StartTransition();
            }
            else
            {
                LoadScene(_dominoesScene.ToString());
            }
        }

        #region Events
        private void LoadScene_Completed(AsyncOperation operation)
        {
            _logger.Info("Scene {scene} loaded", _dominoesScene.ToString());

            operation.completed -= LoadScene_Completed;
            _transitionController.FinishTransition();
        }

        private void TransitionController_Completed()
        {
            LoadScene(_dominoesScene.ToString());
        }
        #endregion

        private void LoadScene(string scene)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene);
            asyncOperation.completed += LoadScene_Completed;
        }
    }
}
