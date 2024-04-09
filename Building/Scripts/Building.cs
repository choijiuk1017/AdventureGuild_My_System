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

        //�ǹ� ���콺 Ŭ�� ��
        public virtual void BuildingMouseClick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                // Ray�� �浹�� Collider2D�� �ִٸ�
                if (hit.collider != null && hit.collider == this)
                {
                    MoveCamera(new Vector2 (0,0));
                }
            }
        }


        //ī�޶� �̵� �Լ�
        protected void MoveCamera(Vector2 targetPosition)
        {
            mainCamera.transform.position = targetPosition;
        }

        public void CollectTax(int tax)
        {
            Debug.Log("���� ¡��: " + tax);
        }
        public void PaymentSalary(int salary)
        {
            Debug.Log("���� ����: " + salary);
        }

        public void PerformActionBasedOnBuildingType(int buildingType, int buildingValue)
        {
            switch (buildingType)
            {
                case 1:
                    // Building_Type�� 1�� ���, ������ ȸ���ϴ� ��� ����
                    RestoreWillingness(buildingValue);
                    break;
                case 2:
                    // Building_Type�� 2�� ���, ü�°� ������ ȸ���ϴ� ��� ����
                    RestoreHPAndMP(buildingValue);
                    break;
                // �ٸ� Building_Type�� ���� ó�� �߰�
                default:
                    // ��Ÿ �ൿ
                    break;
            }
        }

        private void RestoreWillingness(int value)
        {
            Debug.Log("������ ȸ���մϴ�: " + value);
        }

        private void RestoreHPAndMP(int value)
        {
            Debug.Log("ü�°� ������ ȸ���մϴ�: " + value);
        }

    }
}

