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


        //���� ��� �κ�
        private IEnumerator UsingTemple(GameObject adventure)
        {
            adventureInside = true;

            PerformActionBasedOnBuildingType(adventure, buildingData.buildingType, buildingData.buildingValue);

            var delayTime = buildingData.buildingTime;

            yield return new WaitForSeconds(delayTime);

            Debug.Log("���谡 ���� ����");

            SetLayerRecursively(adventure, LayerMask.NameToLayer("Adventure"));

            adventure.transform.position = templeEntrance.position;

            currentAdventure--;

            adventureInside = false;


        }


        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Adventure") && currentAdventure <= maxAdventure)
            {
                currentAdventure++;
                Debug.Log("���谡 ���� ����");

                SetLayerRecursively(col.gameObject, LayerMask.NameToLayer("Invisible"));

                //StartCoroutine(TempleTime(col.gameObject));
            }
        }
    }

}

