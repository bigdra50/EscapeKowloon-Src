using System;
using UnityEngine;
using UnityEngine.Events;

namespace EscapeKowloon.Scripts.FieldObjects
{
    public class RespawnableObject : MonoBehaviour
    {
        [SerializeField] private Transform _respawnPoint;
        [SerializeField] private float _respawnHeight = -5f;
        public UnityEvent OnRespawn = new UnityEvent();
        private Rigidbody _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        void Update()
        {
            if(transform.position.y <= _respawnHeight) Respawn();
        }

        void Respawn()
        {
            var currentTransform = transform;
            if (_rigidbody)
            {
                _rigidbody.velocity = Vector3.zero;
                _rigidbody.angularVelocity = Vector3.zero;
            }
            if (_respawnPoint)
            {
                currentTransform.position = _respawnPoint.position;
                currentTransform.rotation = _respawnPoint.rotation;
            }
            else
            {
                var currentPosition = currentTransform.position;
                currentPosition = new Vector3(currentPosition.x, 2f, currentPosition.z);
                currentTransform.position = currentPosition;
            }
            OnRespawn.Invoke();
        }
    }
}