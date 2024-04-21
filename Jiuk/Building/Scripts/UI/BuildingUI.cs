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

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetUpUI(BuildingManager.BuildingData buildingData)
        {
            buildingName = buildingData.buildingName;

            nameText.text = buildingData.buildingKoreanName;

            goldText.text = buildingData.buildingGold.ToString();
        }

        public void ConstructBuilding()
        {
            GameObject building = GameObject.Find(buildingName);

            if (building != null)
            {
                building.GetComponent<SpriteRenderer>().enabled = true; 
                building.GetComponent<BoxCollider2D>().enabled = true;
            }
            else
            {
                Debug.Log("¾øÀ½ " + buildingName);
            }

        }
    }


}

