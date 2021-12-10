using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{ //플레이어의 피격 스크립트
    public float hitForce = 10f; //피격시 가해지는 힘
    public bool isHit = false;  //피격 상태 저장

    private Rigidbody2D playerRigidbody; //오브젝트 위치제어 컴포넌트
    private Animator animator; //오브젝트 애니메이터


    void Start()//생성자
    {
        //각 컴포넌트 할당
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    private void OnTriggerStay2D(Collider2D other)
    { //다른 트리거와 접촉하는 동안
        if (other.tag == "Monster" && !isHit)
        { //접촉한 트리거의 태그가 Moster이고 피격된 상태가 아니면
            Hit(); //피격
        }
    }

    private void Hit() //피격 판정
    {
        playerRigidbody.velocity = Vector2.zero; //움직이던 캐릭터의 속도를 0으로 만듦
        playerRigidbody.AddForce(new Vector2(-20f * hitForce, 100f * hitForce)); //왼쪽으로 밀치기 
        animator.SetTrigger("hit"); // 피격 애니메이션
        isHit = true; //피격된 상태임을 알림
    }
}