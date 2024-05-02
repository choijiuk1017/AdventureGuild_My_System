using Core.Unit.FSM;
using UnityEngine;

namespace Core.Unit.State.Adventure
{
    public class MoveState : State<AdventureAI>
    {
        private GameObject targetObject;
        private const string animationName = "walk";
        private bool isRight = false;

        public override void Enter(AdventureAI entity)
        {
            targetObject = entity.targetObject;
            entity.unitAnimation.SetAnimation(animationName);
        }

        public override void Execute(AdventureAI entity)
        {
            if ((transform.position - targetObject.transform.position).magnitude < 1f)
            {
                entity.ChangeState(AdventureStateType.Interaction);
            }
            
            var x = targetObject.transform.position.x > transform.position.x ? 1 : -1;

            if (isRight)
            {
                if(x == -1)
                    Flip();
            }
            else
            {
                if(x == 1)
                    Flip();
            }
            
            transform.position += new Vector3(x, 0, 0) * Time.deltaTime;
        }

        public override void Exit(AdventureAI entity)
        {
            
        }

        public override void OnTransition(AdventureAI entity)
        {
            
        }

        private void Flip()
        {
            var scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            isRight = !isRight;
        }
    }
}
