using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestingAdventure : MonoBehaviour
{
    public int willingness;
    public int hp;
    public int mp;

    public Transform destination; // 목적지

    public float moveSpeed = 5f; // 이동 속도

    private bool isMoving = false; // 이동 중인지 여부

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DecreaseWillpowerRoutine());

        // 목적지가 설정되어 있고 이동 중이 아닌 경우에만 이동
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
            yield return new WaitForSeconds(10f); // 1초마다 실행
            willingness -= 10; // 의지 감소
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

    // 특정 위치로 이동하는 함수
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
