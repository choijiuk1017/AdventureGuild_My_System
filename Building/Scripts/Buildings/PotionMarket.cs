using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Manager;

namespace Core.Building.PotionMarket
{
    public class PotionMarket : Building
    {
        public Transform potionMarketInside;
        public Transform main;

        // Start is called before the first frame update
        void Start()
        {
            Init(5);
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
                if (hit.collider != null && hit.collider.name == "PotionMarket")
                {
                    MoveCamera(potionMarketInside.transform.position);
                }

            }
        }

        protected IEnumerator HandlingAdventure(GameObject adventure)
        {
            adventureInside = true;


            yield return new WaitForSeconds(7f);

            Debug.Log("모험가 포션 상점 퇴장");

            adventure.transform.position = main.transform.position;

            adventureInside = false;

        }


        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Adventure"))
            {
                Debug.Log("모험가 포션 상점 입장");
                col.transform.position = potionMarketInside.transform.position;

                StartCoroutine(HandlingAdventure(col.gameObject));
            }
        }

    }
}

