using UnityEngine;

namespace EscapeKowloon.Scripts.Items.ItemImpls
{
    /// <summary>
    /// 対応するドアノブの外れたドアに触れたらそのドアの外れていたドアノブがアクティブになり, ドアを開けられるようになる
    /// </summary>
    public class DoorHandle : ItemBase, IGrabbable
    {
        [SerializeField] private OVRGrabbable ovrGrabbable;
        public bool isGrabbed => ovrGrabbable.isGrabbed;
        public Transform grabbedTransform => ovrGrabbable.grabbedTransform;
        public Collider[] grabPoints => ovrGrabbable.grabPoints;

        protected override string Name => "ドアノブ";

        public override void Use()
        {
            ovrGrabbable.grabbedBy.ForceRelease(ovrGrabbable);
            gameObject.SetActive(false);
        }
    }
}