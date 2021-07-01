using UnityEngine;

namespace EscapeKowloon.Scripts.UI.HeadUpDisplay
{
    public class MotionPlayer : MonoBehaviour
    {
        //モーション再生する対象
        [SerializeField] Transform _target;
        [SerializeField] private bool _followPosition = true;
        [SerializeField] private bool _followRotation = true;
        private MotionClip _motionClip;
        private float _startTime = 0f;
        private float _delayTimeSec = 0f;

        private enum PlayState
        {
            None,
            Stop,
            Playing
        }

        private PlayState _playState = PlayState.None;

        public void Update()
        {
            MotionUpdate();
        }

        //motionClipからデータを取り出して、対象のPosition及びRotationを変化させる。
        private void MotionUpdate()
        {
            switch (_playState)
            {
                case PlayState.Playing:
                    break;
                default:
                    return;
            }

            var playTime = Time.time - _startTime - _delayTimeSec;

            if (playTime < 0f)
            {
                return;
            }

            if (_target == null) return;
            var newPos = _followPosition
                ? new Vector3(_motionClip.Curve.PosXCurve.Evaluate(playTime)
                    , _motionClip.Curve.PosYCurve.Evaluate(playTime)
                    , _motionClip.Curve.PosZCurve.Evaluate(playTime))
                : _target.position;

            var newRot = _followRotation
                ? new Quaternion(_motionClip.Curve.RotXCurve.Evaluate(playTime)
                    , _motionClip.Curve.RotYCurve.Evaluate(playTime)
                    , _motionClip.Curve.RotZCurve.Evaluate(playTime)
                    , _motionClip.Curve.RotWCurve.Evaluate(playTime))
                : _target.rotation;

            _target.SetPositionAndRotation(newPos, newRot);
        }

        //_delayTime_sec秒遅れて再生させる。
        public void MotionPlay(MotionClip motionClip, float delayTimeSec = 1f)
        {
            if (_target == null)
            {
                Debug.LogWarning("モーション再生対象が設定されていません。");
                return;
            }

            _startTime = Time.time;
            _motionClip = motionClip;
            _delayTimeSec = delayTimeSec;
            _playState = PlayState.Playing;
        }

        public void MotionStop()
        {
            if (_playState != PlayState.Playing)
            {
                return;
            }

            Debug.Log("モーション停止");
            _playState = PlayState.Stop;
        }
    }
}