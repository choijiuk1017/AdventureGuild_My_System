using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Unit;
using Core.Guild;
using Core.Manager;

namespace Core.Building.Library
{
    public class Library : Building
    {
        public Transform libraryEntrance;


        // Start is called before the first frame update
        new void Start()
        {
            Init(7);
        }
        protected override void InitEntity()
        {
            GuildManager.Instance.AddGuildEntity(GuildEntityType.Entrance, this);
        }

        public override void OnInteraction(Adventure adventureEntity)
        {
            StartCoroutine(UsingLibrary(adventureEntity.gameObject));
        }

        public override void EndInteraction()
        {

        }

        protected IEnumerator UsingLibrary(GameObject adventure)
        {
            adventureInside = true;

            SetLayerRecursively(adventure, LayerMask.NameToLayer("Invisible"));

            var delayTime = buildingData.buildingTime * 60;

            yield return new WaitForSeconds(delayTime);

            UpgradeSkill();

            Debug.Log("모험가 도서관 퇴장");

            SetLayerRecursively(adventure, LayerMask.NameToLayer("Adventure"));
            adventure.transform.position = libraryEntrance.position;


            adventure.GetComponent<TestingAdventure>().isInBuilding = false;
            adventure.GetComponent<TestingAdventure>().isSetDestination = false;

            adventureInside = false;

        }

        private void UpgradeSkill()
        {
            Debug.Log("스킬 강화");
        }
    }

}


