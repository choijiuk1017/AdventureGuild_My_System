using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public List<GameObject> panelPrefabs = new List<GameObject>();
    public Canvas canvas; // �г��� ������ ĵ����

    // �г��� �����ϰ� Ȱ��ȭ�ϴ� �Լ�
    public void ShowPanel(int panelIndex)
    {
        // �ε����� �ش��ϴ� �г� ������ ��������
        if (panelIndex >= 0 && panelIndex < panelPrefabs.Count)
        {
            GameObject panelPrefab = panelPrefabs[panelIndex];

            // �г��� �����ϰ� ���� �߰�
            GameObject panelInstance = Instantiate(panelPrefab, Vector3.zero, Quaternion.identity);

            // �г��� ĵ������ �ڽ����� ����
            panelInstance.transform.SetParent(canvas.transform, false);

            // ������ �г��� ȭ�� �߾����� �̵� (���÷�, �߾����� �̵��ϴ� �ڵ�)
            panelInstance.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);

            // �г��� CanvasGroup ������Ʈ�� ã�� Ȱ��ȭ
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
