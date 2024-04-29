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

        public List<string> foodOptions = new List<string> { "토스트", "스테이크", "샐러드", "수프", "파스타" };


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
            Debug.Log(adventure.GetComponent<Adventure>().AdventureInfo.AdventureName + "이(가) 선택한 음식: " + chosenFood);
            EatFood(chosenFood);

            yield return new WaitForSeconds(7f);

            Debug.Log("모험가 주점 퇴장");

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
            Debug.Log("모험가가 " + food + "를(을) 먹습니다.");
        }

        private void MatchingParty()
        {

        }

    }
}


