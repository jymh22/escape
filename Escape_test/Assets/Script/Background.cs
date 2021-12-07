using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public GameObject BG; //������ ���
    private float width; //����

    public float speed = 3f; //����� �̵��ӵ�

    private void Awake()//�ν��Ͻ� �ε� �߿� ȣ��
    {
        BoxCollider2D backgroundCollider = GetComponent<BoxCollider2D>();
        width = backgroundCollider.size.x; //����� ���� ũ�� �Ҵ�
    }


    void Update()
    {
        ScrollingObject(); //�������� �̵���Ŵ


        if (transform.position.x <= -width*2f)  Reposition();
        //�̵��Ҹ�ŭ �̵������� �ٽ� ��ġ�� ������

    }

    private void Reposition()
    {
        Vector2 offset = new Vector2(width*3f, 0);
        transform.position = (Vector2)transform.position + offset;
        //���� ��ġ���� offset��ŭ ��ġ�� �����༭ ����ġ�� ���ƿ�
        
    }

    private void ScrollingObject()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime); //�������� �̵�
    }

}
