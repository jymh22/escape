using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{ //���� �޴��� : ������ ���۰� ��, UIǥ��

    private GameObject Player; //Player ��ġ
    public GameObject gameClearText; //���� ����� Ȱ��ȭ
    public Text timeText; //����ð� ���� Text
    public Text recordText; //�ְ��� ���� Text

    private PropsAltar alter; //GameEnd Ȯ���� ���� ��Ʈ��Ʈ
    private float playTime; //�÷��� �� ����ð�


    void Start() //������
    {
        playTime = 0f; //�ð� �ʱ�ȭ
        alter = FindObjectOfType<PropsAltar>(); //GameEnd Ȯ���� ���� ��ũ��Ʈ �Ҵ�
    }

    void Update() //�� ������ ���� ����
    {
        playTime += Time.deltaTime; //�ð� ��� ���
        GamePlayingManager();
    }


    private void GamePlayingManager()
    {
        if (!alter.gameEnd) //���� �������� ���
            timeText.text = "Time: " + (int)playTime; //Text ���� �ð����� ������Ʈ
        else
            endGame(); //���� ����
    }

    public void endGame()
    {
        gameClearText.SetActive(true); //Ŭ���� Text Ȱ��ȭ

        float bestTime = PlayerPrefs.GetFloat("BestTime"); //���� �ְ��� �޾ƿ�

        if (playTime < bestTime || bestTime == 0)
        { //�ְ��� ����
            bestTime = playTime;
            PlayerPrefs.SetFloat("BestTime", bestTime);
        }

        recordText.text = "Best Time: " + (int)bestTime; //�ְ��� �ؽ�Ʈ ����
        Player.gameObject.SetActive(false); //���� ����� ĳ���� ��Ȱ��ȭ

    }


}

