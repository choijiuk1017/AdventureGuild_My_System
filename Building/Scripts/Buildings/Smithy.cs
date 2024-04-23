using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Unit;
using Core.Guild;
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

        protected override void InitEntity()
        {
            GuildManager.Instance.AddGuildEntity(GuildEntityType.Entrance, this);
        }

        public override void OnInteraction(Adventure adventureEntity)
        {
            StartCoroutine(UsingSmithy(adventureEntity.gameObject));
        }

        public override void EndInteraction()
        {

        }


        private void PurchaseEquipment()
        {
            Debug.Log("장비 구매");
        }

        private void RepairEquipment()
        {
            Debug.Log("장비 수리");
        }

        protected IEnumerator UsingSmithy(GameObject adventure)
        {
            adventureInside = true;

            SetLayerRecursively(adventure, LayerMask.NameToLayer("Invisible"));

            PurchaseEquipment();
            RepairEquipment();

            yield return new WaitForSeconds(7f);

            adventure.transform.position = smithyEntrance.position;

            Debug.Log("모험가 대장간 퇴장");

            SetLayerRecursively(adventure, LayerMask.NameToLayer("Adventure"));


            adventure.GetComponent<TestingAdventure>().isInBuilding = false;

            adventure.GetComponent<TestingAdventure>().isSetDestination = false;

            adventureInside = false;
   
        }

    }

}

