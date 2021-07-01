using UnityEngine;

namespace EscapeKowloon.Scripts.UI.VRUI
{
    public class ActiveSwitcher : MonoBehaviour
    {
        public void SwitchActive() => gameObject.SetActive(!gameObject.activeSelf);
    }
}