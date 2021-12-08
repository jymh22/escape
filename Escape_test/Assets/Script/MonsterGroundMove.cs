using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGroundMove : MonoBehaviour
{
    public float idleTimeMin = 0f;//������ �ִ� �ð� �ּ�
    public float idleTimeMax = 2f;//������ �ִ� �ð� �ִ�

    public float moveTimeMin = 0f;//�����̴� �ð� �ּ�
    public float moveTimeMax = 2f;//�����̴� �ð� �ִ�

    public float moveSpeed = 0.1f; //������ �ӵ�
    private float idleTime; //������ �ִ� �ð� ī��Ʈ
    private float moveTime; //������ �ִ� �ð� ī��Ʈ
    private float timeAfterCycle; //���� �������� ����Ŭ �ð�(����Ŭ = �����̴� �ð� + ������ �ִ� �ð�)

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        idleTime = Random.Range(idleTimeMin, idleTimeMax); //������ �ִ� �ð��� �������� ����
        moveTime = Random.Range(moveTimeMin, moveTimeMax); //������ �ִ� �ð��� �������� ����
        timeAfterCycle = 0f; //������ �ִ� ���� ���� �ð�
    }

    private void Update()
    {
        timeAfterCycle += Time.deltaTime; //�ʴ� 1�� ����
        if (timeAfterCycle > (moveTime + idleTime)) //�� ����Ŭ ������ ��
        {
            timeAfterCycle = 0f; //����Ŭ �ð� �ʱ�ȭ
            idleTime = Random.Range(idleTimeMin, idleTimeMax); //������ �ִ� �ð� ������
            moveTime = Random.Range(moveTimeMin, moveTimeMax); //������ �ִ� �ð� ������
            if(Random.Range(0, 2) == 1)
                turnabout(); // 1/2 Ȯ���� ������ȯ
        }
        else if (timeAfterCycle > moveTime) //������ �ִ� �ð�
            animator.SetBool("Moved", false); //�̵� �ִϸ��̼� ��
        else //�����̴� �ð�
        {
            transform.position = new Vector2(transform.position.x - (moveSpeed * Time.deltaTime), transform.position.y); //speed��ŭ �̵�
            animator.SetBool("Moved", true); //�̵� �ִϸ��̼� ��
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //�ٴ��� ���� ������� ����
        if (other.tag == "GroundEnd")
        {
            turnabout(); //������ȯ
            transform.position = new Vector2(transform.position.x - (moveSpeed * Time.deltaTime), transform.position.y); //speed��ŭ �̵�
        }
    }

    private void turnabout() //������ȯ
    {
        moveSpeed = -moveSpeed; //�̵� ���� ��ȯ
        transform.Rotate(0f, 180f, 0f); //�׷��� ���� ��ȯ
    }
}