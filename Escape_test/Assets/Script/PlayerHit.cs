using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    public float hitForce = 10f;
    public bool isHit = false;

    private Rigidbody2D playerRigidbody; //������ٵ�
    private Animator animator; // �ִϸ�����


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

    private void Hit() //�ǰ� ����
    {
        playerRigidbody.velocity = Vector2.zero;
        playerRigidbody.AddForce(new Vector2(-400f * hitForce, 400f * hitForce)); // ��ġ�� - �̵������� �ݴ�������� ��ġ�� �ؾ���. ��������.
        animator.SetTrigger("hit"); // �ǰ� �ִϸ��̼�
        isHit = true;
    }
}