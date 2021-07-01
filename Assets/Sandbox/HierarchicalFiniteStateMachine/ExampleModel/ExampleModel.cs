using System.Numerics;

namespace Sandbox.HierarchicalFiniteStateMachine.ExampleModel
{
    public class ExampleModel: Model<ModelState> 
    {
        
        public Vector3 Position { get; set; }
        
        
        public bool IsInsightTarget()
        {
            return true;
        }

        public bool IsReachableTarget()
        {
            return true;
        }

        public void Move() { }
        public void ApproachTarget(){}
        public void Attack(){}
    }
}