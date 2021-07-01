using UnityEngine;

namespace Sandbox.HierarchicalFiniteStateMachine.ExampleModel
{
    public class ExampleNpc : MonoBehaviour
    {
        void Start()
        {
            var model = new ExampleModel();
            var patrol = new PatrolState(model);
            patrol.AddChildState(ModelState.Move, new PatrolState(model));

            //model.RootState.ChildStateTable[ModelState.Patrol] = patrol;
        }
    }
}