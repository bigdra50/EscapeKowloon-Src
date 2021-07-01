using UnityEngine;

namespace EscapeKowloon.Scripts.NpcActions
{
    /// <summary>
    /// NPCとして行動することができる
    /// </summary>
    public interface INpcActionable 
    {
        Transform NpcTransform { get; set; }
        Transform NpcTarget { get; set; }
        void ApplyEffect(NpcState state);
    }
}