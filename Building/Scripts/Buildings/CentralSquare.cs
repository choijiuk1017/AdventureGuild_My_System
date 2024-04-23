using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Unit;
using Core.Guild;
using Core.Manager;

namespace Core.Building.CentralSquare
{
    public class CentralSquare : Building
    {

        public Transform centralSquareEntrance;

        // Start is called before the first frame update
        new void Start()
        {
            Init(8);
        }
        protected override void InitEntity()
        {
            GuildManager.Instance.AddGuildEntity(GuildEntityType.Entrance, this);
        }

        public override void OnInteraction(Adventure adventureEntity)
        {
            StartCoroutine(UsingCentralSquare(adventureEntity.gameObject));
        }

        public override void EndInteraction()
        {

        }


        protected IEnumerator UsingCentralSquare(GameObject adventure)
        {
            adventureInside = true;

            SetLayerRecursively(adventure, LayerMask.NameToLayer("Invisible"));

            yield return new WaitForSeconds(7f);

            Debug.Log("¸ðÇè°¡ Áß¾Ó ±¤Àå ÅðÀå");

            SetLayerRecursively(adventure, LayerMask.NameToLayer("Adventure"));
            adventure.transform.position = centralSquareEntrance.position;

            adventure.GetComponent<TestingAdventure>().isInBuilding = false;
            adventure.GetComponent<TestingAdventure>().isSetDestination = false;

            adventureInside = false;

        }

    }
}

