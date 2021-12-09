using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{

    public void backTitle()
    {
        SceneManager.LoadScene(0);
    }

    public void gameStart()
    {
        SceneManager.LoadScene(1);
    }
    public void gameQuit()
    {
        Application.Quit();
    }
}