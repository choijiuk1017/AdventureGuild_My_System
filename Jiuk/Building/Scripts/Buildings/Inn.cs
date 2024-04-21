using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Manager;

namespace Core.Building.Inn
{
    public class Inn : Building
    {
        public Transform InnEntrance;
        public Transform main;


        void Start()
        {
            Init(5);
        }

        protected IEnumerator HandlingAdventure(GameObject adventure)
        {
            adventureInside = true;

            SetLayerRecursively(adventure, LayerMask.NameToLayer("Invisible"));


            yield return new WaitForSeconds(7f);

            Debug.Log("모험가 주점 퇴장");
            adventure.transform.position = InnEntrance.position + new Vector3(0, -1, 0);

            adventure.SetActive(true);
            adventure.GetComponent<TestingAdventure>().isInBuilding = false;
            adventure.GetComponent<TestingAdventure>().isSetDestination = false;


            adventureInside = false;

        }


        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Adventure") && col.GetComponent<TestingAdventure>().isInBuilding)
            {

                Debug.Log("모험가 주점 입장");

                StartCoroutine(HandlingAdventure(col.gameObject));
            }
        }
    }
}


