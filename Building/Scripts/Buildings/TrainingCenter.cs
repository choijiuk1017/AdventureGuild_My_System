using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Manager;

namespace Core.Building.TrainingCenter
{
    public class TrainingCenter : Building
    {

        public Transform trainingEntrance;


        // Start is called before the first frame update
        new void Start()
        {
            Init(6);
        }

        protected IEnumerator HandlingAdventure(GameObject adventure)
        {
            adventureInside = true;

            yield return new WaitForSeconds(7f);

            Debug.Log("모험가 훈련소 퇴장");
            SetLayerRecursively(adventure, LayerMask.NameToLayer("Adventure"));

            adventure.transform.position = trainingEntrance.position;

            adventure.GetComponent<TestingAdventure>().isInBuilding = false;
            adventure.GetComponent<TestingAdventure>().isSetDestination = false;

            adventureInside = false;

        }


        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Adventure"))
            {
                Debug.Log("모험가 훈련소 입장");

                SetLayerRecursively(col.gameObject, LayerMask.NameToLayer("Invisible"));

                StartCoroutine(HandlingAdventure(col.gameObject));
            }
        }
    }


}

