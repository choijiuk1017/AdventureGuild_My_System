using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core.Manager;
using Core.UI;

namespace Core.UI
{
    public class AdventureListPanel : MonoBehaviour
    {
        public GameObject panel;

        public RectTransform content;

        public GameObject adventureListPrefab;


        // Start is called before the first frame update
        void Start()
        {
            SpawnUI();
        }

        void SpawnUI()
        {
            for(int i = 0; i < 5; i++)
            {
                GameObject adventureList = Instantiate(adventureListPrefab, content);

                AdventureList adventureListUI = adventureList.GetComponent<AdventureList>();

                //adventureListUI.SetUpUI();
            }
        }
    }
}

