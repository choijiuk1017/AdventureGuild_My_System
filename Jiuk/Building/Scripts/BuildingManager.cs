using System;
using System.Collections.Generic;
using UnityEngine;
using Utility.Data;


namespace Core.Manager
{
    public class BuildingManager : MonoBehaviour
    {   
        [SerializeField]
        private Dictionary<int, BuildingData> buildingData;

        [SerializeField]
        private List<BuildingData> buildingList;

        private const string buildingDataTableName = "Building_Master";

        private const string buildingID = "ID";
        private const string buildingKoreanName = "Korean_Name";
        private const string buildingName = "Building_Name";
        private const string buildingType = "Building_Type";
        private const string buildingValue = "Building_Value";
        private const string buildingRound = "Building_Round";
        private const string buildingTime = "Building_Time";
        private const string buildingGold = "Building_Gold";
        private const string buildingCost = "Building_Cost";
        private const string buildingTax = "Building_Tax";
        private const string buildingSalary = "Building_Salary";


        // Start is called before the first frame update
        void Start()
        {
            InitBuildingData();
        }

        private void InitBuildingData()
        {
            buildingData = new Dictionary<int, BuildingData>();
            buildingList = new();

            var buildings = DataParser.Parser(buildingDataTableName);

            foreach(var buildingParsered in buildings)
            {
                var bData = new BuildingData()
                {
                    buildingID = DataParser.IntParse(buildingParsered[buildingID]),
                    buildingKoreanName = buildingParsered[buildingKoreanName].ToString(),
                    buildingName = buildingParsered[buildingName].ToString(),
                    buildingType = DataParser.IntParse(buildingParsered[buildingType]),
                    buildingValue = DataParser.IntParse(buildingParsered[buildingValue]),
                    buildingRound = DataParser.IntParse(buildingParsered[buildingRound]),
                    buildingTime = DataParser.IntParse(buildingParsered[buildingTime]),
                    buildingGold = DataParser.IntParse(buildingParsered[buildingGold]), 
                    buildingCost = DataParser.IntParse(buildingParsered[buildingCost]), 
                    buildingTax = DataParser.IntParse(buildingParsered[buildingTax]),
                    buildingSalary = DataParser.IntParse(buildingParsered[buildingSalary]),
                };
                buildingList.Add(bData);
                buildingData.Add(bData.buildingID, bData);
            }

            
        }

        public BuildingData GetBuildingData(int buildingID)
        {           
            return buildingData[buildingID];
        }

        public List<BuildingData> GetBuildingList()
        {
            return buildingList;
        }


        [Serializable]
        public class BuildingData
        {
            public int buildingID;
            public string buildingKoreanName;
            public string buildingName;
            public int buildingType;
            public int buildingValue;
            public int buildingRound;
            public int buildingTime;
            public int buildingGold;
            public int buildingCost;
            public int buildingTax;
            public int buildingSalary;
        }
    }

    

}


