using System;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeKowloon.Scripts.Managers
{
    public class GameControlSettings : MonoBehaviour
    {
        [SerializeField] private OVRPlayerController _ovrPlayerController;

        public void SetRotationMode(RotationMode mode) => _ovrPlayerController.SnapRotation = mode == RotationMode.Snap;
        public void SetRotationMode(bool isSnap) => _ovrPlayerController.SnapRotation = isSnap;

        public enum RotationMode
        {
            Slide,
            Snap,
        }
    }
}