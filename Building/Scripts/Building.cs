using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Unit;
using Core.Guild;
using Core.Manager;

namespace Core.Building
{
    public class Building : GuildEntity
    {
        protected Camera mainCamera;

        public BuildingManager.BuildingData buildingData;

        [SerializeField]
        protected bool adventureInside = false;

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        protected override void InitEntity()
        {

        }

        public override void OnInteraction(Adventure adventureEntity)
        {

        }

        public override void EndInteraction()
        {

        }


        protected void Init(int buildingID)
        {
            // �� �ǹ� Ŭ�������� buildingData�� �ʱ�ȭ�ϵ��� ����
            BuildingManager buildingManager = GameObject.FindObjectOfType<BuildingManager>();
            if (buildingManager != null)
            {
                buildingData = buildingManager.GetBuildingData(buildingID);
                Debug.Log("���� ����" + buildingData.buildingName);
                // ���⼭ ���� ������ �ǹ� Ŭ���� ���ο��� ����� �� ����
            }
            else
            {
                Debug.LogError("BuildingManager�� ���� �����ϴ�.");
            }
        }

        public void CollectTax(int tax)
        {
            Debug.Log("���� ¡��: " + tax);
        }
        public void PaymentSalary(int salary)
        {
            Debug.Log("���� ����: " + salary);
        }

        //���� ����
        public void PerformActionBasedOnBuildingType(GameObject adventure, int buildingType, int buildingValue)
        {
            switch (buildingType)
            {
                case 1:
                    // Building_Type�� 1�� ���, ������ ȸ���ϴ� ��� ����
                    RestoreWillingness(adventure, buildingValue);
                    break;
                case 2:
                    // Building_Type�� 2�� ���, ü�°� ������ ȸ���ϴ� ��� ����
                    RestoreHPAndMP(adventure, buildingValue);
                    break;
                // �ٸ� Building_Type�� ���� ó�� �߰�
                default:
                    // ��Ÿ �ൿ
                    break;
            }
        }

        private void RestoreWillingness(GameObject adventure, int value)
        {
            Debug.Log("������ ȸ���մϴ�: " + value);
        }

        private void RestoreHPAndMP(GameObject adventure, int value)
        {
            adventure.GetComponent<Adventure>().AdventureInfo.AdventureStat.curHp += value;
            adventure.GetComponent<Adventure>().AdventureInfo.AdventureStat.curMp += value;
            Debug.Log("ü�°� ������ ȸ���մϴ�: " + value);
        }

        public void SetLayerRecursively(GameObject obj, int newLayer)
        {
            if (obj == null)
                return;

            obj.layer = newLayer;
            foreach (Transform child in obj.transform)
            {
                SetLayerRecursively(child.gameObject, newLayer);
            }
        }

    }
}

