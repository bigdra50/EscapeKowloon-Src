using System.Collections.Generic;
using EscapeKowloon.Scripts.NpcActions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EscapeKowloon.Scripts.Monster
{
    public class MonsterActionsTable : SerializedMonoBehaviour, INpcActionsTable<NpcState>
    {
        public IDictionary<NpcState, List<NpcAction>> ActionsTable => _actionsTable;

        //[SerializeField] private NpcActionTableData _npcActionTableData;
        [SerializeField] private readonly Dictionary<NpcState, List<NpcAction>> _actionsTable = new Dictionary<NpcState, List<NpcAction>>();

        public List<NpcAction> GetCurrentActions(NpcState state)
        {
            return ActionsTable[state];
        }
    }
}