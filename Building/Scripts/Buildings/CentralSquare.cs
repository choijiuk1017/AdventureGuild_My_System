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

        // Start is called before the first frame update
        new void Start()
        {
            Init(8);
        }

        protected IEnumerator HandlingAdventure(GameObject adventure)
        {
            adventureInside = true;

           yield return new WaitForSeconds(7f);

            Debug.Log("¸ðÇè°¡ Áß¾Ó ±¤Àå ÅðÀå");

            SetLayerRecursively(adventure, LayerMask.NameToLayer("Adventure"));
            adventure.transform.position = centralSquareEntrance.position;

            adventure.GetComponent<TestingAdventure>().isInBuilding = false;
            adventure.GetComponent<TestingAdventure>().isSetDestination = false;

            adventureInside = false;

        }


        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Adventure"))
            {
                Debug.Log("¸ðÇè°¡ Áß¾Ó±¤Àå ÀÔÀå");

                SetLayerRecursively(col.gameObject, LayerMask.NameToLayer("Invisible"));

                StartCoroutine(HandlingAdventure(col.gameObject));
            }
        }
    }
}

