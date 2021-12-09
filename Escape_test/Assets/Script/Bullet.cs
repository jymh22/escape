using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{ //총알 오브젝트의 총알 이동 스크립트
    public float BulletSpeed = 1.0f; //총알 속도
    public float destroyTime = 10f; // 소멸 시간

    private void Start() //생성자
    {
        Destroy(gameObject, destroyTime); //destroyTime후에 몬스터 제거
        if (this.name == "leftbullet") BulletSpeed *= -1; //왼쪽에서 쏘는 총알일 경우 방향 변경
    }
    void Update() //1프레임마다 실행
    {
        transform.position = new Vector2(transform.position.x - (BulletSpeed * Time.deltaTime), transform.position.y);
        //speed만큼의 속도로 이동
    }
}
