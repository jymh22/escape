using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{ //Scene �̵� ��ũ��Ʈ (Scene��ȣ - 0 = Ÿ��Ʋ, 1 = �ΰ���)

    public void backTitle()
    {
        SceneManager.LoadScene(0); //Ÿ��Ʋ ��ư�� ���� �� Ÿ��Ʋ�� �ǵ��ư�
    }

    public void gameStart()
    {
        SceneManager.LoadScene(1); //���� ��ŸƮ ��ư�� ���� �� ���� ������ �Ѿ
    }
    public void gameQuit()
    {
        Application.Quit(); //Quit��ư ���� �� ������ ������
    }
}
