using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Unit;
using Core.Guild;
using Core.Manager;

namespace Core.Building.Inn
{
    public class Inn : Building
    {
        public Transform InnEntrance;

        public List<string> foodOptions = new List<string> { "�佺Ʈ", "������ũ", "������", "����", "�Ľ�Ÿ" };


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
            StartCoroutine(UsingInn(adventureEntity.gameObject));
        }

        public override void EndInteraction()
        {

        }

        protected IEnumerator UsingInn(GameObject adventure)
        {
            adventureInside = true;

            SetLayerRecursively(adventure, LayerMask.NameToLayer("Invisible"));

            string chosenFood = ChooseRandomFood();
            Debug.Log(adventure.GetComponent<Adventure>().AdventureInfo.AdventureName + "��(��) ������ ����: " + chosenFood);
            EatFood(chosenFood);

            yield return new WaitForSeconds(7f);

            Debug.Log("���谡 ���� ����");

            SetLayerRecursively(adventure, LayerMask.NameToLayer("Adventure"));
            adventure.transform.position = InnEntrance.position;

            adventure.GetComponent<TestingAdventure>().isInBuilding = false;
            adventure.GetComponent<TestingAdventure>().isSetDestination = false;


            adventureInside = false;

        }

        private string ChooseRandomFood()
        {
            int randomIndex = UnityEngine.Random.Range(0, foodOptions.Count);
            return foodOptions[randomIndex];
        }

        private void EatFood(string food)
        {
            Debug.Log("���谡�� " + food + "��(��) �Խ��ϴ�.");
        }

        private void MatchingParty()
        {

        }

    }
}


