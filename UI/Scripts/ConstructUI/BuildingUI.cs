using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core.Manager;

namespace Core.UI
{
    public class BuildingUI : MonoBehaviour
    {
        public string buildingName;

        public Text nameText;

        public Text goldText;


        public void SetUpUI(BuildingManager.BuildingData buildingData)
        {
            buildingName = buildingData.buildingName;

            nameText.text = buildingData.buildingKoreanName;

            goldText.text = buildingData.buildingGold.ToString();
        }

        public void ConstructButtonClick()
        {
            //buildingName,buildingID에 따라 조건 출력
        }
    }
}


