using System;
using UnityEngine;
using UnityEngine.Events;

namespace EscapeKowloon.Scripts.FieldObjects
{
    public class Key : MonoBehaviour
    {
        public UnityEvent keyEvents;

        private void OnDestroy()
        {
            keyEvents.Invoke();
        }
    }
}