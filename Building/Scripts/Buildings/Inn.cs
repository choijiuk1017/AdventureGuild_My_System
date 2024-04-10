using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Manager;

namespace Core.Building.Inn
{
    public class Inn : Building
    {
        public Transform InnInside;
        public Transform main;


        void Start()
        {
            Init(4);
        }

        public override void BuildingMouseClick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                // Ray�� �浹�� Collider2D�� �ִٸ�
                if (hit.collider != null && hit.collider.name == "Inn")
                {
                    MoveCamera(InnInside.transform.position);
                }
            }
        }

        void Update()
        {
            BuildingMouseClick();
        }

        protected IEnumerator HandlingAdventure(GameObject adventure)
        {
            adventureInside = true;


            yield return new WaitForSeconds(7f);

            Debug.Log("���谡 ���� ����");

            adventure.transform.position = main.transform.position;

            adventureInside = false;

        }


        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Adventure") )
            {

                Debug.Log("���谡 ���� ����");
                col.transform.position = InnInside.transform.position;

                StartCoroutine(HandlingAdventure(col.gameObject));
            }
        }
    }
}


