using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    public float hitForce = 10f;
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

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Monster" && !isHit)
        {
            Hit();
        }
    }

    private void Hit() //피격 판정
    {
        playerRigidbody.velocity = Vector2.zero;
        playerRigidbody.AddForce(new Vector2(-400f * hitForce, 400f * hitForce)); // 밀치기 - 이동방향의 반대방향으로 밀치게 해야함. 버그있음.
        animator.SetTrigger("hit"); // 피격 애니메이션
        isHit = true;
    }
}