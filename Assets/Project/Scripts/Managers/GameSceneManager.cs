using DG.Tweening;
using Dominoes.Controllers;
using Dominoes.Core.Enums;
using Gazeus.CoreMobile.Commons.Core.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dominoes.Managers
{
    internal class GameSceneManager : MonoBehaviour
    {
        public static GameSceneManager Instance { get; private set; }

        [SerializeField] private AnimationController _animationController;
        [SerializeField] private Animator _animator;

        private readonly int _hideTriggerHash = Animator.StringToHash("Hide");
        private readonly int _showTriggerHash = Animator.StringToHash("Show");

        private IGzLogger<GameSceneManager> _logger;
        private DominoesScene _dominoesScene;
        private bool _useTransition;

        public void Initialize()
        {
            _animationController.Initialize();
            _logger = ServiceProvider.GetRequiredService<IGzLogger<GameSceneManager>>();
        }

        public void LoadScene(DominoesScene scene, bool useTransition = true)
        {
            _logger.Debug("CALLED: {method} - {scene}",
                          nameof(LoadScene),
                          scene.ToString());

            _ = DOTween.KillAll();
            _dominoesScene = scene;
            _useTransition = useTransition;
            if (_useTransition)
            {
                _animator.SetTrigger(_showTriggerHash);
            }
            else
            {
                DoLoadScene(_dominoesScene.ToString());
            }
        }

        #region Unity
        private void Awake()
        {
            Instance = this;

            _animationController.EventFired += AnimationController_EventFired;

            DontDestroyOnLoad(gameObject);
        }

        private void OnDestroy()
        {
            _animationController.EventFired -= AnimationController_EventFired;
        }
        #endregion

        #region Events
        private void AnimationController_EventFired(string @event)
        {
            if (@event == "Transition_End" && _useTransition)
            {
                DoLoadScene(_dominoesScene.ToString());
            }
        }

        private void LoadScene_Completed(AsyncOperation operation)
        {
            _logger.Info("Scene {scene} loaded", _dominoesScene.ToString());

            operation.completed -= LoadScene_Completed;
            if (_useTransition)
            {
                _useTransition = false;
                _animator.SetTrigger(_hideTriggerHash);
            }
        }
        #endregion

        private void DoLoadScene(string scene)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene);
            asyncOperation.completed += LoadScene_Completed;
        }
    }
}
