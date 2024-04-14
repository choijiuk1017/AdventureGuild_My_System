using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Manager;


namespace Core.Building.Smithy
{

    public class Smithy : Building
    {

        public Transform smithyInside;
        public Transform main;

        // Start is called before the first frame update
        void Start()
        {
            Init(3);
        }

        // Update is called once per frame
        void Update()
        {
            BuildingMouseClick();
        }

        public override void BuildingMouseClick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                // Ray가 충돌한 Collider2D가 있다면
                if (hit.collider != null && hit.collider.name == "Smithy")
                {
                    MoveCamera(smithyInside.transform.position);
                }
            }
        }

        private void PurchaseEquipment()
        {
            Debug.Log("장비 구매");
        }

        private void RepairEquipment()
        {
            Debug.Log("장비 수리");
        }

        protected IEnumerator HandlingAdventure(GameObject adventure)
        {
            adventureInside = true;

            PurchaseEquipment();
            RepairEquipment();

            yield return new WaitForSeconds(7f);

            Debug.Log("모험가 대장간 퇴장");

            adventure.transform.position = main.transform.position;

            adventure.GetComponent<TestingAdventure>().isInBuilding = false;

            adventureInside = false;
   
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Adventure"))
            {
                Debug.Log("모험가 대장간 입장");

                col.transform.position = smithyInside.transform.position;

               StartCoroutine(HandlingAdventure(col.gameObject));
            }
        }
    }

}

