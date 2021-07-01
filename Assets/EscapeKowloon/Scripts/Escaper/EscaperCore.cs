using System;
using EscapeKowloon.Scripts.Inputs;
using EscapeKowloon.Scripts.Monster;
using Sirenix.OdinInspector;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace EscapeKowloon.Scripts.Escaper
{
    public class EscaperCore : MonoBehaviour, IEscaper
    {
        [ShowInInspector] public BoolReactiveProperty IsMovable { get; } = new BoolReactiveProperty();
        public IObservable<Unit> OnDeadObservable => _onDeadObservable;
        public bool _isTeleportAvailable;
        
        [Inject] private IEscaperInputEventProvider _input;
        [SerializeField, Required] private Transform _spawnPoint;
        [SerializeField] private ItemHandler _rightItemHandler;
        [SerializeField] private ItemHandler _leftItemHandler;
        [SerializeField] private TeleportHandler _teleportHandler;
        
        private Subject<Unit> _onDeadObservable = new Subject<Unit>();
        private CharacterController _characterController;
        private OVRPlayerController _ovrPlayerController;

        public void Initialize()
        {
            _characterController = GetComponent<CharacterController>();
            _ovrPlayerController = GetComponent<OVRPlayerController>();
            transform.position = _spawnPoint.position;
            IsMovable.Value = true;
            _isTeleportAvailable = true;
            print($"Escaper has initialized!");
        }


        private async void Start()
        {
            Initialize();

            IsMovable
                .Subscribe(x => _characterController.enabled = x)
                .AddTo(this);

            // 攻撃するモンスターに触れたら死ぬ
            _characterController
                .OnTriggerEnterAsObservable()
                .Select(x => (x.TryGetComponent<IMonster>(out var monster), monster))
                .Where(x => x.Item1)
                .Where(x => x.monster.IsAttacking)
                .Subscribe(_ => Death());


            _input.OnPressDownTeleportButton
                .Where(_=> _teleportHandler)
                .Where(_ => _isTeleportAvailable)
                .Do(_=>print($"<color=red>Teleport</color>"))
                .Subscribe(_ =>
                {
                    _teleportHandler.StartDraw();
                    _ovrPlayerController.EnableRotation = false;
                });
            
            _input.OnPressUpTeleportButton
                .Where(_=> _teleportHandler)
                .Where(_ => _isTeleportAvailable)
                .Do(_=>print($"<color=blue>Teleport</color>"))
                .Subscribe(_ =>
                {
                    _teleportHandler.StopDraw(transform);
                    _ovrPlayerController.EnableRotation = true;
                });
            
        }

        public void Death()
        {
            _onDeadObservable.OnNext(default);
            print("<color=red>Escaper is dead!</color>");
        }
    }
}