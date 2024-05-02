namespace Core.Unit.FSM
{
    public class AdventureFSM<T> where T : class 
    {
        T entity;
        State<T> curState;
        State<T> prevState;

        public void Init(T entitiy, State<T> state)
        {
            this.entity = entitiy;
            curState = state;
            ChangeState(state);
        }

        public void ChangeState(State<T> newState)
        {
            prevState = curState;
            if(curState != null)
                curState.Exit(entity);
            curState = newState; 
            curState.Enter(entity);
        }

        public void StateUpdate()
        {
            if (curState != null)
            {
                curState.Execute(entity);
                curState.OnTransition(entity);
            }            
        }

        public State<T> GetPrevState()
        {
            return prevState;
        }
    }
}
