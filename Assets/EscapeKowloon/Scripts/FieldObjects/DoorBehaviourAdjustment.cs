using UniRx;
using UnityEngine;

namespace EscapeKowloon.Scripts.FieldObjects
{
    /// <summary>
    /// ドアの振る舞いを調整する
    /// </summary>
    public class DoorBehaviourAdjustment : MonoBehaviour
    {
        [SerializeField] private DoorBehaviour _door;
        [SerializeField] private Collider _doorHitbox;

        private void Start()
        {
            // ドアを開閉中に体とドアが当たって開けにくいので､ 開閉中は当たり判定を無くす
            _door.OnGrabDoorKnob
                .Subscribe(_ => _doorHitbox.enabled = false)
                .AddTo(this);

            _door.OnReleaseDoorKnob
                .Subscribe(_ => _doorHitbox.enabled = true)
                .AddTo(this);
        }
    }
}