using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float FiringCycle = 2f; //�߻� �ֱ�
    private float CycleTime = 0f; //���� ����Ŭ �ð�

    public GameObject Bullet;
    public Transform FirePos;


    void Update()
    {
        CycleTime += Time.deltaTime; //�ʴ� 1�� ����
        if (CycleTime > FiringCycle) //�� ����Ŭ ������ ��
        {
            //�����Ѵ�. //'Bullet'�� 'FirePos.transform.position' ��ġ�� 'FirePos.transform.rotation' ȸ��������.
            GameObject bullet = Instantiate(Bullet, FirePos.position, FirePos.rotation);
            CycleTime = 0f;
            if(transform.rotation.y == 1)
                bullet.name = "leftbullet";
        }
    }
}
