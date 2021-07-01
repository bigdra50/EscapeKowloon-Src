using System.Collections.Generic;
using EscapeKowloon.Scripts.NpcActions;
using UnityEngine;

namespace EscapeKowloon.Scripts.Monster
{
    public class MonsterActionableEffector : INpcActionable
    {
        private List<NpcAction> _currentNpcActions = new List<NpcAction>();
        private readonly MonsterEntity _monsterEntity;
        private readonly INpcActionsRepository<NpcState> _actionsRepository;

        public MonsterActionableEffector(MonsterEntity monster, INpcActionsRepository<NpcState> repository)
        {
            _monsterEntity = monster;
            _actionsRepository = repository;
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
            var actions = _actionsRepository.ResolveNpcActions(state);
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