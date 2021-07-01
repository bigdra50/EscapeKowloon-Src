using UnityEngine;

namespace EscapeKowloon.Scripts.Managers
{
    public class ApplicationManager : MonoBehaviour
    {
        public void QuitApp()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
            UnityEngine.Application.Quit();
#endif
        }
    }
}