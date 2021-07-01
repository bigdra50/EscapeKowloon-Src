using UnityEngine;

namespace EscapeKowloon.Scripts.Utility
{
    public class OVRRecenterPose : MonoBehaviour
    {
        public void RecenterPose() => OVRManager.display.RecenterPose();
    }
}