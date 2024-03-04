using DG.Tweening;
using Dominoes.Core.Enums;
using Gazeus.CoreMobile.Commons.Core.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Dominoes.Managers
{
    internal class GameSceneManager : MonoBehaviour
    {
        public static GameSceneManager Instance { get; private set; }

        [SerializeField] private Image _transitionTilesAlpha;
        [SerializeField] private Image _transitionTilesTopAlpha;
        [SerializeField] private RectTransform _transitionTilesTransform;

        private IGzLogger<GameSceneManager> _logger;
        private DominoesScene _dominoesScene;

        public void Initialize()
        {
            _logger = ServiceProviderManager.Instance.GetRequiredService<IGzLogger<GameSceneManager>>();
        }

        public void LoadScene(DominoesScene scene, bool useTransition = true)
        {
            _logger.Debug("CALLED: {method} - {scene}",
                          nameof(LoadScene),
                          scene.ToString());

            _ = DOTween.KillAll();
            _dominoesScene = scene;
            if (useTransition)
            {
                _ = _transitionTilesAlpha.DOColor(Color.white, 0.6f);
                _ = _transitionTilesTopAlpha.DOColor(Color.white, 0.6f);
                _ = DOTween.Sequence()
                    .Append(_transitionTilesTransform.DOAnchorMax(new Vector2(1f, 0.16f), 0f))
                    .AppendInterval(0.1f)
                    .Append(_transitionTilesTransform.DOAnchorMax(new Vector2(1f, 0.32f), 0f))
                    .AppendInterval(0.1f)
                    .Append(_transitionTilesTransform.DOAnchorMax(new Vector2(1f, 0.48f), 0f))
                    .AppendInterval(0.1f)
                    .Append(_transitionTilesTransform.DOAnchorMax(new Vector2(1f, 0.64f), 0f))
                    .AppendInterval(0.1f)
                    .Append(_transitionTilesTransform.DOAnchorMax(new Vector2(1f, 0.80f), 0f))
                    .AppendInterval(0.1f)
                    .Append(_transitionTilesTransform.DOAnchorMax(new Vector2(1f, 0.96f), 0f))
                    .AppendInterval(0.1f)
                    .Append(_transitionTilesTransform.DOAnchorMax(new Vector2(1f, 1f), 0f))
                    .OnComplete(() => DoLoadScene(_dominoesScene.ToString()));
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

            DontDestroyOnLoad(gameObject);
        }
        #endregion

        #region Events
        private void LoadScene_Completed(AsyncOperation operation)
        {
            _logger.Info("Scene {scene} loaded", _dominoesScene.ToString());

            operation.completed -= LoadScene_Completed;

            Color transparent = new(1f, 1f, 1f, 0f);
            _ = _transitionTilesAlpha.DOColor(transparent, 0.6f);
            _ = _transitionTilesTopAlpha.DOColor(transparent, 0.6f);
            _ = DOTween.Sequence()
                .AppendInterval(0.5f)
                .Append(_transitionTilesTransform.DOAnchorMax(new Vector2(1f, 0.96f), 0f))
                .AppendInterval(0.1f)
                .Append(_transitionTilesTransform.DOAnchorMax(new Vector2(1f, 0.80f), 0f))
                .AppendInterval(0.1f)
                .Append(_transitionTilesTransform.DOAnchorMax(new Vector2(1f, 0.64f), 0f))
                .AppendInterval(0.1f)
                .Append(_transitionTilesTransform.DOAnchorMax(new Vector2(1f, 0.48f), 0f))
                .AppendInterval(0.1f)
                .Append(_transitionTilesTransform.DOAnchorMax(new Vector2(1f, 0.32f), 0f))
                .AppendInterval(0.1f)
                .Append(_transitionTilesTransform.DOAnchorMax(new Vector2(1f, 0.16f), 0f))
                .AppendInterval(0.1f)
                .Append(_transitionTilesTransform.DOAnchorMax(new Vector2(1f, 0f), 0f));
        }
        #endregion

        private void DoLoadScene(string scene)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene);
            asyncOperation.completed += LoadScene_Completed;
        }
    }
}
