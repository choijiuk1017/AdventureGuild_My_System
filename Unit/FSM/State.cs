using UnityEngine;

namespace Core.Unit.FSM
{
    public abstract class State<T> : MonoBehaviour
    {
        protected float elapsedTime;
        public abstract void Enter(T entity);
        public abstract void Execute(T entity);
        public abstract void Exit(T entity);
        public abstract void OnTransition(T entity);
    }
}