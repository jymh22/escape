using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    public float hitForce = 10f; //피격시 가해지는 힘
    public bool isHit = false; 

    private Rigidbody2D playerRigidbody; //리지드바디
    private Animator animator; // 애니메이터


    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    { //트리거와 접촉하는 순간
        if (other.tag == "Monster" && !isHit)
        { //접촉한 트리거의 태그가 Moster이고 피격된 상태가 아니면
            Hit();
        }
    }

    private void Hit() //피격 판정
    {
        Debug.Log("ddd"); //로그 출력
        playerRigidbody.velocity = Vector2.zero; //움직이던 캐릭터의 속도를 0으로 만듦
        playerRigidbody.AddForce(new Vector2(-20f * hitForce, 100f * hitForce)); //오른쪽으로 밀치기 
        animator.SetTrigger("hit"); // 피격 애니메이션
        isHit = true; //피격된 상태임을 알림
    }
}