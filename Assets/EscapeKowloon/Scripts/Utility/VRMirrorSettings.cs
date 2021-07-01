using System;
using UnityEngine;

namespace EscapeKowloon.Scripts.Utility
{
    public class VRMirrorSettings : MonoBehaviour
    {
        public bool _isVRMirrorEnable = true;

        private void Start()
        {
            UnityEngine.XR.XRSettings.showDeviceView = _isVRMirrorEnable;
        }
    }
}