using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGroundMove : MonoBehaviour
{
    public float moveSpeed = 0.11f;

    private Rigidbody mosterRigidbody;
    private Animator animator;

    void Start()
    {
        mosterRigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        MoveFront();
        animator.SetBool("Moved", true); //�ִϸ��̼�  
    }
    private void MoveFront()
    {
        transform.position = new Vector2(transform.position.x - (moveSpeed * Time.deltaTime), transform.position.y);
    }

    private void MonsterAni()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        //�ٴڿ� ������� ����
        if (other.tag == "GroundEnd")
        {
            moveSpeed = -moveSpeed;
            transform.Rotate(0f, 180f, 0f);
        }
    }
}
