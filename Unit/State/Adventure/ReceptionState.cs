using Core.Manager;
using Core.Unit.FSM;

namespace Core.Unit.State.Adventure
{
    public class ReceptionState : State<AdventureAI>
    {
        private const string animationName = "idle";
        
        public override void Enter(AdventureAI entity)
        {
            entity.unitAnimation.SetAnimation(animationName);
            EventBus.Publish(EventType.Recruit);
        }

        public override void Execute(AdventureAI entity)
        {
            
        }

        public override void Exit(AdventureAI entity)
        {
            //randomPos or next Behavior
        }

        public override void OnTransition(AdventureAI entity)
        {
            
        }
    }
}
