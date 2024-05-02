using Core.Guild.Furniture;
using Core.Guild.Quest;
using Core.Unit.FSM;
using UnityEngine;

namespace Core.Unit.State.Adventure
{
    public class InitState : State<AdventureAI>
    {
        public override void Enter(AdventureAI entity)
        {
            FindObjectOfType<ReceptionDesk>().AddWaitingList(entity.adventure);
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
