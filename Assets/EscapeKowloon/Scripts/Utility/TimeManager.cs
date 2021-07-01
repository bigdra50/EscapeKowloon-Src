using UnityEngine;

namespace EscapeKowloon.Scripts.Utility
{
    /// <summary>
    /// 経過時間を管理する
    /// </summary>
    public class TimeManager : MonoBehaviour
    {
        public float Seconds => _elapsedSecond;
        public float Minutes => _elapsedSecond / 60;
        public float Hour => Minutes / 60;
        public float OneDayHour => Hour % 24;
        public float OneHourMinutes => Minutes % 60;

        private float _elapsedSecond;

        void Update()
        {
            _elapsedSecond += Time.deltaTime * 60 * 60 * 2;
        }
    }
}