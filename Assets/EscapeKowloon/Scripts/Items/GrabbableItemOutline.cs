using Cysharp.Threading.Tasks.Triggers;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace EscapeKowloon.Scripts.Items
{
    public class GrabbableItemOutline : MonoBehaviour
    {
        [SerializeField] private Collider[] _grabPoints;
        [SerializeField, Required] private Renderer _renderer;
        [SerializeField] private string _shaderPropName = "_Outline_Width";

        void Start()
        {
            var outlineHandler = new OutlineHandler(_renderer.materials[1]);
            _grabPoints.ForEach(c =>
            {
                c.OnTriggerEnterAsObservable()
                    .Where(x => x.CompareTag($"Hand"))
                    .Subscribe(_ => outlineHandler.Switch(_shaderPropName, 5));
                c.OnTriggerExitAsObservable()
                    .Where(x => x.CompareTag($"Hand"))
                    .Subscribe(_ => outlineHandler.Switch(_shaderPropName, 0));
            });
        }
    }
}