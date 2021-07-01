using System;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace EscapeKowloon.Scripts.Items.ItemImpls
{
    public class Battery : ItemBase, IGrabbable
    {
        [SerializeField] private OVRGrabbable ovrGrabbable;
        public bool isGrabbed => ovrGrabbable.isGrabbed;
        public Transform grabbedTransform => ovrGrabbable.grabbedTransform;
        public Collider[] grabPoints => ovrGrabbable.grabPoints;


        protected override string Name => "バッテリー";

        [Button]
        public override void Use()
        {
            if (!_isAvailable.Value) return;
            Debug.Log("Use Battery");
        }
    }
}