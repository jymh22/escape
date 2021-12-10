using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{ //�÷��̾��� �ǰ� ��ũ��Ʈ
    public float hitForce = 10f; //�ǰݽ� �������� ��
    public bool isHit = false;  //�ǰ� ���� ����

    private Rigidbody2D playerRigidbody; //������Ʈ ��ġ���� ������Ʈ
    private Animator animator; //������Ʈ �ִϸ�����


    void Start()//������
    {
        //�� ������Ʈ �Ҵ�
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    private void OnTriggerStay2D(Collider2D other)
    { //�ٸ� Ʈ���ſ� �����ϴ� ����
        if (other.tag == "Monster" && !isHit)
        { //������ Ʈ������ �±װ� Moster�̰� �ǰݵ� ���°� �ƴϸ�
            Hit(); //�ǰ�
        }
    }

    private void Hit() //�ǰ� ����
    {
        playerRigidbody.velocity = Vector2.zero; //�����̴� ĳ������ �ӵ��� 0���� ����
        playerRigidbody.AddForce(new Vector2(-20f * hitForce, 100f * hitForce)); //�������� ��ġ�� 
        animator.SetTrigger("hit"); // �ǰ� �ִϸ��̼�
        isHit = true; //�ǰݵ� �������� �˸�
    }
}