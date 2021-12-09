using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{ //��� ��ũ��Ʈ

    public GameObject BG; //������ ��� ������Ʈ
    private float width; //���� ����

    public float speed = 3f; //����� �̵��ӵ�

    private void Awake() //������
    {
        BoxCollider2D backgroundCollider = GetComponent<BoxCollider2D>(); //����� ������ ����
        width = backgroundCollider.size.x; //����� ���� ũ�⸦ ����
    }


    void Update() //�� �����Ӹ��� ����
    {
        ScrollingObject(); //�������� �̵���Ŵ
        if (transform.position.x <= -width*2f)  Reposition();
        //���� ������ �̵��ϸ�, ���� ������ �ٽ� ����

    }
    private void ScrollingObject()
    { //�������� �̵�
        transform.Translate(Vector3.left * speed * Time.deltaTime); //�������� �̵�
    }

    private void Reposition()
    { //���� ������ �ٽ� ����
        Vector2 offset = new Vector2(width*3f, 0);
        transform.position = (Vector2)transform.position + offset;
        //���� ��ġ���� offset��ŭ ��ġ�� �����༭ ����ġ�� ���ƿ�
    }


}
