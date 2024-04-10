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

                // Ray�� �浹�� Collider2D�� �ִٸ�
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
            int randomTickets = UnityEngine.Random.Range(1, 4); // 1���� 3 ������ ������ Ƽ�� ��
            currentTickets += randomTickets;
            Debug.Log(randomTickets + "���� Ƽ���� ��Ŀ���� �߰��Ǿ����ϴ�.");
        }

        //��Ŀ�� ��� �κ�
        private IEnumerator CircusTime(GameObject adventurePosition)
        {
            adventureInside = true;

            PerformActionBasedOnBuildingType(buildingData.buildingType, buildingData.buildingValue);

            yield return new WaitForSeconds(buildingData.buildingTime);

            Debug.Log("���谡 ����");

            adventurePosition.transform.position = main.transform.position;
            adventureInside = false;

            

            if (currentTickets <= 0)
            {
                Debug.Log("Ƽ���� �����մϴ�.");
                // Ȥ�� �ٸ� ó���� �ص� ��
            }

        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Adventure") && currentTickets > 0)
            {
                currentTickets--;
                Debug.Log("���谡 ��Ŀ�� ����");
                col.transform.position = circusInside.transform.position;

                StartCoroutine(CircusTime(col.gameObject));

                
            }
        }

        //���� ������ ���� �̺�Ʈ �Լ�
        private void OnDestroy()
        {
            // ��ũ��Ʈ�� �Ҹ�� �� �ڵ鷯 ����
            ItemSetting.OnPriceChanged -= HandleDayChanged;
        }

        //���� ������ ���� �̺�Ʈ �Լ�
        private void HandleDayChanged()
        {

            AddRandomTickets();
        }
    }

}
