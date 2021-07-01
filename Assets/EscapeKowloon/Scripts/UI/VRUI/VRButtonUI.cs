using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace EscapeKowloon.Scripts.UI.VRUI
{
    [RequireComponent(typeof(Button)), RequireComponent(typeof(Collider))]
    public class VRButtonUI : MonoBehaviour
    {
        private const string FingerTagName = "Finger";

        private void Start()
        {
            var button = GetComponent<Button>();
            var collider = GetComponent<Collider>();
            
            // 指で触れたらButtonをクリックしたことにする.
            collider
                .OnTriggerEnterAsObservable()
                .Where(c => c.CompareTag(FingerTagName))
                .Subscribe(_ => button.onClick.Invoke());
        }
    }
}