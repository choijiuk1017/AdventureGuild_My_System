using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Utility.Data;

namespace Core.Unit.Body
{
    public class BodyPreset : Singleton<BodyPreset>
    {
        private const string bodyPresetDataTableName = "Part_Master";

        private const string presetId = "Preset_ID";
        private const string presetName = "Preset_Name";
        
        private const string partId = "Part_ID";
        private const string partName = "Part_Name";
        private const string partHp = "Part_HP";
        private const string partHpPercent = "Part_HP_Percent";
        private const string partPercent = "Part_Percent";
        private const string partMainType = "Part_Main_Type";
        private const string partSubType = "Part_Sub_Type";
        private const string partItem = "Part_Item";

        private Dictionary<int, List<BodyData>> bodyPresets;
        public List<List<BodyData>> debugData;
        
        // Start is called before the first frame update
        void Start()
        {
            InitBodyPreset();
        }

        private void InitBodyPreset()
        {
            bodyPresets = new Dictionary<int, List<BodyData>>();
            debugData = new();
            
            var presets = DataParser.Parser(bodyPresetDataTableName);

            foreach (var bodyPreset in presets)
            {
                var bps = DataParser.Parser(bodyPreset[presetName].ToString());
                var bodyParts = new List<BodyData>();
                
                foreach (var parts in bps)
                {
                    var bodyData = new BodyData()
                    {
                        partID = DataParser.IntParse(parts[partId]),
                        partName = parts[partName].ToString(),
                        partHpPercent = DataParser.IntParse(parts[partHpPercent]),
                        partPercent = DataParser.IntParse(parts[partPercent]),
                        partMain = DataParser.IntParse(parts[partMainType]),
                        partSubType = DataParser.IntParse(parts[partSubType]),
                        partItem = DataParser.IntParse(parts[partItem])
                    };
                    
                    //debugData.Add(bodyData);
                    bodyParts.Add(bodyData);
                }
                debugData.Add(bodyParts);
                bodyPresets.Add(DataParser.IntParse(bodyPreset[presetId]), bodyParts);
            }
        }

        public List<BodyData> GetBodyPreset(int idx)
        {
            return debugData[idx];
        }

        public BodyData GetBodyData(int idx, int partID)
        {
            return debugData[idx][partID - 1];
        }
    }

    [Serializable]
    public class BodyData
    {
        public int partID;
        public string partName;
        public int partHpPercent;
        public int partPercent;
        public int partMain;
        public int partSubType;
        public int partItem;
    }
}
