using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject Chiken; //생성할 몬스터
    public float spawnRateMin = 1f; //최소 생성 주기
    public float spawnRateMax = 3f; // 최대 생성 주기
    public float spawnBottomPositionMIN = -1f; //소환 낮은 위치 설정 
    public float spawnTopPositionMax = 5f; //소환 위치 최대 높이 설정

    private Transform target; // Player 위치 
    private float spawnRate; //생성 주기
    private float spawnRandomPosition; // 소환 랜덤 위치
    private float timeAfterSpawn; //최근 생성시간으로부터 지난 시간

    void Start()
    {
        timeAfterSpawn = 0f;

        spawnRandomPosition = Random.Range(spawnBottomPositionMIN, spawnTopPositionMax); //소환 위치 랜덤 결정

        target = FindObjectOfType<PlayerController>().transform; // 플레이어 위치 찾기
    }


    void Update()
    {

        transform.position = new Vector2(transform.position.x , target.transform.position.y + spawnRandomPosition);
        //MosterSpawner의 위치의 Y축을 Player에 맞춤 

        timeAfterSpawn += Time.deltaTime; 

        if(timeAfterSpawn>= spawnRate) //생성주기를 오버하면
        {
            timeAfterSpawn = 0f;

            spawnRate = Random.Range(spawnRateMin, spawnRateMax); // 소환 랜덤시간 결정
            spawnRandomPosition = Random.Range(spawnBottomPositionMIN, spawnTopPositionMax); //소환 랜덤위치 결정

            GameObject chiken = Instantiate(Chiken, transform.position, transform.rotation); //치킨 소환
         //   chiken.transform.LookAt(target);
        }


    }
}
