using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{ //Plants 몬스터의 총알을 발사하는 스크립트
    public float FiringCycle = 2f; //발사 주기
    private float CycleTime = 0f; //현재 사이클 시간

    public GameObject Bullet; //총알 오브젝트를 받아옴
    public Transform FirePos; //총알 생성 위치를 받아옴(몬스터 입부분)


    void Update() //1프레임 마다 실행
    {
        CycleTime += Time.deltaTime; //초당 1씩 증가
        MonsterFire();
    }

    private void MonsterFire()
    {
        if (CycleTime > FiringCycle) //한 사이클 끝났을 때
        {
            BulletSpawn(); //총알 소환
            CycleTime = 0f; //사이클 시간 초기화
        }
    }

    private void BulletSpawn() //총알 소환
    {
        GameObject bullet = Instantiate(Bullet, FirePos.position, FirePos.rotation);
        //복제한다. //'Bullet'을 'FirePos.transform.position' 위치에 'FirePos.transform.rotation' 회전값으로.
        if (transform.rotation.y == 1) bullet.name = "leftbullet";  //왼쪽에서 쏘는 총알 일 경우 총알 이름 변경
    }
}
