using System;
using System.Collections.Generic;

namespace EscapeKowloon.Scripts.NpcActions
{
    public interface INpcActionsTable<T> where T: Enum
    {
        IDictionary<T, List<NpcAction>> ActionsTable { get; }

        List<NpcAction> GetCurrentActions(T state);
    }
}