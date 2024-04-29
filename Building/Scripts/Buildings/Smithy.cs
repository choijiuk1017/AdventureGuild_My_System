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

        public List<string> equipmentOptions = new List<string> {"갑옷", "철 검", "철 방패"};  

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

        private string ChooseRandomEquipment()
        {
            int randomIndex = UnityEngine.Random.Range(0, equipmentOptions.Count);
            return equipmentOptions[randomIndex];
        }

        private void PurchaseEquipment(string equipment)
        {
            Debug.Log("모험가가 " + equipment + "를(을) 구매했습니다.");
        }

        private void RepairEquipment()
        {
            Debug.Log("장비 수리");
        }

        protected IEnumerator UsingSmithy(GameObject adventure)
        {
            adventureInside = true;

            SetLayerRecursively(adventure, LayerMask.NameToLayer("Invisible"));

            string chosenEquipment = ChooseRandomEquipment();
            Debug.Log(adventure.GetComponent<Adventure>().AdventureInfo.AdventureName + "이(가) 선택한 음식: " + chosenEquipment);
            PurchaseEquipment(chosenEquipment);

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

