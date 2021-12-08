using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float BulletSpeed = 1.0f;
    public float destroyTime = 10f; // 소멸 시간

    private void Start()
    {
        Destroy(gameObject, destroyTime); // 몬스터 제거(물체, 소멸시간(second))
        if (this.name == "leftbullet")
            BulletSpeed *= -1;
    }
    void Update()
    {
        //프레임마다 오브젝트를 로컬좌표상에서 앞으로 1의 힘만큼 날아가라
        transform.position = new Vector2(transform.position.x - (BulletSpeed * Time.deltaTime), transform.position.y); //speed만큼 이동
    }
}
