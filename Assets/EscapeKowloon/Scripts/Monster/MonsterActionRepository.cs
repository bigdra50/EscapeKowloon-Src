using System;
using System.Collections.Generic;
using EscapeKowloon.Scripts.NpcActions;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Rendering;

namespace EscapeKowloon.Scripts.Monster
{
    public class MonsterActionRepository : SerializedMonoBehaviour, INpcActionsRepository<NpcState>
    {
        public IDictionary<NpcState, List<NpcAction>> ActionsTable => _actionsTable;

        //[SerializeField] private NpcActionTableData _npcActionTableData;
        [SerializeField] private readonly Dictionary<NpcState, List<NpcAction>> _actionsTable = new Dictionary<NpcState, List<NpcAction>>();

        public List<NpcAction> ResolveNpcActions(NpcState state)
        {
            return ActionsTable[state];
        }
    }
}