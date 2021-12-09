using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public GameObject gameClearText; //게임 종료시 활성화
    public Text timeText;
    public Text recordText;


    private PropsAltar alter;
    private float playTime;


    // Start is called before the first frame update
    void Start()
    {
        playTime = 0;
        alter = FindObjectOfType<PropsAltar>();

    }

    // Update is called once per frame
    void Update()
    {

        if (!alter.gameEnd)
        {
            playTime += Time.deltaTime;
            timeText.text = "Time: " + (int)playTime;
        } else
        {
            endGame();
        }

    }


    public void endGame()
    {
        gameClearText.SetActive(true);

        float bestTime = PlayerPrefs.GetFloat("BestTime");
        if(bestTime ==0)
            PlayerPrefs.SetFloat("BestTime", 100000);

        if (playTime < bestTime)
        {
            bestTime = playTime;
            PlayerPrefs.SetFloat("BestTime", bestTime);
        }

        recordText.text = "Best Time: " + (int)bestTime;
    }


}

