using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace EscapeKowloon.Scripts.NpcActions.NpcActionImpls
{
    /// <summary>
    /// 発見済みの目標を追跡する
    /// </summary>
    public class ChaseTarget : NpcAction
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        private Transform Target => _npc.NpcTarget;

        private void Start()
        {
            StopMove();
        }

        private void Update()
        {
            //  目標が発見済みの場合のみ動かす   
            if (Target == null)
            {
                StopMove();
            }
            else
            {
                DoMove();
            }

            _navMeshAgent.SetDestination(Target.position);
        }

        private void DoMove()
        {
            _navMeshAgent.updatePosition = true;
            _navMeshAgent.updateRotation = true;
        }

        private void StopMove()
        {
            _navMeshAgent.updatePosition = false;
            _navMeshAgent.updateRotation = false;
        }

        private void OnDisable()
        {
            StopMove();
        }
    }
}