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

        // Start is called before the first frame update
        new void Start()
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

            PurchaseEquipment();
            RepairEquipment();

            yield return new WaitForSeconds(7f);
            adventure.transform.position = smithyEntrance.position;

            Debug.Log("���谡 ���尣 ����");

            SetLayerRecursively(adventure, LayerMask.NameToLayer("Adventure"));


            adventure.GetComponent<TestingAdventure>().isInBuilding = false;

            adventure.GetComponent<TestingAdventure>().isSetDestination = false;

            adventureInside = false;
   
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Adventure"))
            {
                Debug.Log("���谡 ���尣 ����");

                SetLayerRecursively(col.gameObject, LayerMask.NameToLayer("Invisible"));

                StartCoroutine(HandlingAdventure(col.gameObject));
            }
        }
    }

}

