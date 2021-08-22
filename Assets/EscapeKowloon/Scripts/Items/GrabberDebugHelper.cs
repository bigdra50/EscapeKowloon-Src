using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace EscapeKowloon.Scripts.Items
{
    public class GrabberDebugHelper : MonoBehaviour
    {
        [SerializeField] private OVRGrabber _ovrGrabber;
        [SerializeField] private CapsuleCollider _grabberCollider;
        [SerializeField] private float _magnification = 3f;


        private float _defaultColliderRadius;

        private void Start()
        {
            _defaultColliderRadius = _grabberCollider.radius;
            this.UpdateAsObservable()
                .Select(_ => Input.GetKey(KeyCode.B))
                .Subscribe(x =>
                {
                    SwitchGrabberCollider(x);
                    if (x)
                    {
                        _ovrGrabber.GrabBeginForDebug();
                    }
                    else
                    {
                        _ovrGrabber.GrabEndForDebug();
                    }
                });
        }

        public void SwitchGrabberCollider(bool isExpand)
        {
            _grabberCollider.enabled = true;
            if (isExpand)
            {
                _grabberCollider.radius = _defaultColliderRadius * _magnification;
            }
            else
            {
                _grabberCollider.radius = _defaultColliderRadius;
            }
        }
    }
}