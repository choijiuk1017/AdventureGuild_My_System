using Core.Unit.FSM;

namespace Core.Unit.State.Adventure
{
    public class EnterState : State<AdventureAI>
    {
        public override void Enter(AdventureAI entity)
        {
            if (entity.adventure.IsQuestActive())
            {
                if (entity.adventure.GetStat().hp < 100)
                {
                    // TaskData에 추가 의료소
                    NewTask(entity, TaskType.Temple);
                }

                NewTask(entity, TaskType.SellLoot);
            }
            
            NewTask(entity, TaskType.CheckQuest);
            //NewTask(entity, TaskType.Shop);
            
            entity.ChangeState(AdventureStateType.Idle);
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

        private void NewTask(AdventureAI entity, TaskType newTaskType)
        {
            var newTask = new TaskData
            {
                taskType = newTaskType
            };

            entity.taskList.Add(newTask);
        }
    }
}
