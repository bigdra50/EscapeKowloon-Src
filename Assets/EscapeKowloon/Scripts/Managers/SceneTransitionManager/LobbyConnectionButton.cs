using System;
using EscapeKowloon.Scripts.SceneTransitionManager;
using SceneTransitionManager;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace EscapeKowloon.Scripts.Managers.SceneTransitionManager
{
    /// <summary>
    /// プラットフォームを選択してマッチングのためのシーンへ遷移する
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class LobbyConnectionButton : MonoBehaviour
    {
        [SerializeField] private PlatformType _platformType;
        [Inject] private ZenjectSceneLoader _zenjectSceneLoader;
        [SerializeField] private SceneType NextScene = SceneType.PlayGround;

        void Start()
        {
            var button = GetComponent<Button>();
            button.OnClickAsObservable().Subscribe(async _ =>
            {
                // ボタンをクリックしたときの処理
                SceneLoader.LoadScene(NextScene.ToString(),
                    diContainer => { diContainer.BindInstance(_platformType).AsTransient(); });
            });
        }
    }
}