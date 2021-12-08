using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameClearText; //게임 종료시 활성화
    public Text timeText;
    public Text recordText;

    private float playTime;
    private bool isGameEnd;
    // Start is called before the first frame update
    void Start()
    {
        playTime = 0;
        isGameEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameEnd)
        {
            playTime += Time.deltaTime;
            timeText.text = "Time: " + (int)playTime;
        }

    }

    public void endGame()
    {
        isGameEnd = true;
        gameClearText.SetActive(true);

        float bestTime = PlayerPrefs.GetFloat("BestTime");

        if(playTime < bestTime)
        {
            bestTime = playTime;
            PlayerPrefs.SetFloat("BestTime", bestTime);

        }

        recordText.text = "Best Time: " + (int)bestTime;
    }

    public void SceneLoad()
    {
        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        SceneManager.LoadScene("1");
    }
}
