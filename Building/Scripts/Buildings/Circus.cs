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

        public Transform circusInside;
        public Transform main;
   
        // Start is called before the first frame update
        void Start()
        {
            Init(1);
            DayCycle.OnDayChanged += HandleDayChanged;
        }



        public override void BuildingMouseClick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                // Ray가 충돌한 Collider2D가 있다면
                if (hit.collider != null && hit.collider.name == "Circus")
                {
                    MoveCamera(circusInside.transform.position);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            BuildingMouseClick();
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

            PerformActionBasedOnBuildingType(buildingData.buildingType, buildingData.buildingValue);

            yield return new WaitForSeconds(buildingData.buildingTime);

            Debug.Log("모험가 퇴장");

            adventurePosition.transform.position = main.transform.position;
            adventureInside = false;

            

            if (currentTickets <= 0)
            {
                Debug.Log("티켓이 부족합니다.");
                // 혹은 다른 처리를 해도 됨
            }

        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Adventure") && currentTickets > 0)
            {
                currentTickets--;
                Debug.Log("모험가 서커스 입장");
                col.transform.position = circusInside.transform.position;

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
