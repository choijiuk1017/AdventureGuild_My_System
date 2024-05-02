using System;
using System.Collections.Generic;
using Core.Battle;
using Core.Manager;
using Core.Unit.Body;
using UnityEngine;

namespace Core.Unit.Monster
{
    public class Monster : Unit
    {
        private int monsterID;
        
        protected override void Init()
        {
            base.Init();
            unitBattleStats = new();

            unitStat = new Stat()
            {
                hp = 100,
                curHp = 100,
                mp = 10,
                str = 5,
                agi = 5,
                inte = 5
            };
            
            unitBody.SetHp(unitStat.hp);
            SetMonsterInfo(1);
            DropItem();
        }

        public void SetMonsterInfo(int monsterID)
        {
            this.monsterID = monsterID;
        }

        public int GetMonsterID()
        {
            return monsterID;
        }

        public override void Die()
        {
            base.Die();
        }

        public List<Item.Item> DropItem()
        {
            List<Item.Item> dropItem = new List<Item.Item>();

            foreach (var bodyPart in unitBody.GetBodyParts())
            {
                var partsData = BodyPreset.Instance.GetBodyData(1,bodyPart.partID);
                if(partsData.partItem == 0) continue;
                dropItem.Add(ItemManager.Instance.GetItem(partsData.partItem));
            }

            return dropItem;
        }
    }
}
