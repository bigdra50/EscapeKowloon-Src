using System;
using Sandbox.NpcStateMachine;
using UnityEngine;

namespace Sandbox.HierarchicalFiniteStateMachine
{
    public abstract class Model<TState> where TState: Enum
    {
        
        public State<TState, Model<TState>> RootState { get; set; }

        public void OnUpdate()
        {
            RootState?.OnUpdate();
        }

    }
}