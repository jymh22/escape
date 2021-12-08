using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject Bullet; //������ �Ѿ�
    public float spawnRateMin = 1f; //�ּ� ���� �ֱ�
    public float spawnRateMax = 3f; // �ִ� ���� �ֱ�
    public float spawnBottomPositionMIN = -1f; //��ȯ ���� ��ġ ���� 
    public float spawnTopPositionMax = 5f; //��ȯ ��ġ �ִ� ���� ����

    private Transform target; // Player ��ġ 
    private float spawnRate; //���� �ֱ�
    private float spawnRandomPosition; // ��ȯ ���� ��ġ
    private float timeAfterSpawn; //�ֱ� �����ð����κ��� ���� �ð�

    void Start()
    {
        timeAfterSpawn = 0f;

        spawnRandomPosition = Random.Range(spawnBottomPositionMIN, spawnTopPositionMax); //��ȯ ��ġ ���� ����

        target = FindObjectOfType<PlayerController>().transform; // �÷��̾� ��ġ ã��
    }


    void Update()
    {
        FireBullet();
    }

    private void FireBullet()
    {
        transform.position = new Vector2(transform.position.x, target.transform.position.y + spawnRandomPosition);
        //MosterSpawner�� ��ġ�� Y���� Player�� ���� 
        timeAfterSpawn += Time.deltaTime;
        if (transform.position.y > 63)
        {

            if (timeAfterSpawn >= spawnRate) //�����ֱ⸦ �����ϸ�
            {
                timeAfterSpawn = 0f;

                spawnRate = Random.Range(spawnRateMin, spawnRateMax); // ��ȯ �����ð� ����
                spawnRandomPosition = Random.Range(spawnBottomPositionMIN, spawnTopPositionMax); //��ȯ ������ġ ����

                GameObject bullet = Instantiate(Bullet, transform.position, transform.rotation); //�Ѿ� ��ȯ
                if (this.name == "LeftBulletSpawner")
                    bullet.name = "leftbullet";
            }
        }

    }
}
