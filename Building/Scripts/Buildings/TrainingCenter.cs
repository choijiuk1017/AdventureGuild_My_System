using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Unit;
using Core.Guild;
using Core.Manager;

namespace Core.Building.TrainingCenter
{
    public class TrainingCenter : Building
    {

        public Transform trainingEntrance;


        // Start is called before the first frame update
        new void Start()
        {
            Init(6);
        }

        protected override void InitEntity()
        {
            GuildManager.Instance.AddGuildEntity(GuildEntityType.Entrance, this);
        }

        public override void OnInteraction(Adventure adventureEntity)
        {
            StartCoroutine(UsingTrainingCenter(adventureEntity.gameObject));
        }

        public override void EndInteraction()
        {

        }

        protected IEnumerator UsingTrainingCenter(GameObject adventure)
        {
            adventureInside = true;


            SetLayerRecursively(adventure, LayerMask.NameToLayer("Invisible"));


            yield return new WaitForSeconds(7f);

            Debug.Log("¸ðÇè°¡ ÈÆ·Ã¼Ò ÅðÀå");
            SetLayerRecursively(adventure, LayerMask.NameToLayer("Adventure"));

            adventure.transform.position = trainingEntrance.position;

            adventure.GetComponent<TestingAdventure>().isInBuilding = false;
            adventure.GetComponent<TestingAdventure>().isSetDestination = false;

            adventureInside = false;

        }
    }
}

