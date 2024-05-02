using System;
using System.Collections.Generic;
using Core.Unit.FSM;
using Core.Unit.State.Adventure;
using UnityEngine;

namespace Core.Unit
{
    public enum AdventureStateType
    {
        Init, Enter, Exit, Idle, Move, Reception, Interaction, Last
    }

    public class AdventureAI : MonoBehaviour
    {
        private AdventureFSM<AdventureAI> fsm;
        private State<AdventureAI>[] states;
        [SerializeField] private AdventureStateType prevState;
        
        public Adventure adventure;

        public UnitAnimation unitAnimation;

        public GameObject targetObject;

        public List<TaskData> taskList;
    
        // Start is called before the first frame update
        private void Start()
        {
            unitAnimation = GetComponent<UnitAnimation>();
            adventure = GetComponent<Adventure>();

            fsm = new AdventureFSM<AdventureAI>();
            states = new State<AdventureAI>[((int)AdventureStateType.Last)];

            states[((int)AdventureStateType.Init)] = GetComponent<InitState>();
            states[((int)AdventureStateType.Enter)] = GetComponent<EnterState>();
            states[((int)AdventureStateType.Exit)] = GetComponent<ExitState>();
            states[((int)AdventureStateType.Idle)] = GetComponent<IdleState>();
            states[((int)AdventureStateType.Move)] = GetComponent<MoveState>();
            states[((int)AdventureStateType.Reception)] = GetComponent<ReceptionState>();
            states[((int)AdventureStateType.Interaction)] = GetComponent<InteractionState>();
            
            fsm.Init(this, states[(int)AdventureStateType.Init]);
        }

        private void Update()
        {
            fsm.StateUpdate();
        }

        public void ChangeState(AdventureStateType newState)
        {
            if (newState != AdventureStateType.Move)
                prevState = newState;
            
            fsm.ChangeState(states[(int)newState]);
        }
    }

    public enum TaskType
    {
        CheckQuest, Reception, Temple, Shop, SellLoot
    }

    [Serializable]
    public class TaskData
    {
        public TaskType taskType;
    }
}