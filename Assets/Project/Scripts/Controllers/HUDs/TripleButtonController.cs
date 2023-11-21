﻿using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Dominoes.Controllers.HUDs
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

        private bool _isEnabled;

        public void SetState(int state)
        {
            Vector3 position = state switch
            {
                1 => _button1.gameObject.transform.position,
                2 => _button2.gameObject.transform.position,
                3 => _button3.gameObject.transform.position,
                _ => throw new ArgumentOutOfRangeException(nameof(state)),
            };
            _highlight.position = position;
        }

        #region Unity
        private void Awake()
        {
            _button1.onClick.AddListener(Button1Clicked);
            _button2.onClick.AddListener(Button2Clicked);
            _button3.onClick.AddListener(Button3Clicked);
            _isEnabled = true;
        }
        #endregion

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
