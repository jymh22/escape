using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGroundMove : MonoBehaviour
{ //몬스터의 이동 스크립트 (랜덤시간 이동 -> 랜덤시간 멈춤)
    public float idleTimeMin = 0f;//가만히 있는 최소 시간
    public float idleTimeMax = 2f;//가만히 있는 최대 시간

    public float moveTimeMin = 0f;//움직이는 최소 시간
    public float moveTimeMax = 2f;//움직이는 최대 시간

    public float moveSpeed = 0.1f; //몬스터의 속도
    private float idleTime = 0f; //가만히 있는 시간 카운트
    private float moveTime = 0f; //움직이는 시간 카운트
    private float timeAfterCycle; //현재 진행중인 사이클 시간(사이클 = 움직이는 시간 + 가만히 있는 시간)

    private Animator animator; //오브젝트의 애니메이션 컴포넌트

    void Start() //생성자
    {
        animator = GetComponent<Animator>(); //애니메이션  컴포넌트 할당
        timeAfterCycle = 0f; //사이클 경과 시간
    }

    private void Update()
    {
        timeAfterCycle += Time.deltaTime; //시간 경과 저장
        if (timeAfterCycle > (moveTime + idleTime)) //한 사이클 끝났을 때
        {
            timeAfterCycle = 0f; //사이클 경과 시간 초기화
            idleTime = Random.Range(idleTimeMin, idleTimeMax); //가만히 있는 시간 재지정
            moveTime = Random.Range(moveTimeMin, moveTimeMax); //가만히 있는 시간 재지정
            if (Random.Range(0, 2) == 1)
                turnabout(); // 1/2 확률로 방향전환
        }
        else if (timeAfterCycle > moveTime) //가만히 있는 시간
            MonsterIdle(); //몬스터 멈춤
        else //움직이는 시간
        {
            MonsterMove(); //몬스터 이동
        }
    }

    private void MonsterIdle()
    {
        animator.SetBool("Moved", false); //이동 애니메이션 끔
    }
    private void MonsterMove()
    {
        transform.position = new Vector2(transform.position.x - (moveSpeed * Time.deltaTime), transform.position.y);
        //앞으로 speed의 속도로 이동
        animator.SetBool("Moved", true); //이동 애니메이션 켬
    }

    private void OnTriggerEnter2D(Collider2D other)
    { //다른 트리거와 접촉하는 순간
        if (other.tag == "GroundEnd") //바닥의 끝에 닿았을 때
            turnabout(); //방향전환
    }

    private void turnabout() //방향전환
    {
        moveSpeed = -moveSpeed; //이동 방향 전환
        transform.Rotate(0f, 180f, 0f); //그래픽 방향 전환
        transform.position = new Vector2(transform.position.x - (moveSpeed * Time.deltaTime), transform.position.y);
        //트리거 밖으로 이동
    }
}