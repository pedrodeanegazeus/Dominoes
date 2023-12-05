using System;
using Dominoes.Core.Enums;
using Gazeus.CoreMobile.Commons.Core.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace Dominoes.Controllers.HUDs
{
    internal class SettingsPlayerController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private LocalizeStringEvent _positionText;

        private IGzLogger<SettingsPlayerController> _logger;

        public void SetName(string name)
        {
            _logger.Debug("CALLED: {method} - {name}",
                          nameof(SetName));

            _nameText.text = name;
        }

        public void SetPosition(TablePosition tablePosition)
        {
            string key = tablePosition switch
            {
                TablePosition.Bottom => "position-bottom",
                TablePosition.Left => "position-left",
                TablePosition.Right => "position-right",
                TablePosition.Top => "position-top",
                _ => throw new NotImplementedException($"Table position {tablePosition} not implemented"),
            };
            _positionText.SetEntry(key);
        }

        #region Unity
        private void Awake()
        {
            _logger = ServiceProvider.GetRequiredService<IGzLogger<SettingsPlayerController>>();
        }
        #endregion
    }
}
