using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace EscapeKowloon.Scripts.UI.ToolTip
{
    public class Model
    {
        public IReadOnlyReactiveDictionary<Guid, Msg> Msgs => _displayableMsgs;
        private ReactiveDictionary<Guid, Msg> _displayableMsgs;
        private Queue<Msg> _stockMsgs;
        private int _maxDisplayMsgCount;

        public Model(int maxDisplayableMsgsCount = 3)
        {
            _maxDisplayMsgCount = maxDisplayableMsgsCount;
            _displayableMsgs = new ReactiveDictionary<Guid, Msg>();
            _stockMsgs = new Queue<Msg>();
            ObserveMsgs();
        }

        private void ObserveMsgs()
        {
            Debug.Log("ObserveMsg");
            _displayableMsgs.ObserveAdd()
                .Do(_ => Debug.Log("ObserveAdd"))
                .Subscribe(async d =>
                {
                    // 寿命が来たら消す
                    await UniTask.Delay(d.Value.LifeTimeSec* 1000);
                    _displayableMsgs.Remove(d.Key);
                });
            _displayableMsgs.ObserveRemove()
                .Do(_ => Debug.Log("ObserveRemove"))
                .Subscribe(_ =>
                {
                    if (_stockMsgs.Count == 0 || _displayableMsgs.Count >= _maxDisplayMsgCount) return;
                    var msg = _stockMsgs.Dequeue();
                    _displayableMsgs.Add(msg.guid, msg);
                });
        }

        public void PushMsg(Msg msg)
        {
            if (_displayableMsgs.Count < _maxDisplayMsgCount)
            {
                _displayableMsgs.Add(msg.guid, msg);
            }
            else
            {
                _stockMsgs.Enqueue(msg);
            }
        }


        public class Msg
        {
            public string Text { get; }
            public Guid guid { get; }

            public int LifeTimeSec => _lifeTimeSec;

            private readonly int _lifeTimeSec;

            public Msg(string text, int lifeTimeSec = 3)
            {
                Text = text;
                _lifeTimeSec = lifeTimeSec;
                guid = Guid.NewGuid();
            }
        }
    }
}