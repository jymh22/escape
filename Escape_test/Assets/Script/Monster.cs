using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float speedMIN = 1f; //���� �ּ� �ӵ�
    public float speedMax = 4f; //���� �ִ� �ӵ�

    public float idleTimeMin = 0f;//������ �ִ� �ð� �ּ�
    public float idleTimeMax = 2f;//������ �ִ� �ð� �ִ�

    private float speed; //������ �ӵ�
    private float idleTime ; //������ �ִ� �ð� ī��Ʈ
    private float timeAfterIdle; 

    public float destroyTime = 5f; // �Ҹ� �ð�

    private bool isMoved = false; //idle���� move����

    private Rigidbody mosterRigidbody;
    private Animator animator;


    void Start()
    {
        mosterRigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        speed = Random.Range(speedMIN, speedMax); //�ӵ��� �������� ����
        idleTime = Random.Range(idleTimeMin, idleTimeMax); //������ �ִ� �ð��� �������� ����
        timeAfterIdle = 0f; //������ �ִ� ���� ���� �ð�

        Destroy(gameObject, destroyTime); // ���� ����(��ü, �Ҹ�ð�(second))
    }
    void Update()
    {
        timeAfterIdle += Time.deltaTime; //�ʴ� 1�� ����

        //������ �ִ� �ð� ������ MosterMoved() �Լ� �����Ͽ� ������
        if(timeAfterIdle > idleTime)   MosterMoved();

        animator.SetBool("Moved", isMoved); //�ִϸ��̼�  

    }

    private void  MosterMoved()
    {
        isMoved = true; //true�� move �ִϸ��̼�, false�� idle �ִϸ��̼� 
        transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        //��ġ x��ǥ�� �ʴ� -speed�� ����, �̴� �������� �̵����� �ǹ�
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
