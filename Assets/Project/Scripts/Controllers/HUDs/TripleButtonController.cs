using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

namespace Dominoes.Controllers.HUDs
{
    internal class TripleButtonController : MonoBehaviour
    {
        public Action<int> Clicked;

        [SerializeField] private Button _button1;
        [SerializeField] private Button _button2;
        [SerializeField] private Button _button3;

        [Space]
        [SerializeField] private RectTransform _highlight;

        [Space]
        [SerializeField] private LocalizeStringEvent _button1Text;
        [SerializeField] private LocalizeStringEvent _button2Text;
        [SerializeField] private LocalizeStringEvent _button3Text;

        #region Unity
        private void Awake()
        {
            _button1.onClick.AddListener(Button1Clicked);
            _button2.onClick.AddListener(Button2Clicked);
            _button3.onClick.AddListener(Button3Clicked);
        }

        private void Start()
        {
            LocalizeStringEvent button1Text = _button1.GetComponentInChildren<LocalizeStringEvent>();
            button1Text.StringReference = _button1Text.StringReference;
            LocalizeStringEvent button2Text = _button2.GetComponentInChildren<LocalizeStringEvent>();
            button2Text.StringReference = _button2Text.StringReference;
            LocalizeStringEvent button3Text = _button3.GetComponentInChildren<LocalizeStringEvent>();
            button3Text.StringReference = _button3Text.StringReference;
        }
        #endregion

        private void Button1Clicked()
        {
            Debug.Log("1");

            _highlight.DOMove(_button1.gameObject.transform.position, 0.25f);
            Clicked?.Invoke(1);
        }

        private void Button2Clicked()
        {
            Debug.Log("2");

            _highlight.DOMove(_button2.gameObject.transform.position, 0.25f);
            Clicked?.Invoke(2);
        }

        private void Button3Clicked()
        {
            Debug.Log("3");

            _highlight.DOMove(_button3.gameObject.transform.position, 0.25f);
            Clicked?.Invoke(3);
        }
    }
}
