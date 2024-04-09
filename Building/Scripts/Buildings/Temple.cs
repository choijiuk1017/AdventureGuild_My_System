using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Manager;

namespace Core.Building.Temple
{
    public class Temple : Building
    {
        public bool usingTemple = false;

        public Transform templeInside;
        public Transform main;

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("adsafa");
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

        //서커스 사용 부분
        private IEnumerator TempleTime(GameObject adventurePosition)
        {
            usingTemple = true;

            PerformActionBasedOnBuildingType(buildingData.buildingType, buildingData.buildingValue);

            yield return new WaitForSeconds(buildingData.buildingTime);

            Debug.Log("모험가 퇴장");

            adventurePosition.transform.position = main.transform.position;
            usingTemple = false;


        }


        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Adventure") )
            {

                Debug.Log("모험가 입장");
                col.transform.position = templeInside.transform.position;

                StartCoroutine(TempleTime(col.gameObject));
            }
        }
    }

}

