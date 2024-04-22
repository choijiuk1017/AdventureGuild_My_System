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
            // 각 건물 클래스에서 buildingData를 초기화하도록 변경
            BuildingManager buildingManager = GameObject.FindObjectOfType<BuildingManager>();
            if (buildingManager != null)
            {
                buildingData = buildingManager.GetBuildingData(buildingID);
                Debug.Log("정보 받음" + buildingData.buildingName);
                // 여기서 받은 정보를 건물 클래스 내부에서 사용할 수 있음
            }
            else
            {
                Debug.LogError("BuildingManager가 씬에 없습니다.");
            }
        }

        public void CollectTax(int tax)
        {
            Debug.Log("세금 징수: " + tax);
        }
        public void PaymentSalary(int salary)
        {
            Debug.Log("월급 지급: " + salary);
        }

        //스텟 증감
        public void PerformActionBasedOnBuildingType(GameObject adventure, int buildingType, int buildingValue)
        {
            switch (buildingType)
            {
                case 1:
                    // Building_Type이 1인 경우, 의지를 회복하는 기능 수행
                    RestoreWillingness(adventure, buildingValue);
                    break;
                case 2:
                    // Building_Type이 2인 경우, 체력과 마나를 회복하는 기능 수행
                    RestoreHPAndMP(adventure, buildingValue);
                    break;
                // 다른 Building_Type에 대한 처리 추가
                default:
                    // 기타 행동
                    break;
            }
        }

        private void RestoreWillingness(GameObject adventure, int value)
        {
            Debug.Log("의지를 회복합니다: " + value);
        }

        private void RestoreHPAndMP(GameObject adventure, int value)
        {
            adventure.GetComponent<Adventure>().AdventureInfo.AdventureStat.curHp += value;
            adventure.GetComponent<Adventure>().AdventureInfo.AdventureStat.curMp += value;
            Debug.Log("체력과 마나를 회복합니다: " + value);
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

