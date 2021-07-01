using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace EscapeKowloon.Scripts.NpcActions.NpcActionImpls
{
    /// <summary>
    /// 徘徊させる
    /// </summary>
    public class RandomWalk : NpcAction
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        public Transform[] points;
        int destPoint;
        float distance;
        Vector3 pos;

        private void Start()
        {
            _navMeshAgent.autoBraking = false;

            GotoNextPoint();
        }

        private void Update()
        {
            //_navMeshAgent.SetDestination(_target);
            //Debug.Log("<color=magenta> RandomWalk </color>");

            DoMove();
            if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance < 0.5f)
            {
                //StopMove();
                GotoNextPoint();
            }
            else
            {
            }

            //distance = Vector3.Distance(_npc.NpcTransform.position, pos);

            //if (distance < 0.5f)
            //GotoNextPoint();
        }

        void GotoNextPoint() 
        {
            destPoint = Random.Range(0, points.Length + 1);

            //print($"Go To Next Point:{points[destPoint].position}");
            if (points.Length == 0)
                return;

            //_navMeshAgent.destination = points[destPoint].position;
            _navMeshAgent.SetDestination(points[destPoint].position);

            //pos = points[destPoint].position;
            //_npc.NpcTransform.position = Vector3.Lerp(_npc.NpcTransform.position, pos, Time.deltaTime);

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

    }
}