using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{ //총알 생성기 스크립트
  //맨 위쪽 구간에서 활성화, 왼쪽/오른쪽에서 플레이어 근처의 랜덤 위치, 랜텀 주기로 총알 생성
    public GameObject Bullet; //생성할 총알 오브젝트
    public float spawnRateMin = 1f; //최소 생성 주기
    public float spawnRateMax = 3f; // 최대 생성 주기
    public float spawnBottomPositionMIN = -1f; //소환 위치 최소 높이 
    public float spawnTopPositionMax = 5f; //소환 위치 최대 높이

    private Transform target; //Player 위치
    private float spawnRate = 0; //생성 주기
    private float spawnRandomPosition = 0; // 소환 랜덤 위치
    private float timeAfterSpawn; //최근 생성시간으로부터 지난 시간

    void Start() //생성자
    {
        timeAfterSpawn = 0f; //시간 초기화
        target = FindObjectOfType<PlayerController>().transform; //플레이어 위치 할당
    }


    void Update() //1프레임마다 실행
    {
        timeAfterSpawn += Time.deltaTime; //시간 경과 체크
        FireBullet(); //총알 발사
    }

    private void FireBullet()
    {
        bool fire = timeAfterSpawn >= spawnRate && target.position.y > 63;
        //생성주기를 오버 && 플레이어 위치가 일정구간에 도달
        if (fire)
        {
            BulletSpawn(); //총알 소환
            timeAfterSpawn = 0f; //시간 초기화
            SetSpawnLocation(); //다음 소환 시간, 위치 설정
        }
    }

    private void BulletSpawn()
    {
        transform.position = new Vector2(transform.position.x, target.transform.position.y + spawnRandomPosition);
        //MosterSpawner의 위치의 Y축을 Player에 맞춤
        GameObject bullet = Instantiate(Bullet, transform.position, transform.rotation); //총알 소환
        if (this.name == "LeftBulletSpawner") bullet.name = "leftbullet"; //왼쪽에서 쏘는 경우 총알 이름 변경
    }

    private void SetSpawnLocation()
    {
        spawnRandomPosition = Random.Range(spawnBottomPositionMIN, spawnTopPositionMax); //다음 소환 위치 랜덤 결정
        spawnRate = Random.Range(spawnRateMin, spawnRateMax); //다음 소환 랜덤시간 결정
    }
}
