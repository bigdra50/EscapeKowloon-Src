using System;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace EscapeKowloon.Scripts.Escaper
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField, Required] private GameObject _player;
        [SerializeField] private float _respawnHeight;
        [SerializeField] private Transform _spawnPoint;


        private void Start()
        {
            _player.ObserveEveryValueChanged(p => p.transform.position)
                .Where(p => p.y <= _respawnHeight)
                .Subscribe(_ => Spawn(_spawnPoint));

            //_player.GetComponent<EscaperCore>().OnDeadObservable
            //    .Subscribe(_ => Spawn(_spawnPoint))
            //    .AddTo(this);
        }

        void Spawn(Transform spawnPoint)
        {
            if (_player.TryGetComponent<OVRPlayerController>(out var ovrPlayerController))
            {
                ovrPlayerController.enabled = false;
            }

            _player.transform.position = spawnPoint.position;
            ovrPlayerController.enabled = true;
        }
    }
}