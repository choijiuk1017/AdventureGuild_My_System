using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Manager;


namespace Core.Building.Circus
{
    public class Circus : Building
    {

        public int maxTickets = 3;
        public int currentTickets = 0;

        public Transform circusEntrance;
        public Transform main;
   
        // Start is called before the first frame update
        void Start()
        {
            Init(1);
            DayCycle.OnDayChanged += HandleDayChanged;
        }

        void AddRandomTickets()
        {
            int randomTickets = UnityEngine.Random.Range(1, 4); // 1에서 3 사이의 랜덤한 티켓 수
            currentTickets += randomTickets;
            Debug.Log(randomTickets + "개의 티켓이 서커스에 추가되었습니다.");
        }

        //서커스 사용 부분
        private IEnumerator CircusTime(GameObject adventurePosition)
        {
            adventureInside = true;

            SetLayerRecursively(adventurePosition, LayerMask.NameToLayer("Invisible"));


            PerformActionBasedOnBuildingType(buildingData.buildingType, buildingData.buildingValue);

            yield return new WaitForSeconds(buildingData.buildingTime);

            Debug.Log("모험가 서커스 퇴장");
            adventurePosition.transform.position = circusEntrance.position + new Vector3(0, -1, 0);

            adventurePosition.SetActive(true);
            adventurePosition.GetComponent<TestingAdventure>().isInBuilding = false;
            adventurePosition.GetComponent<TestingAdventure>().isSetDestination = false;
            adventureInside = false;

            

            if (currentTickets <= 0)
            {
                Debug.Log("티켓이 부족합니다.");
                // 혹은 다른 처리를 해도 됨
            }

        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Adventure") && currentTickets > 0 && col.GetComponent<TestingAdventure>().isInBuilding)
            {
                currentTickets--;
                Debug.Log("모험가 서커스 입장");

                col.gameObject.GetComponentInChildren<Renderer>().enabled = false;

                StartCoroutine(CircusTime(col.gameObject));

                
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
