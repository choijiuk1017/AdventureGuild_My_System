using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core.Manager;
using Core.UI;


namespace Core.UI
{
    public class BuildingConstructUI : MonoBehaviour
    {

        private List<BuildingManager.BuildingData> buildingList;

        public GameObject panel;

        public RectTransform content;

        public GameObject buildingUIPrefab;

        // Start is called before the first frame update
        void Start()
        {
            BuildingManager buildingManager = GameObject.FindObjectOfType<BuildingManager>();
            if (buildingManager != null)
            {
                buildingList = buildingManager.GetBuildingList();
            }
            else
            {
                Debug.LogError("BuildingManager가 씬에 없습니다.");
            }


            SpawnUI();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void SpawnUI()
        {       
            foreach (BuildingManager.BuildingData building in buildingList)
            {
                GameObject buildingGo = Instantiate(buildingUIPrefab, content);

                BuildingUI buildingUI = buildingGo.GetComponent<BuildingUI>();

                buildingUI.SetUpUI(building);
            }
        }
    }
}

