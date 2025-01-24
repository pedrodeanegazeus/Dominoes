using System.Collections.Generic;
using DG.Tweening;
using Gazeus.CoreMobile.SDK.Core.Extensions;
using Gazeus.CoreMobile.SDK.Core.Interfaces;
using Gazeus.Mobile.Domino.Core.Enum;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gazeus.Mobile.Domino.Managers
{
    public class GameSceneManager : MonoBehaviour
    {
        private IGzLogger<GameSceneManager> _logger;
        private Dictionary<GameScene, object> _parameters;
        private GameScene _currentScene;

        public TParam GetParameter<TParam>()
        {
            TParam param = default;
            if (_parameters.TryGetValue(_currentScene, out object value))
            {
                param = (TParam)value;
                _parameters.Remove(_currentScene);
            }

            return param;
        }

        public void Initialize()
        {
            _parameters = new Dictionary<GameScene, object>();
            _currentScene = GameScene.Bootstrap;

            _logger = GameManager.ServiceProviderManager.GetService<IGzLogger<GameSceneManager>>();
            _logger.Info("Initialized");
        }

        public void LoadScene(GameScene gameScene)
        {
            _logger.LogMethodCall(nameof(LoadScene),
                                  gameScene);

            _logger.Info($"Loading {gameScene}");

            _ = DOTween.KillAll();

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync((int)gameScene);
            asyncOperation.completed += LoadScene_Completed;

            _currentScene = gameScene;
        }

        public void LoadSceneWithParameter<TParam>(GameScene gameScene, TParam param)
        {
            _logger.LogMethodCall(nameof(LoadSceneWithParameter),
                                  gameScene,
                                  param);

            _parameters[gameScene] = param;

            LoadScene(gameScene);
        }

        #region Events
        private void LoadScene_Completed(AsyncOperation operation)
        {
            operation.completed -= LoadScene_Completed;

            _logger.Info($"Scene {_currentScene} loaded");
        }
        #endregion
    }
}
