using System;

namespace EscapeKowloon.Scripts.NpcActions
{
    [Flags]
    public enum NpcState
    {
        Vigilance = 0,    // 警戒中
        FoundTarget = 1,    // 標的を発見済み
        Stuck = 1 << 1,    // 動けない
    }
}