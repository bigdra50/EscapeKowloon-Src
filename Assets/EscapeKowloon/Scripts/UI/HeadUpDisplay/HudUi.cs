using UnityEngine;

namespace EscapeKowloon.Scripts.UI.HeadUpDisplay
{
    public class HudUi : MonoBehaviour
    {
        [SerializeField] private MotionRecorder _motionRecorder;
        [SerializeField] private MotionPlayer _motionPlayer;
        private MotionClip _motionClip;
        public float delayTimeSec = .3f;

        private void Start()
        {
            _motionClip = new MotionClip();
            _motionRecorder.StartRecord(_motionClip);
            _motionPlayer.MotionPlay(_motionClip, delayTimeSec);
        }

        private void OnDestroy()
        {
            _motionRecorder.StartRecord(_motionClip);
            _motionPlayer.MotionStop();
        }
    }
}