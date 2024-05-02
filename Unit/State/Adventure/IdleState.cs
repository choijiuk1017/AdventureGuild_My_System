using System;
using Core.Guild;
using Core.Manager;
using Core.Unit.FSM;
using UnityEngine;

namespace Core.Unit.State.Adventure
{
    public class IdleState : State<AdventureAI>
    {
        private const string animationName = "idle";
        
        public override void Enter(AdventureAI entity)
        {
            GuildEntity guildEntity = null;
            
            entity.unitAnimation.SetAnimation(animationName);

            if (entity.taskList.Count == 0)
                guildEntity = GuildManager.Instance.GetGuildEntity(GuildEntityType.Entrance);
            else
            {
                switch (entity.taskList[0].taskType)
                {
                    case TaskType.CheckQuest:
                        guildEntity = GuildManager.Instance.GetGuildEntity(GuildEntityType.NoticeBoard);
                        break;
                    case TaskType.Reception:
                        guildEntity = GuildManager.Instance.GetGuildEntity(GuildEntityType.ReceptionDesk);
                        break;
                    case TaskType.Temple:
                        break;
                    case TaskType.Shop:
                        break;
                    case TaskType.SellLoot:
                        break;
                }
                
                entity.taskList.RemoveAt(0);
            }

            if (guildEntity != null)
                guildEntity.ReadyForInteraction(entity.adventure);
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
