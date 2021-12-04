using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private float width;
    private float height;

    public float speed = 3f;

    private void Awake()
    {
        BoxCollider2D backgroundCollider = GetComponent<BoxCollider2D>();
        width = backgroundCollider.size.x;
        height = backgroundCollider.size.y;
    }


    void Update()
    {
        ScrollingObject();
        if (transform.position.x <= -width)  Reposition();

    }

    private void Reposition()
    {
  
        Vector2 offset = new Vector2(width * 2, 0);
        transform.position = (Vector2)transform.position + offset;
    }

    private void ScrollingObject()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
