using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{ //�Ѿ� ������Ʈ�� �Ѿ� �̵� ��ũ��Ʈ
    public float BulletSpeed = 1.0f; //�Ѿ� �ӵ�
    public float destroyTime = 10f; // �Ҹ� �ð�

    private void Start() //������
    {
        Destroy(gameObject, destroyTime); //destroyTime�Ŀ� ���� ����
        if (this.name == "leftbullet") BulletSpeed *= -1; //���ʿ��� ��� �Ѿ��� ��� ���� ����
    }
    void Update() //1�����Ӹ��� ����
    {
        transform.position = new Vector2(transform.position.x - (BulletSpeed * Time.deltaTime), transform.position.y);
        //speed��ŭ�� �ӵ��� �̵�
    }
}
