using System;
using UnityEngine;

namespace Dominoes.Controllers
{
    internal class AnimationController : MonoBehaviour
    {
        public event Action<string> EventFired;

        public void OnEventFired(string @event)
        {
            EventFired?.Invoke(@event);
        }
    }
}
