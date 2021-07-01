using System;
using EscapeKowloon.Scripts.Inputs;
using EscapeKowloon.Scripts.Items;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using Zenject;

namespace EscapeKowloon.Scripts.Escaper
{
    public class ItemHandler : MonoBehaviour
    {
        [Inject] private IEscaperInputEventProvider _inputEvent;
        [SerializeField] private HandType _handType;
        [SerializeField, Required] private OVRGrabber _grabber;
        [ShowInInspector, ReadOnly] private IItem _holdingItem;


        private void Start()
        {
            SubscribeUseItemEvent();
            _grabber
                .ObserveEveryValueChanged(x => x.grabbedObject)
                .Subscribe(x => _holdingItem = GetComponent<IItem>());
        }

        private void SubscribeUseItemEvent()
        {
            switch (_handType)
            {
                case HandType.Right:
                    _inputEvent
                        .OnPressRightUseItemButton
                        .Subscribe(_ => _holdingItem?.Use());
                    break;
                case HandType.Left:
                    _inputEvent
                        .OnPressLeftUseItemButton
                        .Subscribe(_ => _holdingItem?.Use());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    internal enum HandType
    {
        Right,
        Left,
    }
}