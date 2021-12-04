using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float speedMIN = 1f; //몬스터 최소 속도
    public float speedMax = 4f; //몬스터 최대 속도

    public float idleTimeMin = 0f;//가만히 있는 시간 최소
    public float idleTimeMax = 2f;//가만히 있는 시간 최대

    private float speed; //몬스터의 속도
    private float idleTime ; //가만히 있는 시간 카운트
    private float timeAfterIdle; 

    public float destroyTime = 5f; // 소멸 시간

    private bool isMoved = false; //idle인지 move인지

    private Rigidbody mosterRigidbody;
    private Animator animator;


    void Start()
    {
        mosterRigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        speed = Random.Range(speedMIN, speedMax); //랜덤속도 지정
        idleTime = Random.Range(idleTimeMin, idleTimeMax); //가만히 있는 랜덤시간 지정
        timeAfterIdle = 0f; //가만히 있는 동안 지난 시간

        Destroy(gameObject, destroyTime); // 몬스터 제거(물체, 소멸시간(second))
    }
    void Update()
    {
        timeAfterIdle += Time.deltaTime;

        //가만히 있는 시간 지나면 움직임
        if(timeAfterIdle > idleTime)   MosterMoved();

        animator.SetBool("Moved", isMoved); //애니메이션  

    }

    private void  MosterMoved()
    {
        isMoved = true; //true면 move, false면 idle
        transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
    }


    /*
    private void MosterAni()
    {
        animator.SetBool("Moved", isMoved);
    }
    */

    /*
   private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerController PlayerController = other.GetComponent<PlayerController>();
            if (PlayerController != null)
            {
                PlayerController.Hit();
            }
        }
    }
    */
}
