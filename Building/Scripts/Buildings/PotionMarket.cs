using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Manager;

namespace Core.Building.PotionMarket
{
    public class PotionMarket : Building
    {
        public Transform potionMarketEntrance;

        // Start is called before the first frame update
        new void Start()
        {
            Init(5);
        }


        protected IEnumerator HandlingAdventure(GameObject adventure)
        {
            adventureInside = true;


            yield return new WaitForSeconds(7f);

            Debug.Log("���谡 ���� ���� ����");

            SetLayerRecursively(adventure, LayerMask.NameToLayer("Adventure"));

            adventure.transform.position = potionMarketEntrance.position;

            adventure.GetComponent<TestingAdventure>().isInBuilding = false;
            adventure.GetComponent<TestingAdventure>().isSetDestination = false;


            adventureInside = false;

        }


        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Adventure"))
            {
                Debug.Log("���谡 ���� ���� ����");

                SetLayerRecursively(col.gameObject, LayerMask.NameToLayer("Invisible"));

                StartCoroutine(HandlingAdventure(col.gameObject));
            }
        }

    }
}

