using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Dominoes.Controllers.HUDs
{
    internal class ToggleButtonController : MonoBehaviour
    {
        public Action<bool> Clicked;

        [SerializeField] private Button _button;
        [SerializeField] private RectTransform _switch;

        [Space]
        [SerializeField] private Transform _onPosition;
        [SerializeField] private Color _onColor;

        [Space]
        [SerializeField] private Transform _offPosition;
        [SerializeField] private Color _offColor;

        [Space]
        [SerializeField] private bool _initialState;
        [SerializeField] private float _switchMoveSpeed = 0.25f;

        private Image _buttonImage;
        private bool _isEnabled;
        private bool _state;

        #region Unity
        private void Awake()
        {
            _button.onClick.AddListener(ButtonClicked);
            _buttonImage = _button.GetComponent<Image>();
            _buttonImage.color = _initialState ? _onColor : _offColor;
            _isEnabled = true;
            _state = _initialState;
        }

        private void Start()
        {
            _switch.position = _initialState ? _offPosition.position : _onPosition.position;
        }
        #endregion

        private void ButtonClicked()
        {
            if (_isEnabled)
            {
                _isEnabled = false;
                _state = !_state;
                _buttonImage.DOColor(_state ? _onColor : _offColor, _switchMoveSpeed);
                _switch
                    .DOMove(_state ? _offPosition.position : _onPosition.position, _switchMoveSpeed)
                    .OnComplete(() => _isEnabled = true);
                Clicked?.Invoke(_state);
            }
        }
    }
}
