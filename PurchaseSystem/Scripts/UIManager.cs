using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public List<GameObject> panelPrefabs = new List<GameObject>();
    public Canvas canvas; // 패널이 생성될 캔버스

    // 패널을 생성하고 활성화하는 함수
    public void ShowPanel(int panelIndex)
    {
        // 인덱스에 해당하는 패널 프리팹 가져오기
        if (panelIndex >= 0 && panelIndex < panelPrefabs.Count)
        {
            GameObject panelPrefab = panelPrefabs[panelIndex];

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
