using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class CategoriButton : MonoBehaviour
    {
        public GameObject[] categoryScrollViews;

        public void ShowCategory(int index)
        {
            foreach(GameObject scrollview in categoryScrollViews)
            {
                scrollview.SetActive(false);
            }

            if(index >= 0 && index < categoryScrollViews.Length)
            {
                categoryScrollViews[index].SetActive(true);
            }
        }

        public void ExitButton()
        {
            gameObject.SetActive(false);
        }
    }
}


