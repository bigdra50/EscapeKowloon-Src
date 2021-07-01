using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EscapeKowloon.Scripts.NpcActions.NpcActionImpls
{
    // 索敵して標的を更新する
    public class SearchTarget : NpcAction
    {
        Vector3 playerPos;
        public GameObject player;
        float distance;
        [SerializeField] float trackingRange = 10f;

        private void Start()
        {

        }

        private void Update()
        {
            //Debug.Log("<color=green> Searching Target </color>");

            playerPos = player.transform.position;
            distance = Vector3.Distance(_npc.NpcTransform.position, playerPos);

            if (distance < trackingRange)
            {
                SetTarget(player.transform);
            }
            else
            {
                SetTarget(null);
            }
           
        }
            [Button]
            private void SetTarget(Transform target) => _npc.NpcTarget = target;
    }
}