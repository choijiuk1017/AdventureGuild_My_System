using System.Collections.Generic;
using Core.Manager;
using Core.UI;
using Core.Unit;
using UnityEngine;
using EventType = Core.Manager.EventType;

namespace Core.Guild.Furniture
{
    public class ReceptionDesk : GuildEntity
    {
        [SerializeField] private List<Adventure> waitingList;

        [SerializeField] private RecruitUI recruitUI;

        [SerializeField] private Adventure adventure;

        //[SerializeField] private List<QuestData> questList;

        protected override void InitEntity()
        {
            GuildManager.Instance.AddGuildEntity(GuildEntityType.ReceptionDesk, this);
            
            recruitUI = FindObjectOfType<RecruitUI>();
            recruitUI.ShowRecruitUI(false);
            waitingList = new List<Adventure>();
            
            EventBus.EventSubscribe(EventType.Recruit, ShowRecruitUI);
            EventBus.EventSubscribe(EventType.CompleteRecruit, Recruit);
            EventBus.EventSubscribe(EventType.FinishReception, Reception);
        }

        public void AddWaitingList(Adventure newAdventure)
        {
            waitingList.Add(newAdventure);
            Reception();
        }

        private void Reception()
        {
            if (waitingList.Count == 0)
                return;
            waitingList[0].AdventureAI.targetObject = gameObject;
            waitingList[0].AdventureAI.ChangeState(AdventureStateType.Move);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(waitingList.Count == 0) return;
            var adv = other.GetComponent<Adventure>();
            if (adv != waitingList[0]) return;
            
            Debug.Log("sadf");
            adventure = adv; 
            waitingList[0].AdventureAI.ChangeState(AdventureStateType.Reception);
            waitingList.RemoveAt(0);
        }

        public override void OnInteraction(Adventure adventureEntity)
        {
            
        }

        public override void EndInteraction()
        {
            
        }

        private void ShowRecruitUI()
        {
            recruitUI.SetRecruitData(adventure.AdventureInfo);
            recruitUI.ShowRecruitUI(true);
        }

        private void Recruit()
        {
            GuildManager.Instance.GetGuild().AddAdventure(adventure);
            adventure.AdventureAI.ChangeState(AdventureStateType.Enter);
        }
    }
}
