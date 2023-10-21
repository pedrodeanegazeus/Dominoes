using System;
using Gazeus.CoreMobile.Commons.Core.Interfaces;
using UnityEngine;

namespace Dominoes.Controllers
{
    internal class AnimationController : MonoBehaviour
    {
        public event Action<string> EventFired;

        private readonly IGzLogger<AnimationController> _logger = ServiceProvider.GetRequiredService<IGzLogger<AnimationController>>();

        public void OnEventFired(string @event)
        {
            _logger.Debug("CALLED: {method} - {event}",
                          nameof(OnEventFired),
                          @event);

            EventFired?.Invoke(@event);
        }
    }
}
