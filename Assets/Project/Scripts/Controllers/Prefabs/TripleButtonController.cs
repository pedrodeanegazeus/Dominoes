using System;
using DG.Tweening;
using Dominoes.Managers;
using Gazeus.CoreMobile.Commons.Core.Extensions;
using Gazeus.CoreMobile.Commons.Core.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Dominoes.Controllers.Prefabs
{
    internal class TripleButtonController : MonoBehaviour
    {
        public event Action<int> Clicked;

        [SerializeField] private Button _button1;
        [SerializeField] private Button _button2;
        [SerializeField] private Button _button3;

        [Space]
        [SerializeField] private RectTransform _highlight;
        [SerializeField] private float _highlightMoveSpeed = 0.25f;

        private IGzLogger<TripleButtonController> _logger;
        private bool _isEnabled;

        #region Unity
        private void Awake()
        {
            _logger = GameManager.ServiceProvider.GetRequiredService<IGzLogger<TripleButtonController>>();

            _button1.onClick.AddListener(Button1Clicked);
            _button2.onClick.AddListener(Button2Clicked);
            _button3.onClick.AddListener(Button3Clicked);
            _isEnabled = true;
        }

        private void OnDestroy()
        {
            _button1.onClick.RemoveAllListeners();
            _button2.onClick.RemoveAllListeners();
            _button3.onClick.RemoveAllListeners();
        }
        #endregion

        public void SetState(int state)
        {
            _logger.Debug("CALLED: {method} - {state}",
                          nameof(SetState),
                          state);

            Vector3 position = state switch
            {
                1 => _button1.gameObject.transform.position,
                2 => _button2.gameObject.transform.position,
                3 => _button3.gameObject.transform.position,
                _ => throw new ArgumentOutOfRangeException(nameof(state)),
            };
            _highlight.position = position;
        }

        private void Button1Clicked()
        {
            if (_isEnabled)
            {
                _isEnabled = false;
                _highlight
                    .DOMove(_button1.gameObject.transform.position, _highlightMoveSpeed)
                    .OnComplete(() => _isEnabled = true);
                Clicked?.Invoke(1);
            }
        }

        private void Button2Clicked()
        {
            if (_isEnabled)
            {
                _isEnabled = false;
                _highlight
                    .DOMove(_button2.gameObject.transform.position, _highlightMoveSpeed)
                    .OnComplete(() => _isEnabled = true);
                Clicked?.Invoke(2);
            }
        }

        private void Button3Clicked()
        {
            if (_isEnabled)
            {
                _isEnabled = false;
                _highlight
                    .DOMove(_button3.gameObject.transform.position, _highlightMoveSpeed)
                    .OnComplete(() => _isEnabled = true);
                Clicked?.Invoke(3);
            }
        }
    }
}
