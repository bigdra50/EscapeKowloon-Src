using System.Collections.Generic;
using EscapeKowloon.Scripts.NpcActions;
using UnityEngine;

namespace EscapeKowloon.Scripts.Monster
{
    public class MonsterActionApplier : INpcActionable
    {
        private List<NpcAction> _currentNpcActions = new List<NpcAction>();
        private readonly MonsterEntity _monsterEntity;
        private readonly INpcActionsTable<NpcState> _actionsTable;

        public MonsterActionApplier(MonsterEntity monster, INpcActionsTable<NpcState> table)
        {
            _monsterEntity = monster;
            _actionsTable = table;
        }

        public Transform NpcTransform
        {
            get => _monsterEntity.transform;
            set
            {
                var transform = _monsterEntity.transform;
                transform.position = value.position;
                transform.rotation = value.rotation;
            }
        }

        public Transform NpcTarget
        {
            get => _monsterEntity.CurrentTarget;
            set => _monsterEntity.SetTarget(value);
        }

        public void ApplyEffect(NpcState state)
        {
            var actions = _actionsTable.GetCurrentActions(state);
            // 前のNpcActionを一旦無効にして, 次のを有効にする
            _currentNpcActions.ForEach(x => x.enabled = false);
            _currentNpcActions = actions;
            _currentNpcActions.ForEach(x =>
            {
                x.enabled = true;
                x.RegisterNpc(this);
            });
        }
    }
}