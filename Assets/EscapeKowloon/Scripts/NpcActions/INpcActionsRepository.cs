using System;
using System.Collections.Generic;

namespace EscapeKowloon.Scripts.NpcActions
{
    public interface INpcActionsRepository<T> where T: Enum
    {
        IDictionary<T, List<NpcAction>> ActionsTable { get; }

        List<NpcAction> ResolveNpcActions(T state);
    }
}