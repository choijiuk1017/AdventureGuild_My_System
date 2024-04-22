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


        // Start is called before the first frame update
        new void Start()
        {
            Init(7);
        }


        protected IEnumerator HandlingAdventure(GameObject adventure)
        {
            adventureInside = true;

            yield return new WaitForSeconds(7f);

            Debug.Log("모험가 도서관 퇴장");

            SetLayerRecursively(adventure, LayerMask.NameToLayer("Adventure"));
            adventure.transform.position = libraryEntrance.position;


            adventure.GetComponent<TestingAdventure>().isInBuilding = false;
            adventure.GetComponent<TestingAdventure>().isSetDestination = false;

            adventureInside = false;

        }


        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Adventure"))
            {
                Debug.Log("모험가 도서관 입장");

                SetLayerRecursively(col.gameObject, LayerMask.NameToLayer("Invisible"));

                StartCoroutine(HandlingAdventure(col.gameObject));
            }
        }
    }

}


