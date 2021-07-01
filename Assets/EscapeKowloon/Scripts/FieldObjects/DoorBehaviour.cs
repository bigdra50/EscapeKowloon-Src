using System;
using EscapeKowloon.Scripts.Items.ItemImpls;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace EscapeKowloon.Scripts.FieldObjects
{
    public class DoorBehaviour : MonoBehaviour
    {
        public IObservable<Unit> OnGrabDoorKnob => _onGrabDoorKnob;
        public IObservable<Unit> OnReleaseDoorKnob => _onReleaseDoorKnob;

        /// <summary>
        /// ドアが施錠されているか
        /// </summary>
        public IReadOnlyReactiveProperty<bool> IsLock => _isLock;

        /// <summary>
        /// ドアが開いているか
        /// </summary>
        public IReadOnlyReactiveProperty<bool> IsOpen => _isOpen;

        [SerializeField] private Collider _handleSlot;
        [SerializeField] private GameObject _doorHandleObject;
        [SerializeField] private OVRGrabbable _pickupHandleObject;
        [SerializeField] private Rigidbody _doorJointRigidBody;
        private Subject<Unit> _onGrabDoorKnob = new Subject<Unit>();
        private Subject<Unit> _onReleaseDoorKnob = new Subject<Unit>();
        private BoolReactiveProperty _isLock = new BoolReactiveProperty(true);
        private BoolReactiveProperty _isOpen = new BoolReactiveProperty(false);

        void Start()
        {
            // ドアのロックを解除したら, ドアを掴んだときのイベントを発行開始する
            _isLock
                .Where(x => !x)
                .Subscribe(_ =>
                {
                    var grabDoor = _pickupHandleObject.ObserveEveryValueChanged(x => x.isGrabbed);
                    grabDoor
                        .Where(x => x)
                        .Subscribe(x => _onGrabDoorKnob.OnNext(Unit.Default));
                    grabDoor
                        .Where(x => !x)
                        .Subscribe(x => _onReleaseDoorKnob.OnNext(Unit.Default));
                })
                .AddTo(this);

            _handleSlot
                .OnTriggerEnterAsObservable()
                .Select(c => (isHit: c.TryGetComponent<DoorHandle>(out var doorHandle), doorHandle))
                .Where(x => x.isHit)
                .Subscribe(x =>
                {
                    _isLock.SetValueAndForceNotify(false);
                    _doorHandleObject.SetActive(true);
                    _pickupHandleObject.gameObject.SetActive(true);
                    _doorJointRigidBody.isKinematic = false;
                    x.doorHandle.Use();
                    Destroy(_handleSlot.gameObject);
                });
        }
    }
}