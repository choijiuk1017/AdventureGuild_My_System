using System.Collections;
using System.Collections.Generic;
using Core.Guild.Furniture;
using Core.Unit;
using UnityEngine;

namespace Core.Guild
{
    public class Guild : MonoBehaviour
    {
        [SerializeField]
        private List<Adventure> adventureList;
        private RecruitAdventure recruitAdventurer;
        private ReceptionDesk receptionDesk;
        
        private int fame;
        private int money;

        public int Fame => fame;
        public int Money => money;
    
        // Start is called before the first frame update
        private void Start()
        {
            InitGuild();
            
            recruitAdventurer = GetComponent<RecruitAdventure>();

            StartCoroutine(SpawnAdventure());
        }

        private void InitGuild()
        {
            adventureList = new List<Adventure>();

            receptionDesk = FindObjectOfType<ReceptionDesk>();
        }

        IEnumerator SpawnAdventure()
        {
            yield return new WaitForSeconds(1f);
            
            var newAdventureCount = Random.Range(1, 4);

            for (int i = 0; i < newAdventureCount; i++)
            {
                recruitAdventurer.Recruit();
            }
        }

        public void AddAdventure(Adventure newAdventure)
        {
            adventureList.Add(newAdventure);
        }

        public List<Adventure> GetAdventures()
        {
            return adventureList;
        }

        public void RemoveAdventure()
        {
            //죽어도 남겨둬야할지?
        }
    }
}