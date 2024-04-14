using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Manager;

namespace Core.Building.Temple
{
    public class Temple : Building
    {

        public Transform templeInside;
        public Transform main;

        public int currentAdventure = 0;
        public int maxAdventure = 3;

        // Start is called before the first frame update
        void Start()
        {
            Init(2);
        }



        public override void BuildingMouseClick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                // Ray가 충돌한 Collider2D가 있다면
                if (hit.collider != null && hit.collider.name == "Temple")
                {
                    MoveCamera(templeInside.transform.position);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            BuildingMouseClick();
        }

        //신전 사용 부분
        private IEnumerator TempleTime(GameObject adventurePosition)
        {
            adventureInside = true;

            PerformActionBasedOnBuildingType(buildingData.buildingType, buildingData.buildingValue);

            yield return new WaitForSeconds(buildingData.buildingTime);

            Debug.Log("모험가 신전 퇴장");
            currentAdventure--;
            adventurePosition.transform.position = main.transform.position;

            adventurePosition.GetComponent<TestingAdventure>().isInBuilding = false;
            adventureInside = false;


        }


        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Adventure") && currentAdventure <= maxAdventure)
            {
                currentAdventure++;
                Debug.Log("모험가 신전 입장");
                col.transform.position = templeInside.transform.position;

                StartCoroutine(TempleTime(col.gameObject));
            }
        }
    }

}

