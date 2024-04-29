using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Unit;
using Core.Guild;
using Core.Manager;


namespace Core.Building.Circus
{
    public class Circus : Building
    {

        public int maxTickets = 3;
        public int currentTickets = 0;

        public Transform circusEntrance;
   
        // Start is called before the first frame update
        new void Start()
        {
            Init(1);
            DayCycle.OnDayChanged += HandleDayChanged;
        }

        protected override void InitEntity()
        {
            GuildManager.Instance.AddGuildEntity(GuildEntityType.Entrance, this);
        }

        public override void OnInteraction(Adventure adventureEntity)
        {
            if (currentTickets > 0)
            {
                StartCoroutine(UsingCircus(adventureEntity.gameObject));
            }
        }

        public override void EndInteraction()
        {

        }

        void AddRandomTickets()
        {
            int randomTickets = UnityEngine.Random.Range(1, 4); // 1에서 3 사이의 랜덤한 티켓 수
            currentTickets += randomTickets;
            Debug.Log(randomTickets + "개의 티켓이 서커스에 추가되었습니다.");
        }

        //서커스 사용 부분
        private IEnumerator UsingCircus(GameObject adventure)
        {
            adventureInside = true;

            currentTickets--;

            SetLayerRecursively(adventure, LayerMask.NameToLayer("Invisible"));

            var delayTime = buildingData.buildingTime * 60;

            yield return new WaitForSeconds(delayTime);

            PerformActionBasedOnBuildingType(adventure, buildingData.buildingType, buildingData.buildingValue);

            Debug.Log("모험가 서커스 퇴장");

            SetLayerRecursively(adventure, LayerMask.NameToLayer("Adventure"));

            adventure.transform.position = circusEntrance.position;

            adventure.GetComponent<TestingAdventure>().isInBuilding = false;
            adventure.GetComponent<TestingAdventure>().isSetDestination = false;
            adventureInside = false;

            

            if (currentTickets <= 0)
            {
                Debug.Log("티켓이 부족합니다.");
                // 혹은 다른 처리를 해도 됨
            }

        }


        //가격 변동에 따른 이벤트 함수
        private void OnDestroy()
        {
            // 스크립트가 소멸될 때 핸들러 제거
            ItemSetting.OnPriceChanged -= HandleDayChanged;
        }

        //가격 변동에 따른 이벤트 함수
        private void HandleDayChanged()
        {

            AddRandomTickets();
        }
    }

}
