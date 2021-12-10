using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//when something get into the alta, make the runes glow

    public class PropsAltar : MonoBehaviour
    { //게임 종료 시(종료 오브젝트에 닿을 시) 룬 4개의 색을 서서히 파란색으로 변경시킴 && 3초뒤 chiken 생성
    public List<SpriteRenderer> runes; //게임 종료시 보이는 룬 4개를 리스트로 받아옴
    public float lerpSpeed; //색 변화 속도
    public GameObject chiken; //생성될 닭 오브젝트
    public bool gameEnd = false; //게임 End 판별

    private Color curColor; //현재 색깔
    private Color targetColor; //변화할 목표 색깔
    private float timeAfter; //시간 경과

    private void Start() //생성자
    {
        timeAfter = 0; //시간 경과 초기화
        targetColor = new Color(1, 1, 1, 1); //목표 색깔 초기화
    }

    private void Update() //매 프레임 마다 실행
    {
        ChikenSpawn(); //Chiken 생성
        Setrunes(); //룬 4개 설정
    }

    private void ChikenSpawn()
    {
        if (gameEnd) //게임 종료 시
            timeAfter += Time.deltaTime; //게임 종료 후 시간 경과 기록
        if (timeAfter > 3) //종료후 3초가 지났을 때
            chiken.SetActive(true); //chiken생성
    }

    private void Setrunes()
    {
        curColor = Color.Lerp(curColor, targetColor, lerpSpeed * Time.deltaTime);
        //시간 경과에 따라 색깔을 서서히 목표 색깔로 변경
        foreach (var r in runes) //4개 모든 룬 각각
        {
            r.color = curColor; //룬의 색을 curColor로 바꿈
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    { //종료 오브젝트가 다른 트리거와 접촉하는 순간
        if (other.tag == "Player")//종료 오브젝트에 접촉한 다른 트리거가 플레이어일 경우
            gameEnd = true; //게임 종료
    }

}
