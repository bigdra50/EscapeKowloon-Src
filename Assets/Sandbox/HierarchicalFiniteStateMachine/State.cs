using System;
using System.Collections.Generic;

namespace Sandbox.HierarchicalFiniteStateMachine
{
    public abstract class State<TState, TModel> where TState: Enum
    {
        protected TModel _model;
        protected TState _currentChildStateId;
        protected State<TState, TModel> _currentChildState;

        public State<TState, TModel> Parent { get; set; }
        public IDictionary<TState, State<TState, TModel>> ChildStateTable { get; } = new Dictionary<TState, State<TState, TModel>>();

        public State<TState, TModel> ChildState => this._currentChildState;

        public State(TModel model)
        {
            this._model = model;
        }

        public abstract void OnEnter();
        public abstract void OnUpdate();
        public abstract void OnExit();

        public void AddChildState(TState stateId, State<TState, TModel> state)
        {
            state.Parent = this.Parent;
            this.ChildStateTable[stateId] = state;
        }

        public void ChangeState(TState nextStatus)
        {
            if (this.Parent == null)
            {
                throw new InvalidOperationException("Not set parent");
            }
            this.Parent.ChangeChildState(nextStatus);
        }

        public void ChangeChildState(TState nextStatus)
        {
            if (!this.ChildStateTable.ContainsKey(nextStatus))
            {
                throw new InvalidOperationException($"Can not transit state.{nextStatus.ToString()}");
            }

            var childs = new List<State<TState, TModel>>();
            var tempState = this._currentChildState;
            while (tempState != null)
            {
                childs.Insert(0, tempState);
                tempState = tempState.ChildState;
            }

            foreach (var c in childs)
            {
                c.OnExit();
            }

            _currentChildStateId = nextStatus;
            _currentChildState = ChildStateTable[nextStatus];
            _currentChildState.OnEnter();

            //_currentChildState.OnUpdate();
        }
    }
}