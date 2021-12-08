using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float BulletSpeed = 1.0f;
    public float destroyTime = 10f; // �Ҹ� �ð�

    private void Start()
    {
        Destroy(gameObject, destroyTime); // ���� ����(��ü, �Ҹ�ð�(second))
        if (this.name == "leftbullet")
            BulletSpeed *= -1;
    }
    void Update()
    {
        //�����Ӹ��� ������Ʈ�� ������ǥ�󿡼� ������ 1�� ����ŭ ���ư���
        transform.position = new Vector2(transform.position.x - (BulletSpeed * Time.deltaTime), transform.position.y); //speed��ŭ �̵�
    }
}
