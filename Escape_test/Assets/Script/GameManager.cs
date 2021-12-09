using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{ //게임 메니저 : 게임의 시작과 끝, UI표시

    private GameObject Player; //Player 위치
    public GameObject gameClearText; //게임 종료시 활성화
    public Text timeText; //경과시간 적은 Text
    public Text recordText; //최고기록 적은 Text

    private PropsAltar alter; //GameEnd 확인을 위한 스트립트
    private float playTime; //플레이 후 경과시간


    void Start() //생성자
    {
        playTime = 0f; //시간 초기화
        alter = FindObjectOfType<PropsAltar>(); //GameEnd 확인을 위한 스크립트 할당
    }

    void Update() //매 프레임 마다 실행
    {
        playTime += Time.deltaTime; //시간 경과 기록
        GamePlayingManager();
    }


    private void GamePlayingManager()
    {
        if (!alter.gameEnd) //게임 진행중인 경우
            timeText.text = "Time: " + (int)playTime; //Text 현재 시간으로 업데이트
        else
            endGame(); //게임 종료
    }

    public void endGame()
    {
        gameClearText.SetActive(true); //클리어 Text 활성화

        float bestTime = PlayerPrefs.GetFloat("BestTime"); //전의 최고기록 받아옴

        if (playTime < bestTime || bestTime == 0)
        { //최고기록 갱신
            bestTime = playTime;
            PlayerPrefs.SetFloat("BestTime", bestTime);
        }

        recordText.text = "Best Time: " + (int)bestTime; //최고기록 텍스트 변경
        Player.gameObject.SetActive(false); //게임 종료시 캐릭터 비활성화

    }


}

