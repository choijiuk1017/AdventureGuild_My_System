using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Unit;
using Core.Guild;
using Core.Manager;

namespace Core.Building.PotionMarket
{
    public class PotionMarket : Building
    {
        public Transform potionMarketEntrance;

        public List<string> potionOptions = new List<string> {"ü�� ����", "���� ����", "�ż� ����"};

        // Start is called before the first frame update
        new void Start()
        {
            Init(5);
        }

        protected override void InitEntity()
        {
            GuildManager.Instance.AddGuildEntity(GuildEntityType.Entrance, this);
        }

        public override void OnInteraction(Adventure adventureEntity)
        {
            StartCoroutine(UsingPotionMarket(adventureEntity.gameObject));
        }

        public override void EndInteraction()
        {

        }


        protected IEnumerator UsingPotionMarket(GameObject adventure)
        {
            adventureInside = true;

            SetLayerRecursively(adventure, LayerMask.NameToLayer("Invisible"));

            string chosenPotion = ChooseRandomPotion();
            Debug.Log(adventure.GetComponent<Adventure>().AdventureInfo.AdventureName + "��(��) ������ ����: " + chosenPotion);
            PurchasePotion(chosenPotion);

            yield return new WaitForSeconds(7f);

            Debug.Log("���谡 ���� ���� ����");

            SetLayerRecursively(adventure, LayerMask.NameToLayer("Adventure"));

            adventure.transform.position = potionMarketEntrance.position;

            adventure.GetComponent<TestingAdventure>().isInBuilding = false;
            adventure.GetComponent<TestingAdventure>().isSetDestination = false;


            adventureInside = false;

        }

        private string ChooseRandomPotion()
        {
            int randomIndex = UnityEngine.Random.Range(0, potionOptions.Count);
            return potionOptions[randomIndex];
        }

        private void PurchasePotion(string potion)
        {
            Debug.Log("���谡�� " + potion+ "��(��) �����߽��ϴ�.");
        }

    }
}

