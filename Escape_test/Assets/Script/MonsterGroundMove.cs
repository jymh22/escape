using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGroundMove : MonoBehaviour
{
    public float idleTimeMin = 0f;//가만히 있는 시간 최소
    public float idleTimeMax = 2f;//가만히 있는 시간 최대

    public float moveTimeMin = 0f;//움직이는 시간 최소
    public float moveTimeMax = 2f;//움직이는 시간 최대

    public float moveSpeed = 0.1f; //몬스터의 속도
    private float idleTime; //가만히 있는 시간 카운트
    private float moveTime; //가만히 있는 시간 카운트
    private float timeAfterCycle; //현재 진행중인 사이클 시간(사이클 = 움직이는 시간 + 가만히 있는 시간)

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        idleTime = Random.Range(idleTimeMin, idleTimeMax); //가만히 있는 시간을 랜덤으로 지정
        moveTime = Random.Range(moveTimeMin, moveTimeMax); //가만히 있는 시간을 랜덤으로 지정
        timeAfterCycle = 0f; //가만히 있는 동안 지난 시간
    }

    private void Update()
    {
        timeAfterCycle += Time.deltaTime; //초당 1씩 증가
        if (timeAfterCycle > (moveTime + idleTime)) //한 사이클 끝났을 때
        {
            timeAfterCycle = 0f; //사이클 시간 초기화
            idleTime = Random.Range(idleTimeMin, idleTimeMax); //가만히 있는 시간 재지정
            moveTime = Random.Range(moveTimeMin, moveTimeMax); //가만히 있는 시간 재지정
            if(Random.Range(0, 2) == 1)
                turnabout(); // 1/2 확률로 방향전환
        }
        else if (timeAfterCycle > moveTime) //가만히 있는 시간
            animator.SetBool("Moved", false); //이동 애니메이션 끔
        else //움직이는 시간
        {
            transform.position = new Vector2(transform.position.x - (moveSpeed * Time.deltaTime), transform.position.y); //speed만큼 이동
            animator.SetBool("Moved", true); //이동 애니메이션 켬
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //바닥의 끝에 닿았음을 감지
        if (other.tag == "GroundEnd")
        {
            turnabout(); //방향전환
            transform.position = new Vector2(transform.position.x - (moveSpeed * Time.deltaTime), transform.position.y); //speed만큼 이동
        }
    }

    private void turnabout() //방향전환
    {
        moveSpeed = -moveSpeed; //이동 방향 전환
        transform.Rotate(0f, 180f, 0f); //그래픽 방향 전환
    }
}