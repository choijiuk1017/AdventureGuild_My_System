using System.Collections;
using System.Collections.Generic;
using Core.Unit;
using Unity.VisualScripting;
using UnityEngine;

namespace Core.Guild
{
    public class RecruitAdventure : MonoBehaviour
    {
        private Guild guild;
        [SerializeField] private GameObject adventurePrefab;
        [SerializeField] private Transform guildEntrance;
        
        // Start is called before the first frame update
        void Start()
        {
            guild = FindObjectOfType<Guild>();
        }

        public Adventure Recruit()
        {
            var newAdventure = Instantiate(adventurePrefab, guildEntrance.position, Quaternion.identity).GetComponent<Adventure>();

            return newAdventure;
        }

        public static AdventureInfo CreateAdventureInfo()
        {
            var stat = GetRandomStat();
            var adventureInfo = new AdventureInfo("name", 18, stat);

            return adventureInfo;
        }

        private static Stat GetRandomStat()
        {
            var stat = new Stat
            {
                hp = Random.Range(3, 10),
                mp = Random.Range(1, 10),
                str = Random.Range(1, 10),
                inte = Random.Range(1, 10),
                agi = Random.Range(1, 10)
            };
            stat.curHp = stat.hp;

            return stat;
        }
    }
}