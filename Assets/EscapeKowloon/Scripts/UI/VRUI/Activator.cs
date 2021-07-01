using System;
using UniRx;
using UnityEngine;

namespace EscapeKowloon.Scripts.UI.VRUI
{
    public class Activator : MonoBehaviour
    {
        [SerializeField] private Camera _playerCamera;

        [SerializeField, Tooltip("これより小さければ有効化する")]
        private float _threashold;

        [SerializeField] private GameObject _content;
        private BoolReactiveProperty _isActive = new BoolReactiveProperty();

        void Start()
        {
            _isActive.Subscribe(x => _content.SetActive(x));
        }

        private void Update()
        {
            var dot = Vector3.Dot(-transform.forward.normalized, _playerCamera.transform.forward.normalized);
            _isActive.Value = dot <= _threashold;
        }
    }
}