using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGroundMove : MonoBehaviour
{ //������ �̵� ��ũ��Ʈ (�����ð� �̵� -> �����ð� ����)
    public float idleTimeMin = 0f;//������ �ִ� �ּ� �ð�
    public float idleTimeMax = 2f;//������ �ִ� �ִ� �ð�

    public float moveTimeMin = 0f;//�����̴� �ּ� �ð�
    public float moveTimeMax = 2f;//�����̴� �ִ� �ð�

    public float moveSpeed = 0.1f; //������ �ӵ�
    private float idleTime = 0f; //������ �ִ� �ð� ī��Ʈ
    private float moveTime = 0f; //�����̴� �ð� ī��Ʈ
    private float timeAfterCycle; //���� �������� ����Ŭ �ð�(����Ŭ = �����̴� �ð� + ������ �ִ� �ð�)

    private Animator animator; //������Ʈ�� �ִϸ��̼� ������Ʈ

    void Start() //������
    {
        animator = GetComponent<Animator>(); //�ִϸ��̼�  ������Ʈ �Ҵ�
        timeAfterCycle = 0f; //����Ŭ ��� �ð�
    }

    private void Update()
    {
        timeAfterCycle += Time.deltaTime; //�ð� ��� ����
        if (timeAfterCycle > (moveTime + idleTime)) //�� ����Ŭ ������ ��
        {
            timeAfterCycle = 0f; //����Ŭ ��� �ð� �ʱ�ȭ
            idleTime = Random.Range(idleTimeMin, idleTimeMax); //������ �ִ� �ð� ������
            moveTime = Random.Range(moveTimeMin, moveTimeMax); //������ �ִ� �ð� ������
            if (Random.Range(0, 2) == 1)
                turnabout(); // 1/2 Ȯ���� ������ȯ
        }
        else if (timeAfterCycle > moveTime) //������ �ִ� �ð�
            MonsterIdle(); //���� ����
        else //�����̴� �ð�
        {
            MonsterMove(); //���� �̵�
        }
    }

    private void MonsterIdle()
    {
        animator.SetBool("Moved", false); //�̵� �ִϸ��̼� ��
    }
    private void MonsterMove()
    {
        transform.position = new Vector2(transform.position.x - (moveSpeed * Time.deltaTime), transform.position.y);
        //������ speed�� �ӵ��� �̵�
        animator.SetBool("Moved", true); //�̵� �ִϸ��̼� ��
    }

    private void OnTriggerEnter2D(Collider2D other)
    { //�ٸ� Ʈ���ſ� �����ϴ� ����
        if (other.tag == "GroundEnd") //�ٴ��� ���� ����� ��
            turnabout(); //������ȯ
    }

    private void turnabout() //������ȯ
    {
        moveSpeed = -moveSpeed; //�̵� ���� ��ȯ
        transform.Rotate(0f, 180f, 0f); //�׷��� ���� ��ȯ
        transform.position = new Vector2(transform.position.x - (moveSpeed * Time.deltaTime), transform.position.y);
        //Ʈ���� ������ �̵�
    }
}