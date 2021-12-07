using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public GameObject BG; //생성할 배경
    private float width; //가로

    public float speed = 3f; //배경의 이동속도

    private void Awake()//인스턴스 로드 중에 호출
    {
        BoxCollider2D backgroundCollider = GetComponent<BoxCollider2D>();
        width = backgroundCollider.size.x; //배경의 가로 크기 할당
    }


    void Update()
    {
        ScrollingObject(); //좌측으로 이동시킴


        if (transform.position.x <= -width*2f)  Reposition();
        //이동할만큼 이동했으면 다시 위치를 정해줌

    }

    private void Reposition()
    {
        Vector2 offset = new Vector2(width*3f, 0);
        transform.position = (Vector2)transform.position + offset;
        //현재 위치에서 offset만큼 위치를 더해줘서 원위치로 돌아옴
        
    }

    private void ScrollingObject()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime); //좌측으로 이동
    }

}
