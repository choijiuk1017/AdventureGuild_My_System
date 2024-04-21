using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Manager;

namespace Core.Building.CentralSquare
{
    public class CentralSquare : Building
    {

        public Transform centralSquareEntrance;
        public Transform main;

        // Start is called before the first frame update
        void Start()
        {
            Init(8);
        }

        protected IEnumerator HandlingAdventure(GameObject adventure)
        {
            adventureInside = true;

            SetLayerRecursively(adventure, LayerMask.NameToLayer("Invisible"));


            yield return new WaitForSeconds(7f);

            Debug.Log("¸ðÇè°¡ Áß¾Ó ±¤Àå ÅðÀå");
            adventure.transform.position = centralSquareEntrance.position + new Vector3(0, -1, 0);

            adventure.SetActive(true);

            adventure.GetComponent<TestingAdventure>().isInBuilding = false;
            adventure.GetComponent<TestingAdventure>().isSetDestination = false;

            adventureInside = false;

        }


        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Adventure") && col.GetComponent<TestingAdventure>().isInBuilding)
            {
                Debug.Log("¸ðÇè°¡ Áß¾Ó±¤Àå ÀÔÀå");

                col.gameObject.GetComponentInChildren<Renderer>().enabled = false;

                StartCoroutine(HandlingAdventure(col.gameObject));
            }
        }
    }
}

