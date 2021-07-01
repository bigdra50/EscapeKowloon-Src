using UnityEngine;

namespace EscapeKowloon.Scripts.UI.HeadUpDisplay
{
    /// <summary>
    /// 動きの入力情報を保存するデータクラス
    /// </summary>
    //Motion保存用のデータクラス
    public class MotionClip
    {
        public readonly PosRotCurve Curve = new PosRotCurve();

        //Position及びRotationのアニメーションカーブ
        public class PosRotCurve
        {
            public readonly AnimationCurve PosXCurve;
            public readonly AnimationCurve PosYCurve;
            public readonly AnimationCurve PosZCurve;
            public readonly AnimationCurve RotXCurve;
            public readonly AnimationCurve RotYCurve;
            public readonly AnimationCurve RotZCurve;
            public readonly AnimationCurve RotWCurve;

            public PosRotCurve()
            {
                PosXCurve = new AnimationCurve();
                PosYCurve = new AnimationCurve();
                PosZCurve = new AnimationCurve();
                RotXCurve = new AnimationCurve();
                RotYCurve = new AnimationCurve();
                RotZCurve = new AnimationCurve();
                RotWCurve = new AnimationCurve();
            }

            public void AddKeyPositionAndRotation(float time, Vector3 position, Quaternion rotation)
            {
                PosXCurve.AddKey(time, position.x);
                PosYCurve.AddKey(time, position.y);
                PosZCurve.AddKey(time, position.z);
                RotXCurve.AddKey(time, rotation.x);
                RotYCurve.AddKey(time, rotation.y);
                RotZCurve.AddKey(time, rotation.z);
                RotWCurve.AddKey(time, rotation.w);
            }
        }
    }
}