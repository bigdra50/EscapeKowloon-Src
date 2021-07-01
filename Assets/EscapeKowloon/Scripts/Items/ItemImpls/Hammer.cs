using Cysharp.Threading.Tasks;
using EscapeKowloon.Scripts.FieldObjects;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace EscapeKowloon.Scripts.Items.ItemImpls
{
    /// <summary>
    /// 触れたBreakableObjectを破壊する
    /// </summary>
    public class Hammer : ItemBase
    {
        //public Transform grabbedTransform => vrGrabbable.
        //public Collider[] grabPoints => vrGrabbable.grabPoints;
        [SerializeField] private Collider hitBox;

        protected override string Name => "ハンマー";

        void Start()
        {
            hitBox.OnTriggerEnterAsObservable()
                .Select(c => (c.TryGetComponent<BreakableObject>(out var breakable), breakable))
                .Where(x => x.Item1)
                .Do(_=>print("Break"))
                .Subscribe(x => x.breakable.Break().Forget())
                .AddTo(this);
        }
        public override void Use()
        {
            print(Name);
        }
    }
}