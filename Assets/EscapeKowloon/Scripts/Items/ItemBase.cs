using System;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace EscapeKowloon.Scripts.Items
{
    public abstract class ItemBase : MonoBehaviour, IItem
    {
        public IReadOnlyReactiveProperty<bool> IsAvailable => _isAvailable;
        protected BoolReactiveProperty _isAvailable = new BoolReactiveProperty();
        protected abstract string Name { get; }

        private void Start()
        {
            _isAvailable.Value = true;
        }

        public abstract void Use();
    }
}