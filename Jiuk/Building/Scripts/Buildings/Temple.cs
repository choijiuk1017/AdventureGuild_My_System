using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Manager;

namespace Core.Building.Temple
{
    public class Temple : Building
    {

        public Transform templeEntrance;
        public Transform main;

        public int currentAdventure = 0;
        public int maxAdventure = 3;

        // Start is called before the first frame update
        void Start()
        {
            Init(2);
        }


        //신전 사용 부분
        private IEnumerator TempleTime(GameObject adventurePosition)
        {
            adventureInside = true;

            SetLayerRecursively(adventurePosition, LayerMask.NameToLayer("Invisible"));


            PerformActionBasedOnBuildingType(buildingData.buildingType, buildingData.buildingValue);

            yield return new WaitForSeconds(buildingData.buildingTime);

            Debug.Log("모험가 신전 퇴장");

            adventurePosition.transform.position = templeEntrance.position + new Vector3(0, -1, 0);

            currentAdventure--;
            adventurePosition.SetActive(true);

            adventurePosition.GetComponent<TestingAdventure>().isInBuilding = false;
            adventurePosition.GetComponent<TestingAdventure>().isSetDestination = false;
            adventureInside = false;


        }


        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Adventure") && currentAdventure <= maxAdventure && col.GetComponent<TestingAdventure>().isInBuilding)
            {
                currentAdventure++;
                Debug.Log("모험가 신전 입장");
                col.gameObject.GetComponentInChildren<Renderer>().enabled = false;

                StartCoroutine(TempleTime(col.gameObject));
            }
        }
    }

}

