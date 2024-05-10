using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�� �ý����� UI�� �����ϴ� ��ũ��Ʈ
public class UIManager : MonoBehaviour
{
    public List<GameObject> panelPrefabs = new List<GameObject>();
    public Canvas canvas; // �г��� ������ ĵ����
    public Camera mainCamera;

    public void Start()
    {
        mainCamera = Camera.main;
        canvas = GameObject.FindAnyObjectByType<Canvas>();
    }


    // �г��� �����ϰ� Ȱ��ȭ�ϴ� �Լ�, �� �гε��� �ҷ��� ��ư�� Ŭ�� �Լ��� ���� 
    public void ShowPanel(int panelIndex)
    {
        // �ε����� �ش��ϴ� �г� ������ ��������
        if (panelIndex >= 0 && panelIndex < panelPrefabs.Count)
        {
            GameObject panelPrefab = panelPrefabs[panelIndex];

            // �̹� �ش� �г��� �����ϴ��� Ȯ��
            GameObject existingPanel = GetExistingPanel(panelPrefab);

            if (existingPanel != null)
            {
                // �̹� �����ϴ� �г��� Ȱ��ȭ
                existingPanel.SetActive(true);
            }
            else
            {
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

    private GameObject GetExistingPanel(GameObject panelPrefab)
    {
        // ĵ������ �ڽ����� �ִ� ��� �г� �˻�
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
