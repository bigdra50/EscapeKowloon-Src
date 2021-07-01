using UnityEngine;

namespace EscapeKowloon.Scripts.UI.HeadUpDisplay
{
    public class MotionRecorder : MonoBehaviour
    {
        //録画するオブジェクトの対象
        [SerializeField] private Transform recordTarget;
        private MotionClip _motionClip;
        private float _startTime;

        private float _timer = 0f;

        //モーションの1秒あたりのキー数
        private const int RecordFPS = 30;

        private enum RecordState
        {
            None,
            Recording,
            Stop
        }

        private RecordState _recordState = RecordState.None;

        private void Update()
        {
            CaptureUpdate();
        }

        private void CaptureUpdate()
        {
            switch (_recordState)
            {
                case RecordState.Recording:
                    break;
                case RecordState.Stop:
                    Debug.Log("録画終了");
                    _recordState = RecordState.None;
                    return;
                default:
                    return;
            }

            //１秒間に recordFPS 回数分だけキャプチャする。
            if (_timer > 1f / RecordFPS)
            {
                _timer -= 1f / RecordFPS;
                var playTime = Time.time - _startTime;
                if (recordTarget != null)
                {
                    _motionClip.Curve.AddKeyPositionAndRotation(playTime, recordTarget.position,
                        recordTarget.rotation);
                }
            }

            _timer += Time.deltaTime;
        }

        public void StartRecord(MotionClip motionClip)
        {
            _motionClip = motionClip;
            _recordState = RecordState.Recording;
            _startTime = Time.time;
            _timer = 0f;
        }

        public void StopRecord()
        {
            _recordState = RecordState.Stop;
        }
    }
}