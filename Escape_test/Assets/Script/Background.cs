using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public GameObject BG; //������ ���
    private float width;
    private float Rewidth;

    public float speed = 3f;

    private void Awake()
    {
        BoxCollider2D backgroundCollider = GetComponent<BoxCollider2D>();
        width = backgroundCollider.size.x; //x ���� = width
    }


    void Update()
    {
        ScrollingObject();

        if (transform.position.x <= -width*2f)  Reposition();

    }

    private void Reposition()
    {
  
        Vector2 offset = new Vector2(width*3f, 0);
        transform.position = (Vector2)transform.position + offset;
    }

    private void ScrollingObject()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime); //�������� �̵�
    }

}
