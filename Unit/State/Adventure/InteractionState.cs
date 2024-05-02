using Core.Guild;
using Core.Unit.FSM;

namespace Core.Unit.State.Adventure
{
    public class InteractionState : State<AdventureAI>
    {
        public override void Enter(AdventureAI entity)
        {
            if (entity.targetObject.TryGetComponent(out GuildEntity guildEntity))
            {
                guildEntity.OnInteraction(entity.adventure);
            }
        }

        public override void Execute(AdventureAI entity)
        {
            
        }

        public override void Exit(AdventureAI entity)
        {
            
        }

        public override void OnTransition(AdventureAI entity)
        {
            
        }
    }
}
