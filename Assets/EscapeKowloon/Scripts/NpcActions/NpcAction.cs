using UnityEngine;

namespace EscapeKowloon.Scripts.NpcActions
{
    public abstract class NpcAction : MonoBehaviour
    {
        protected INpcActionable _npc;

        public void RegisterNpc(INpcActionable npc)
        {
            _npc = npc;
        }
    }
}