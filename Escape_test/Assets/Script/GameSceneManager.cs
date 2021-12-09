using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{ //Scene 이동 스크립트 (Scene번호 - 0 = 타이틀, 1 = 인게임)

    public void backTitle()
    {
        SceneManager.LoadScene(0); //타이틀 버튼을 누를 시 타이틀로 되돌아감
    }

    public void gameStart()
    {
        SceneManager.LoadScene(1); //게임 스타트 버튼을 누를 시 게임 씬으로 넘어감
    }
    public void gameQuit()
    {
        Application.Quit(); //Quit버튼 누를 시 게임을 종료함
    }
}
