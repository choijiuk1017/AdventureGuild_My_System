using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//각 시스템의 UI를 관리하는 스크립트
public class UIManager : MonoBehaviour
{
    public List<GameObject> panelPrefabs = new List<GameObject>();
    public Canvas canvas; // 패널이 생성될 캔버스
    public Camera mainCamera;

    public void Start()
    {
        mainCamera = Camera.main;
        canvas = GameObject.FindAnyObjectByType<Canvas>();
    }


    // 패널을 생성하고 활성화하는 함수, 각 패널들을 불러올 버튼의 클릭 함수로 적용 
    public void ShowPanel(int panelIndex)
    {
        // 인덱스에 해당하는 패널 프리팹 가져오기
        if (panelIndex >= 0 && panelIndex < panelPrefabs.Count)
        {
            GameObject panelPrefab = panelPrefabs[panelIndex];

            // 이미 해당 패널이 존재하는지 확인
            GameObject existingPanel = GetExistingPanel(panelPrefab);

            if (existingPanel != null)
            {
                // 이미 존재하는 패널을 활성화
                existingPanel.SetActive(true);
            }
            else
            {
                // 패널을 생성하고 씬에 추가
                GameObject panelInstance = Instantiate(panelPrefab, Vector3.zero, Quaternion.identity);

                // 패널을 캔버스의 자식으로 설정
                panelInstance.transform.SetParent(canvas.transform, false);

                // 생성한 패널을 화면 중앙으로 이동 (예시로, 중앙으로 이동하는 코드)
                panelInstance.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);

                // 패널의 CanvasGroup 컴포넌트를 찾아 활성화
                CanvasGroup canvasGroup = panelInstance.GetComponent<CanvasGroup>();
                if (canvasGroup != null)
                {
                    canvasGroup.alpha = 1;
                    canvasGroup.interactable = true;
                    canvasGroup.blocksRaycasts = true;
                }
            }
        }
    }

    private GameObject GetExistingPanel(GameObject panelPrefab)
    {
        // 캔버스의 자식으로 있는 모든 패널 검사
        foreach (Transform child in canvas.transform)
        {
            if (child.gameObject.name == panelPrefab.name + "(Clone)")
            {
                return child.gameObject;
            }
        }
        return null;
    }

    public void ExitButton()
    {
        mainCamera.transform.position = new Vector3(0f,0f, -10f);
    }
}
