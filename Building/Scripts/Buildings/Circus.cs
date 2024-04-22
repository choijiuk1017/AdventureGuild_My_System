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
   
        // Start is called before the first frame update
        new void Start()
        {
            Init(1);
            DayCycle.OnDayChanged += HandleDayChanged;
        }

        void AddRandomTickets()
        {
            int randomTickets = UnityEngine.Random.Range(1, 4); // 1���� 3 ������ ������ Ƽ�� ��
            currentTickets += randomTickets;
            Debug.Log(randomTickets + "���� Ƽ���� ��Ŀ���� �߰��Ǿ����ϴ�.");
        }

        //��Ŀ�� ��� �κ�
        private IEnumerator CircusTime(GameObject adventure)
        {
            adventureInside = true;

            //PerformActionBasedOnBuildingType(buildingData.buildingType, buildingData.buildingValue);

            yield return new WaitForSeconds(buildingData.buildingTime);

            Debug.Log("���谡 ��Ŀ�� ����");

            SetLayerRecursively(adventure, LayerMask.NameToLayer("Adventure"));

            adventure.transform.position = circusEntrance.position;

            adventure.GetComponent<TestingAdventure>().isInBuilding = false;
            adventure.GetComponent<TestingAdventure>().isSetDestination = false;
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

                SetLayerRecursively(col.gameObject, LayerMask.NameToLayer("Invisible"));

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
