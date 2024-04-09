using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestingAdventure : MonoBehaviour
{
    public int willingness;
    public int hp;
    public int mp;

    public Transform destination; // ������

    public float moveSpeed = 5f; // �̵� �ӵ�

    private bool isMoving = false; // �̵� ������ ����

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DecreaseWillpowerRoutine());

        // �������� �����Ǿ� �ְ� �̵� ���� �ƴ� ��쿡�� �̵�
        if (destination != null && !isMoving)
        {
            StartCoroutine(MoveToDestination());
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator DecreaseWillpowerRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f); // 1�ʸ��� ����
            willingness -= 10; // ���� ����
        }
    }

    private IEnumerator MoveToDestination()
    {
        isMoving = true;
        yield return new WaitForSeconds(15f);
        while (Vector2.Distance(transform.position, destination.position) > 0.1f)
        {
            Vector2 direction = (destination.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
            yield return null;
        }

        isMoving = false;
    }

    // Ư�� ��ġ�� �̵��ϴ� �Լ�
    public void MoveTo(Vector2 targetPosition)
    {
        destination.position = targetPosition;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Building"))
        {
            StopAllCoroutines();
        }
    }
}
