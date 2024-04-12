using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Manager;

namespace Core.Building.TrainingCenter
{
    public class TrainingCenter : Building
    {

        public Transform trainingCenterInside;
        public Transform main;
        // Start is called before the first frame update
        void Start()
        {
            Init(6);
        }

        public override void BuildingMouseClick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                // Ray�� �浹�� Collider2D�� �ִٸ�
                if (hit.collider != null && hit.collider.name == "TrainingCenter")
                {
                    MoveCamera(trainingCenterInside.transform.position);
                }

            }
        }

        // Update is called once per frame
        void Update()
        {
            BuildingMouseClick();
        }

        protected IEnumerator HandlingAdventure(GameObject adventure)
        {
            adventureInside = true;


            yield return new WaitForSeconds(7f);

            Debug.Log("���谡 �Ʒü� ����");

            adventure.transform.position = main.transform.position;

            adventureInside = false;

        }


        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Adventure"))
            {
                Debug.Log("���谡 �Ʒü� ����");
                col.transform.position = trainingCenterInside.transform.position;

                StartCoroutine(HandlingAdventure(col.gameObject));
            }
        }
    }


}

