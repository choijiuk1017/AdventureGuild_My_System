using System;
using UnityEngine;

namespace Core.Unit
{
    public enum AdventureRank
    {
        A, B, C, D, E, F, G
    }
    
    [Serializable]
    public class AdventureInfo
    {
        private string adventureName;
        private int adventureAge;
        [SerializeField]
        private Stat adventureStat;
        private AdventureRank adventureRank;

        public string AdventureName => adventureName;
        public int AdventureAge => adventureAge;
        public Stat AdventureStat => adventureStat;
        public AdventureRank AdventureRank => adventureRank;
        
        public AdventureInfo(string name, int age, Stat stat)
        {
            adventureName = name;
            adventureAge = age;
            adventureStat = stat;
            adventureRank = AdventureRank.G;
        }
    }
}
