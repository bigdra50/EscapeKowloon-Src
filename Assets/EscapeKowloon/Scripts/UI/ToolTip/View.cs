using System;
using System.Collections.Generic;
using System.Linq;
using EscapeKowloon.Scripts.Utility;
using UnityEngine;

namespace EscapeKowloon.Scripts.UI.ToolTip
{
    public class View : MonoBehaviour
    {
        private Queue<ToolTip> _toolTipPool = new Queue<ToolTip>();
        private OrderedDictionary<Guid, ToolTip> _usingToolTip = new OrderedDictionary<Guid, ToolTip>();
        [SerializeField] private ToolTip _toolTipPrefab;
        [SerializeField] private float _spacingBetweenUiObject = .15f;
        [SerializeField] private Vector3 _initialPositionPreset;


        /// <summary>
        /// 必要な分のUIオブジェクトを事前に生成しておく｡
        /// </summary>
        /// <param name="maxDisplayMsgCount"></param>
        public void CreateUiStock(int maxDisplayMsgCount)
        {
            for (var i = 0; i < maxDisplayMsgCount + 1; i++)
            {
                var toolTip = Instantiate(_toolTipPrefab, transform);
                toolTip.transform.position += _initialPositionPreset;
                toolTip.gameObject.SetActive(false);
                _toolTipPool.Enqueue(toolTip);
            }
        }

        public void StartDraw(Model.Msg msg)
        {
            var toolTip = _toolTipPool.Dequeue();
            toolTip.PutText(msg.Text);
            toolTip.name = msg.Text;
            toolTip.gameObject.SetActive(true);
            _usingToolTip.Add(msg.guid, toolTip);
            AdjustToolTipPosition();
            print($"Draw: {msg.Text}");
        }

        public void CompleteDraw(Model.Msg msg)
        {
            _usingToolTip[msg.guid].PutText("");
            _usingToolTip[msg.guid].transform.position = transform.position + _initialPositionPreset;
            _usingToolTip[msg.guid].gameObject.SetActive(false);
            _toolTipPool.Enqueue(_usingToolTip[msg.guid]);
            _usingToolTip.Remove(msg.guid);
            print($"Complete: {msg.Text}");
        }

        private void AdjustToolTipPosition()
        {
            if (_usingToolTip.Count == 0) return;

            foreach (var toolTip in _usingToolTip.Values)
            {
                var toolTipTransform = toolTip.transform;
                var currentPos = toolTipTransform.position;
                toolTipTransform.position =
                    new Vector3(currentPos.x, currentPos.y + _spacingBetweenUiObject, currentPos.z);
            }
        }
    }
}