using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Manager;

namespace Core.Building
{
    public class Building : MonoBehaviour
    {
        protected Camera mainCamera;

        public BuildingData buildingData;

        private void Awake()
        {
            mainCamera = Camera.main;


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

        //건물 마우스 클릭 시
        public virtual void BuildingMouseClick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                // Ray가 충돌한 Collider2D가 있다면
                if (hit.collider != null && hit.collider == this)
                {
                    MoveCamera(new Vector2 (0,0));
                }
            }
        }


        //카메라 이동 함수
        protected void MoveCamera(Vector2 targetPosition)
        {
            mainCamera.transform.position = targetPosition;
        }

        public void CollectTax(int tax)
        {
            Debug.Log("세금 징수: " + tax);
        }
        public void PaymentSalary(int salary)
        {
            Debug.Log("월급 지급: " + salary);
        }

        public void PerformActionBasedOnBuildingType(int buildingType, int buildingValue)
        {
            switch (buildingType)
            {
                case 1:
                    // Building_Type이 1인 경우, 의지를 회복하는 기능 수행
                    RestoreWillingness(buildingValue);
                    break;
                case 2:
                    // Building_Type이 2인 경우, 체력과 마나를 회복하는 기능 수행
                    RestoreHPAndMP(buildingValue);
                    break;
                // 다른 Building_Type에 대한 처리 추가
                default:
                    // 기타 행동
                    break;
            }
        }

        private void RestoreWillingness(int value)
        {
            Debug.Log("의지를 회복합니다: " + value);
        }

        private void RestoreHPAndMP(int value)
        {
            Debug.Log("체력과 마나를 회복합니다: " + value);
        }

    }
}

