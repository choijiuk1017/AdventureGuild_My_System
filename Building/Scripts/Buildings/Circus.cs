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
            int randomTickets = UnityEngine.Random.Range(1, 4); // 1���� 3 ������ ������ Ƽ�� ��
            currentTickets += randomTickets;
            Debug.Log(randomTickets + "���� Ƽ���� ��Ŀ���� �߰��Ǿ����ϴ�.");
        }

        //��Ŀ�� ��� �κ�
        private IEnumerator UsingCircus(GameObject adventure)
        {
            adventureInside = true;

            currentTickets--;

            SetLayerRecursively(adventure, LayerMask.NameToLayer("Invisible"));

            var delayTime = buildingData.buildingTime * 60;

            yield return new WaitForSeconds(delayTime);

            PerformActionBasedOnBuildingType(adventure, buildingData.buildingType, buildingData.buildingValue);

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
