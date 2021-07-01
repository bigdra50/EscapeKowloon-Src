using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace EscapeKowloon.Scripts.UI.ToolTip
{
    public class ToolTipTest : MonoBehaviour
    {
        public Presenter _presenter;

        private async UniTaskVoid Start()
        {
            await UniTask.WaitUntil(() => _presenter != null);
            ToolTipSample();
        }

        async UniTaskVoid ToolTipSample()
        {
            var lifeTimeSec = 5;
            _presenter.PushMsg("エレベーターを探して､ この建物から脱出して下さい｡", lifeTimeSec);
            await UniTask.Delay(1 * 1000);
            _presenter.PushMsg("制限時間は10秒です｡", lifeTimeSec);
            await UniTask.Delay(5 * 1000);
            _presenter.PushMsg("残りの制限時間はあと5秒です｡", lifeTimeSec);
            _presenter.PushMsg("5", 3);
            await UniTask.Delay(1 * 1000);
            _presenter.PushMsg("4", 3);
            await UniTask.Delay(1 * 1000);
            _presenter.PushMsg("3", 3);
            await UniTask.Delay(1 * 1000);
            _presenter.PushMsg("2", 3);
            await UniTask.Delay(1 * 1000);
            _presenter.PushMsg("1", 3);
            await UniTask.Delay(1 * 1000);
            _presenter.PushMsg("GAME OVER", lifeTimeSec);
            await UniTask.Delay(1 * 1000);
        }
    }
}