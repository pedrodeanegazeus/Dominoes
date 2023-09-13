using System;
using UnityEngine;

namespace Dominoes.Controllers
{
    public class AnimationController : MonoBehaviour
    {
        public event Action<string> EventFired;

        public void OnEventFired(string @event)
        {
            EventFired?.Invoke(@event);
        }
    }
}
