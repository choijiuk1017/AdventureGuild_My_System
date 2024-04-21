using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Manager;


namespace Core.Building.Smithy
{

    public class Smithy : Building
    {

        public Transform smithyEntrance;
        public Transform main;

        // Start is called before the first frame update
        void Start()
        {
            Init(3);
        }



        private void PurchaseEquipment()
        {
            Debug.Log("��� ����");
        }

        private void RepairEquipment()
        {
            Debug.Log("��� ����");
        }

        protected IEnumerator HandlingAdventure(GameObject adventure)
        {
            adventureInside = true;

            SetLayerRecursively(adventure, LayerMask.NameToLayer("Invisible"));


            PurchaseEquipment();
            RepairEquipment();

            yield return new WaitForSeconds(7f);
            adventure.transform.position = smithyEntrance.position + new Vector3(0, -1, 0);

            Debug.Log("���谡 ���尣 ����");

            adventure.SetActive(true);

            adventure.GetComponent<TestingAdventure>().isInBuilding = false;

            adventure.GetComponent<TestingAdventure>().isSetDestination = false;

            adventureInside = false;
   
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Adventure") && col.GetComponent<TestingAdventure>().isInBuilding)
            {
                Debug.Log("���谡 ���尣 ����");

                col.gameObject.GetComponentInChildren<Renderer>().enabled = false;

                StartCoroutine(HandlingAdventure(col.gameObject));
            }
        }
    }

}

