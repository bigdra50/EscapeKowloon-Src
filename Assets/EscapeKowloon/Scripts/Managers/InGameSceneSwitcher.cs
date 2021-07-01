using EscapeKowloon.Scripts.Escaper;
using EscapeKowloon.Scripts.SceneTransitionManager;
using SceneTransitionManager;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace EscapeKowloon.Scripts.Managers
{
    public class InGameSceneSwitcher : MonoBehaviour
    {
        [SerializeField, Required] private EscaperCore _escaper;
        [SerializeField] private SceneType NextScene = SceneType.Launcher;

        void Start()
        {
            _escaper.OnDeadObservable
                .Subscribe(_ => SceneLoader.LoadScene(NextScene.ToString(), __=> { }))
                .AddTo(this);
        }
    }
}