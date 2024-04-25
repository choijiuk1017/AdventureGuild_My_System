using System.Collections;
using System.Collections.Generic;
using Core.Manager;
using Core.Unit;
using UnityEngine;
using QuestData = Core.Manager.QuestManager.QuestData;

namespace Core.Guild.Quest
{
    public class NoticeBoard : GuildEntity
    {
        [SerializeField] private List<QuestData> questList;
        protected override void InitEntity()
        {
            GuildManager.Instance.AddGuildEntity(GuildEntityType.NoticeBoard, this);
            questList = new List<QuestData>();
        }

        public override void OnInteraction(Adventure adventureEntity)
        {
            var quest = GetQuest();

            StartCoroutine(WaitForQuestSelection(adventureEntity, quest));
        }

        public override void EndInteraction()
        {
            
        }

        public void AddQuest(QuestData newQuest)
        {
            questList.Add(newQuest);
        }

        public QuestData GetQuest()
        {
            var quest = questList[0];

            questList.RemoveAt(0);

            return quest;
        }

        private IEnumerator WaitForQuestSelection(Adventure adventure, QuestData questData)
        {
            var delayTime = Random.Range(2f, 5f);

            Debug.Log("보는중");
            yield return new WaitForSeconds(delayTime);
            Debug.Log("완료");
            adventure.AcceptQuest(questData);
            adventure.AdventureAI.ChangeState(AdventureStateType.Idle);
        }
    }
}
