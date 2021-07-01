using TMPro;
using UnityEngine;

namespace EscapeKowloon.Scripts.UI.ToolTip
{
    public class ToolTip : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
        [SerializeField] private RectTransform _defaultTransform;

        public void PutText(string text)
        {
            _textMeshProUGUI.text = text;
        }

        public void Reset(Transform initTransform)
        {
            _textMeshProUGUI.text = "";
            transform.position = initTransform.position;
            //transform.rotation = _defaultTransform.rotation;
        }

        // Start is called before the first frame update
        private void Start()
        {
            if (!_defaultTransform)
            {
            }
        }

    }
}