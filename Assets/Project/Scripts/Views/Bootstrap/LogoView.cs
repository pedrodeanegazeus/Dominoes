using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Gazeus.Mobile.Domino.Views
{
    public class LogoView : MonoBehaviour
    {
        public event Action AnimationCompleted;

        [SerializeField] private RectTransform _jTransform;
        [SerializeField] private float _jStartYPosition;
        [SerializeField] private float _jElasticYDelta;

        [Space]
        [SerializeField] private RectTransform _oTransform;
        [SerializeField] private float _oStartYPosition;
        [SerializeField] private float _oElasticYDelta;

        [Space]
        [SerializeField] private RectTransform _gTransform;
        [SerializeField] private float _gStartYPosition;
        [SerializeField] private float _gElasticYDelta;

        [Space]
        [SerializeField] private RectTransform _a1Transform;
        [SerializeField] private float _a1StartYPosition;
        [SerializeField] private float _a1ElasticYDelta;

        [Space]
        [SerializeField] private RectTransform _tTransform;
        [SerializeField] private float _tStartYPosition;
        [SerializeField] private float _tElasticYDelta;

        [Space]
        [SerializeField] private RectTransform _iTransform;
        [SerializeField] private float _iStartYPosition;
        [SerializeField] private float _iElasticYDelta;

        [Space]
        [SerializeField] private RectTransform _nTransform;
        [SerializeField] private float _nStartYPosition;
        [SerializeField] private float _nElasticYDelta;

        [Space]
        [SerializeField] private RectTransform _a2Transform;
        [SerializeField] private float _a2StartYPosition;
        [SerializeField] private float _a2ElasticYDelta;

        [Space]
        [SerializeField] private float _startDelayDuration;
        [SerializeField] private float _elasticDuration;

        private TaskCompletionSource<bool> _jAnimationTask;
        private TaskCompletionSource<bool> _oAnimationTask;
        private TaskCompletionSource<bool> _gAnimationTask;
        private TaskCompletionSource<bool> _a1AnimationTask;
        private TaskCompletionSource<bool> _tAnimationTask;
        private TaskCompletionSource<bool> _iAnimationTask;
        private TaskCompletionSource<bool> _nAnimationTask;
        private TaskCompletionSource<bool> _a2AnimationTask;

        #region Unity
        private void Awake()
        {
            _jAnimationTask = new TaskCompletionSource<bool>();
            _oAnimationTask = new TaskCompletionSource<bool>();
            _gAnimationTask = new TaskCompletionSource<bool>();
            _a1AnimationTask = new TaskCompletionSource<bool>();
            _tAnimationTask = new TaskCompletionSource<bool>();
            _iAnimationTask = new TaskCompletionSource<bool>();
            _nAnimationTask = new TaskCompletionSource<bool>();
            _a2AnimationTask = new TaskCompletionSource<bool>();
        }
        #endregion

        public void Animate()
        {
            Vector2 jEndPosition = _jTransform.localPosition;
            Vector2 jElasticPosition = new(jEndPosition.x, jEndPosition.y - _jElasticYDelta);
            _jTransform.localPosition = new(jEndPosition.x, _jStartYPosition);

            Vector2 oEndPosition = _oTransform.localPosition;
            Vector2 oElasticPosition = new(oEndPosition.x, oEndPosition.y - _oElasticYDelta);
            _oTransform.localPosition = new(oEndPosition.x, _oStartYPosition);

            Vector2 gEndPosition = _gTransform.localPosition;
            Vector2 gElasticPosition = new(gEndPosition.x, gEndPosition.y - _gElasticYDelta);
            _gTransform.localPosition = new(gEndPosition.x, _gStartYPosition);

            Vector2 a1EndPosition = _a1Transform.localPosition;
            Vector2 a1ElasticPosition = new(a1EndPosition.x, a1EndPosition.y - _a1ElasticYDelta);
            _a1Transform.localPosition = new(a1EndPosition.x, _a1StartYPosition);

            Vector2 tEndPosition = _tTransform.localPosition;
            Vector2 tElasticPosition = new(tEndPosition.x, tEndPosition.y - _tElasticYDelta);
            _tTransform.localPosition = new(tEndPosition.x, _tStartYPosition);

            Vector2 iEndPosition = _iTransform.localPosition;
            Vector2 iElasticPosition = new(iEndPosition.x, iEndPosition.y - _iElasticYDelta);
            _iTransform.localPosition = new(iEndPosition.x, _iStartYPosition);

            Vector2 nEndPosition = _nTransform.localPosition;
            Vector2 nElasticPosition = new(nEndPosition.x, nEndPosition.y - _nElasticYDelta);
            _nTransform.localPosition = new(nEndPosition.x, _nStartYPosition);

            Vector2 a2EndPosition = _a2Transform.localPosition;
            Vector2 a2ElasticPosition = new(a2EndPosition.x, a2EndPosition.y - _a2ElasticYDelta);
            _a2Transform.localPosition = new(a2EndPosition.x, _a2StartYPosition);

            _ = DOTween.Sequence()
                .AppendInterval(_startDelayDuration)
                .Append(_jTransform.DOLocalMove(jElasticPosition, _elasticDuration / 2).SetEase(Ease.OutQuint))
                .Append(_jTransform.DOLocalMove(jEndPosition, _elasticDuration / 2).SetEase(Ease.InOutQuad))
                .OnComplete(() => _jAnimationTask.SetResult(true));

            _ = DOTween.Sequence()
                .AppendInterval(_startDelayDuration)
                .Append(_oTransform.DOLocalMove(oElasticPosition, _elasticDuration / 2).SetEase(Ease.OutQuint))
                .Append(_oTransform.DOLocalMove(oEndPosition, _elasticDuration / 2).SetEase(Ease.InOutQuad))
                .OnComplete(() => _oAnimationTask.SetResult(true));

            _ = DOTween.Sequence()
                .AppendInterval(_startDelayDuration)
                .Append(_gTransform.DOLocalMove(gElasticPosition, _elasticDuration / 2).SetEase(Ease.OutQuint))
                .Append(_gTransform.DOLocalMove(gEndPosition, _elasticDuration / 2).SetEase(Ease.InOutQuad))
                .OnComplete(() => _gAnimationTask.SetResult(true));

            _ = DOTween.Sequence()
                .AppendInterval(_startDelayDuration)
                .Append(_a1Transform.DOLocalMove(a1ElasticPosition, _elasticDuration / 2).SetEase(Ease.OutQuint))
                .Append(_a1Transform.DOLocalMove(a1EndPosition, _elasticDuration / 2).SetEase(Ease.InOutQuad))
                .OnComplete(() => _a1AnimationTask.SetResult(true));

            _ = DOTween.Sequence()
                .AppendInterval(_startDelayDuration)
                .Append(_tTransform.DOLocalMove(tElasticPosition, _elasticDuration / 2).SetEase(Ease.OutQuint))
                .Append(_tTransform.DOLocalMove(tEndPosition, _elasticDuration / 2).SetEase(Ease.InOutQuad))
                .OnComplete(() => _tAnimationTask.SetResult(true));

            _ = DOTween.Sequence()
                .AppendInterval(_startDelayDuration)
                .Append(_iTransform.DOLocalMove(iElasticPosition, _elasticDuration / 2).SetEase(Ease.OutQuint))
                .Append(_iTransform.DOLocalMove(iEndPosition, _elasticDuration / 2).SetEase(Ease.InOutQuad))
                .OnComplete(() => _iAnimationTask.SetResult(true));

            _ = DOTween.Sequence()
                .AppendInterval(_startDelayDuration)
                .Append(_nTransform.DOLocalMove(nElasticPosition, _elasticDuration / 2).SetEase(Ease.OutQuint))
                .Append(_nTransform.DOLocalMove(nEndPosition, _elasticDuration / 2).SetEase(Ease.InOutQuad))
                .OnComplete(() => _nAnimationTask.SetResult(true));

            _ = DOTween.Sequence()
                .AppendInterval(_startDelayDuration)
                .Append(_a2Transform.DOLocalMove(a2ElasticPosition, _elasticDuration / 2).SetEase(Ease.OutQuint))
                .Append(_a2Transform.DOLocalMove(a2EndPosition, _elasticDuration / 2).SetEase(Ease.InOutQuad))
                .OnComplete(() => _a2AnimationTask.SetResult(true));

            _ = Task
                .WhenAll(_jAnimationTask.Task,
                         _oAnimationTask.Task,
                         _gAnimationTask.Task,
                         _a1AnimationTask.Task,
                         _tAnimationTask.Task,
                         _iAnimationTask.Task,
                         _nAnimationTask.Task,
                         _a2AnimationTask.Task)
                .ContinueWith(task => AnimationCompleted?.Invoke());
        }
    }
}
