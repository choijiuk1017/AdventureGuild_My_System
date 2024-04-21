using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Manager;

namespace Core.Building.Library
{
    public class Library : Building
    {
        public Transform libraryEntrance;
        public Transform main;


        // Start is called before the first frame update
        void Start()
        {
            Init(7);
        }


        protected IEnumerator HandlingAdventure(GameObject adventure)
        {
            adventureInside = true;

            SetLayerRecursively(adventure, LayerMask.NameToLayer("Invisible"));


            yield return new WaitForSeconds(7f);

            Debug.Log("모험가 도서관 퇴장");
            adventure.transform.position = libraryEntrance.position + new Vector3(0, -1, 0);

            adventure.GetComponent<Renderer>().enabled = true;

            adventure.GetComponent<TestingAdventure>().isInBuilding = false;
            adventure.GetComponent<TestingAdventure>().isSetDestination = false;

            adventureInside = false;

        }


        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Adventure") && col.GetComponent<TestingAdventure>().isInBuilding)
            {
                Debug.Log("모험가 도서관 입장");
                col.gameObject.GetComponentInChildren<Renderer>().enabled = false;

                StartCoroutine(HandlingAdventure(col.gameObject));
            }
        }
    }

}


