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
            int randomTickets = UnityEngine.Random.Range(1, 4); // 1���� 3 ������ ������ Ƽ�� ��
            currentTickets += randomTickets;
            Debug.Log(randomTickets + "���� Ƽ���� ��Ŀ���� �߰��Ǿ����ϴ�.");
        }

        //��Ŀ�� ��� �κ�
        private IEnumerator CircusTime(GameObject adventurePosition)
        {
            adventureInside = true;

            SetLayerRecursively(adventurePosition, LayerMask.NameToLayer("Invisible"));


            PerformActionBasedOnBuildingType(buildingData.buildingType, buildingData.buildingValue);

            yield return new WaitForSeconds(buildingData.buildingTime);

            Debug.Log("���谡 ��Ŀ�� ����");
            adventurePosition.transform.position = circusEntrance.position + new Vector3(0, -1, 0);

            adventurePosition.SetActive(true);
            adventurePosition.GetComponent<TestingAdventure>().isInBuilding = false;
            adventurePosition.GetComponent<TestingAdventure>().isSetDestination = false;
            adventureInside = false;

            

            if (currentTickets <= 0)
            {
                Debug.Log("Ƽ���� �����մϴ�.");
                // Ȥ�� �ٸ� ó���� �ص� ��
            }

        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Adventure") && currentTickets > 0 && col.GetComponent<TestingAdventure>().isInBuilding)
            {
                currentTickets--;
                Debug.Log("���谡 ��Ŀ�� ����");

                col.gameObject.GetComponentInChildren<Renderer>().enabled = false;

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
