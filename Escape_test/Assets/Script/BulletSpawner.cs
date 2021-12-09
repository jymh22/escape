using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{ //�Ѿ� ������ ��ũ��Ʈ
  //�� ���� �������� Ȱ��ȭ, ����/�����ʿ��� �÷��̾� ��ó�� ���� ��ġ, ���� �ֱ�� �Ѿ� ����
    public GameObject Bullet; //������ �Ѿ� ������Ʈ
    public float spawnRateMin = 1f; //�ּ� ���� �ֱ�
    public float spawnRateMax = 3f; // �ִ� ���� �ֱ�
    public float spawnBottomPositionMIN = -1f; //��ȯ ��ġ �ּ� ���� 
    public float spawnTopPositionMax = 5f; //��ȯ ��ġ �ִ� ����

    private Transform target; //Player ��ġ
    private float spawnRate = 0; //���� �ֱ�
    private float spawnRandomPosition = 0; // ��ȯ ���� ��ġ
    private float timeAfterSpawn; //�ֱ� �����ð����κ��� ���� �ð�

    void Start() //������
    {
        timeAfterSpawn = 0f; //�ð� �ʱ�ȭ
        target = FindObjectOfType<PlayerController>().transform; //�÷��̾� ��ġ �Ҵ�
    }


    void Update() //1�����Ӹ��� ����
    {
        timeAfterSpawn += Time.deltaTime; //�ð� ��� üũ
        FireBullet(); //�Ѿ� �߻�
    }

    private void FireBullet()
    {
        bool fire = timeAfterSpawn >= spawnRate && target.position.y > 63;
        //�����ֱ⸦ ���� && �÷��̾� ��ġ�� ���������� ����
        if (fire)
        {
            BulletSpawn(); //�Ѿ� ��ȯ
            timeAfterSpawn = 0f; //�ð� �ʱ�ȭ
            SetSpawnLocation(); //���� ��ȯ �ð�, ��ġ ����
        }
    }

    private void BulletSpawn()
    {
        transform.position = new Vector2(transform.position.x, target.transform.position.y + spawnRandomPosition);
        //MosterSpawner�� ��ġ�� Y���� Player�� ����
        GameObject bullet = Instantiate(Bullet, transform.position, transform.rotation); //�Ѿ� ��ȯ
        if (this.name == "LeftBulletSpawner") bullet.name = "leftbullet"; //���ʿ��� ��� ��� �Ѿ� �̸� ����
    }

    private void SetSpawnLocation()
    {
        spawnRandomPosition = Random.Range(spawnBottomPositionMIN, spawnTopPositionMax); //���� ��ȯ ��ġ ���� ����
        spawnRate = Random.Range(spawnRateMin, spawnRateMax); //���� ��ȯ �����ð� ����
    }
}
