using System.Collections.Generic;
using EscapeKowloon.Scripts.NpcActions;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace EscapeKowloon.Scripts.Monster
{
    public abstract class MonsterEntity : SerializedMonoBehaviour
    {
        [ShowInInspector] public Transform CurrentTarget => _currentTarget;
        public IReadOnlyReactiveProperty<NpcState> CurrentState => _currentState;
        public bool enableDebug;

        protected Transform _currentTarget;
        protected readonly ReactiveProperty<NpcState> _currentState = new ReactiveProperty<NpcState>();


        public void SetTarget(Transform target)
        {
            _currentTarget = target;
        }

        [Button, ShowIf(nameof(enableDebug))]
        protected void ChangeState(NpcState state)
        {
            _currentState.Value = state;
        }

        // 状態の更新 http://yas-hummingbird.blogspot.com/2009/02/c_11.html
        protected void AddState(NpcState state)
        {
            _currentState.Value |= state;
        }

        protected void RemoveState(NpcState state)
        {
            _currentState.Value &= ~state;
        }
    }
}