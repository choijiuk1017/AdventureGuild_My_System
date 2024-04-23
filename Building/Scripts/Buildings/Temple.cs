using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Unit;
using Core.Guild;
using Core.Manager;

namespace Core.Building.Temple
{
    public class Temple : Building
    {

        public Transform templeEntrance;

        public int currentAdventure = 0;
        public int maxAdventure = 3;

        // Start is called before the first frame update
        new void Start()
        {
            Init(2);
        }

        protected override void InitEntity()
        {
            GuildManager.Instance.AddGuildEntity(GuildEntityType.Temple, this);
        }

        public override void OnInteraction(Adventure adventureEntity)
        {
            if (currentAdventure <= maxAdventure)
            {
                currentAdventure++;
                StartCoroutine(UsingTemple(adventureEntity.gameObject));
            }
        }

        public override void EndInteraction()
        {

        }


        //신전 사용 부분
        private IEnumerator UsingTemple(GameObject adventure)
        {
            SetLayerRecursively(adventure, LayerMask.NameToLayer("Invisible"));

            adventureInside = true;

            PerformActionBasedOnBuildingType(adventure, buildingData.buildingType, buildingData.buildingValue);

            var delayTime = buildingData.buildingTime;

            yield return new WaitForSeconds(delayTime);

            Debug.Log("모험가 신전 퇴장");

            SetLayerRecursively(adventure, LayerMask.NameToLayer("Adventure"));

            adventure.transform.position = templeEntrance.position;

            currentAdventure--;

            adventureInside = false;
        }

    }

}

