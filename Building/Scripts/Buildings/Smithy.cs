using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Unit;
using Core.Item;
using Core.Guild;
using Core.Manager;


namespace Core.Building.Smithy
{

    public class Smithy : Building
    {

        public Transform smithyEntrance;

        public List<string> equipmentOptions = new List<string> {"갑옷", "철 검", "철 방패"};

        public Inventory inventory;
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

        private void CraftEquipment(/*string bluePrint, string ore, string miscellaneous*/)
        {
            //아이템 조건 확인
            Debug.Log("장비 제작");
        }

        private void EnhanceEquipment(/*string equipment, int stoneCount*/)
        {
            //아이템 조건 확인
            Debug.Log("장비 강화");
        }
    

        protected IEnumerator UsingSmithy(GameObject adventure)
        {
            adventureInside = true;

            SetLayerRecursively(adventure, LayerMask.NameToLayer("Invisible"));

            CraftEquipment();
            EnhanceEquipment();

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

