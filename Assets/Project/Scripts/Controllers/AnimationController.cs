﻿using System;
using Dominoes.Managers;
using Gazeus.CoreMobile.Commons.Core.Interfaces;
using UnityEngine;

namespace Dominoes.Controllers
{
    internal class AnimationController : MonoBehaviour
    {
        public event Action<string> EventFired;

        private IGzLogger<AnimationController> _logger;

        public void Initialize()
        {
            _logger = ServiceProviderManager.Instance.GetRequiredService<IGzLogger<AnimationController>>();
        }

        public void OnEventFired(string @event)
        {
            _logger.Debug("CALLED: {method} - {event}",
                          nameof(OnEventFired),
                          @event);

            EventFired?.Invoke(@event);
        }
    }
}
