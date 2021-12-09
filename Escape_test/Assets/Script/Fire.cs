using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{ //Plants ������ �Ѿ��� �߻��ϴ� ��ũ��Ʈ
    public float FiringCycle = 2f; //�߻� �ֱ�
    private float CycleTime = 0f; //���� ����Ŭ �ð�

    public GameObject Bullet; //�Ѿ� ������Ʈ�� �޾ƿ�
    public Transform FirePos; //�Ѿ� ���� ��ġ�� �޾ƿ�(���� �Ժκ�)


    void Update() //1������ ���� ����
    {
        CycleTime += Time.deltaTime; //�ʴ� 1�� ����
        MonsterFire();
    }

    private void MonsterFire()
    {
        if (CycleTime > FiringCycle) //�� ����Ŭ ������ ��
        {
            BulletSpawn(); //�Ѿ� ��ȯ
            CycleTime = 0f; //����Ŭ �ð� �ʱ�ȭ
        }
    }

    private void BulletSpawn() //�Ѿ� ��ȯ
    {
        GameObject bullet = Instantiate(Bullet, FirePos.position, FirePos.rotation);
        //�����Ѵ�. //'Bullet'�� 'FirePos.transform.position' ��ġ�� 'FirePos.transform.rotation' ȸ��������.
        if (transform.rotation.y == 1) bullet.name = "leftbullet";  //���ʿ��� ��� �Ѿ� �� ��� �Ѿ� �̸� ����
    }
}
