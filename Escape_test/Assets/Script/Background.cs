using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{ //배경 스크립트

    public GameObject BG; //생성할 배경 오브젝트
    private float width; //가로 길이

    public float speed = 3f; //배경의 이동속도

    private void Awake() //생성자
    {
        BoxCollider2D backgroundCollider = GetComponent<BoxCollider2D>(); //배경의 정보를 저장
        width = backgroundCollider.size.x; //배경의 가로 크기를 저장
    }


    void Update() //매 프레임마다 실행
    {
        ScrollingObject(); //좌측으로 이동시킴
        if (transform.position.x <= -width*2f)  Reposition();
        //좌측 끝까지 이동하면, 우측 끝에서 다시 시작

    }
    private void ScrollingObject()
    { //좌측으로 이동
        transform.Translate(Vector3.left * speed * Time.deltaTime); //좌측으로 이동
    }

    private void Reposition()
    { //우측 끝에서 다시 시작
        Vector2 offset = new Vector2(width*3f, 0);
        transform.position = (Vector2)transform.position + offset;
        //현재 위치에서 offset만큼 위치를 더해줘서 원위치로 돌아옴
    }


}
