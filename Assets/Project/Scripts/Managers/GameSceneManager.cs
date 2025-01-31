using System.Collections.Generic;
using DG.Tweening;
using Gazeus.CoreMobile.SDK.Core.Extensions;
using Gazeus.CoreMobile.SDK.Core.Interfaces;
using Gazeus.Mobile.Domino.Core.Enum;
using Gazeus.Mobile.Domino.Views;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gazeus.Mobile.Domino.Managers
{
    public class GameSceneManager : MonoBehaviour
    {
        [SerializeField] private TransitionView _transitionView;

        private IGzLogger<GameSceneManager> _logger;
        private Dictionary<GameScene, object> _parameters;
        private GameScene _targetScene;
        private bool _withTransition;

        #region Unity
        private void OnDisable()
        {
            _transitionView.InAnimationCompleted -= TransitionView_InAnimationCompleted;
        }

        private void OnEnable()
        {
            _transitionView.InAnimationCompleted += TransitionView_InAnimationCompleted;
        }
        #endregion

        public TParam GetParameter<TParam>()
        {
            TParam param = default;
            if (_parameters.TryGetValue(_targetScene, out object value))
            {
                param = (TParam)value;
                _parameters.Remove(_targetScene);
            }

            return param;
        }

        public void Initialize()
        {
            _transitionView.Initialize();

            _parameters = new Dictionary<GameScene, object>();
            _targetScene = GameScene.Bootstrap;

            _logger = GameManager.ServiceProviderManager.GetService<IGzLogger<GameSceneManager>>();
            _logger.Info("Initialized");
        }

        public void LoadScene(GameScene gameScene, bool useTransition = true)
        {
            _logger.LogMethodCall(nameof(LoadScene),
                                  gameScene,
                                  useTransition);

            _ = DOTween.KillAll();

            _targetScene = gameScene;
            _withTransition = useTransition;

            _logger.Info($"Loading {gameScene}");

            if (useTransition)
            {
                _transitionView.AnimateIn();
            }
            else
            {
                AsyncOperation asyncOperation = SceneManager.LoadSceneAsync((int)gameScene);
                asyncOperation.completed += LoadScene_Completed;
            }
        }

        public void LoadSceneWithParameter<TParam>(GameScene gameScene, TParam param, bool useTransition = true)
        {
            _logger.LogMethodCall(nameof(LoadSceneWithParameter),
                                  gameScene,
                                  param);

            _parameters[gameScene] = param;

            LoadScene(gameScene, useTransition);
        }

        #region Events
        private void LoadScene_Completed(AsyncOperation operation)
        {
            operation.completed -= LoadScene_Completed;

            _logger.Info($"Scene {_targetScene} loaded");

            if (_withTransition)
            {
                _transitionView.AnimateOut();
            }
        }

        private void TransitionView_InAnimationCompleted()
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync((int)_targetScene);
            asyncOperation.completed += LoadScene_Completed;
        }
        #endregion
    }
}
