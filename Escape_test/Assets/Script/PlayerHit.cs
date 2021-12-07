using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    public float hitForce = 10f; //�ǰݽ� �������� ��
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

    private void OnTriggerEnter2D(Collider2D other)
    { //Ʈ���ſ� �����ϴ� ����
        if (other.tag == "Monster" && !isHit)
        { //������ Ʈ������ �±װ� Moster�̰� �ǰݵ� ���°� �ƴϸ�
            Hit();
        }
    }

    private void Hit() //�ǰ� ����
    {
        Debug.Log("ddd"); //�α� ���
        playerRigidbody.velocity = Vector2.zero; //�����̴� ĳ������ �ӵ��� 0���� ����
        playerRigidbody.AddForce(new Vector2(-20f * hitForce, 100f * hitForce)); //���������� ��ġ�� 
        animator.SetTrigger("hit"); // �ǰ� �ִϸ��̼�
        isHit = true; //�ǰݵ� �������� �˸�
    }
}