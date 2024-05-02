using System.Collections;
using Core.Unit.FSM;
using UnityEngine;

namespace Core.Unit.State.Adventure
{
    public class ExitState : State<AdventureAI>
    {
        public override void Enter(AdventureAI entity)
        {
            gameObject.SetActive(false);
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
