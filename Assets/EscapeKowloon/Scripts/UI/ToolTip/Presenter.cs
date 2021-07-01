using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace EscapeKowloon.Scripts.UI.ToolTip
{
    public class Presenter : MonoBehaviour
    {
        [SerializeField] private View _view;
        private Model _model;
        [SerializeField] private int _maxDisplayMsgCount = 5;
        [SerializeField] private int _defaultDisplaySec = 3;

        private void Start()
        {
            _model = new Model(_maxDisplayMsgCount);
            _view.CreateUiStock(_maxDisplayMsgCount);
            _model.Msgs
                .ObserveAdd()
                .Subscribe(m => _view.StartDraw(m.Value))
                .AddTo(this);

            _model.Msgs
                .ObserveRemove()
                .Subscribe(m => _view.CompleteDraw(m.Value))
                .AddTo(this);

            //Test();
        }

        /// <summary>
        /// 一定間隔でToolTipを表示
        /// </summary>
        /// <returns></returns>
        async UniTaskVoid Test()
        {
            var cnt = 0;
            while (true)
            {
                await UniTask.Delay(Random.Range(1, 10) * 100);
                //PushMsg(Random.Range(0, 100).ToString());
                PushMsg($"{cnt++}: GUID: ");
            }
        }

        [Button]
        public void PushMsg(string txt)
        {
            _model.PushMsg(new Model.Msg(txt, _defaultDisplaySec));
        }

        public void PushMsg(string txt, int lifeTimeSeconds)
        {
            _model.PushMsg(new Model.Msg(txt, lifeTimeSeconds));
        }

    }
}