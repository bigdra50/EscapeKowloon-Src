using System.Collections.Generic;
using UnityEngine;

namespace EscapeKowloon.Scripts.UI.Launcher
{
    public class ButtonInteraction : MonoBehaviour
    {
        [SerializeField, Tooltip("このボタンによってアクティブ化するボタン")]
        private List<RectTransform> _activatableButtons;
    }
}