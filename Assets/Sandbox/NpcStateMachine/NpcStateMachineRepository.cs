using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;

namespace Sandbox.NpcStateMachine
{
    [RequireComponent(typeof(NpcStateMachine))]
    public class NpcStateMachineRepository : SerializedMonoBehaviour
    {
        [SerializeField] private SerializedDictionary<NpcState, NpcAction> _actionsMap;
    }
}