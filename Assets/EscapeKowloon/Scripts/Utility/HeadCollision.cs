using System;
using UnityEngine;

namespace EscapeKowloon.Scripts.Utility
{
    public class HeadCollision : MonoBehaviour
    {
        //public GameObject OVRCameraRig;
        [SerializeField]private VRCameraCharacterControllerFix VRCameraCharacterControllerFix;

        private void Start()
        {
            //VRCameraCharacterControllerFix = OVRCameraRig.GetComponent<VRCameraCharacterControllerFix>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (TriggerVRObject(other))
                VRCameraCharacterControllerFix.enabled = true;
        }

        private void OnTriggerStay(Collider other)
        {
            if (TriggerVRObject(other))
            {
                //VRCameraCharacterControllerFix.enabled = true;
                VRCameraCharacterControllerFix.FixPosition();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (TriggerVRObject(other))
                VRCameraCharacterControllerFix.Stop();
        }

        private bool TriggerVRObject(Collider other)
        {
            return other.CompareTag("Wall");
            //if (other.gameObject == OVRCameraRig)
            //    return true;
            //else
            //    return false;
        }
    }
}