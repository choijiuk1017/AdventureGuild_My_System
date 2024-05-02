using System.Collections;
using System.Collections.Generic;
using Core.Battle;
using Core.Guild;
using Core.Manager;
using Core.Unit.Body;
using UnityEngine;

namespace Core.Unit
{
    public class Adventure : Unit
    {
        [SerializeField] private AdventureInfo adventureInfo;
        private AdventureAI adventureAI;
        private BattleStats battleStats;
        private bool isQuestActive;

        public AdventureAI AdventureAI => adventureAI;
        public AdventureInfo AdventureInfo => adventureInfo;
        public BattleStats BattleStats => battleStats;

        [SerializeField] private List<QuestManager.QuestData> acceptedQuest;

        protected override void Init()
        {
            base.Init();
            
            adventureInfo = RecruitAdventure.CreateAdventureInfo();
            adventureAI = GetComponent<AdventureAI>();
            battleStats = new BattleStats();

            //FindObjectOfType<ReceptionDesk>().AddWaitingList(this);
            
            acceptedQuest = new List<QuestManager.QuestData>();
            SetStat(adventureInfo.AdventureStat);
        }

        public void AcceptQuest(QuestManager.QuestData newQuest)
        {
            acceptedQuest.Add(newQuest);
        }

        public void SetQuestActive(bool isQuestActive)
        {
            this.isQuestActive = isQuestActive;
        }

        public bool IsQuestActive()
        {
            return isQuestActive;
        }
    }
}
