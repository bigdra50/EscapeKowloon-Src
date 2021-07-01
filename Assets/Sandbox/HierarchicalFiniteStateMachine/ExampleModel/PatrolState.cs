namespace Sandbox.HierarchicalFiniteStateMachine.ExampleModel
{
    public class PatrolState: State<ModelState, ExampleModel>
    {
        public PatrolState(ExampleModel model) : base(model)
        {
        }

        public override void OnEnter()
        {
            ChangeState(ModelState.Move);
        }

        public override void OnUpdate()
        {
            if (_model.IsInsightTarget())
            {
                ChangeState(ModelState.Battle);
            }
            else
            {
                ChildState.OnUpdate();
            }
        }

        public override void OnExit()
        {
        }
    }
}