using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 500f;
    public float moveSpeed = 7f;

    private bool isleft = false;
    private int JumpCount = 0;
    private bool isGrounded = false; // �ٴڿ� ��Ҵ��� ����
    private bool isCrouched = false; // �ɴ��� ����
    private bool isMoved = false; // ad ������ ����

    private Rigidbody2D playerRigidbody; //������ٵ�
    private Animator animator; // �ִϸ�����


    private void Start()
    {
        //������Ʈ ������ ������ �Ҵ�
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
  
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        //    float xSpeed = xInput * moveSpeed * Time.deltaTime;

        //����� �Է��� ���� 
        //�¿� �ɱ� ���� 

        if (xInput != 0)// �������� ���������� �Է��� �Ǵܿ��� 1�̸� d,��Ű, -1�̸� a,��Ű
        {
           // transform.position = new Vector2(transform.position.x + xSpeed, transform.position.y);
            transform.position = new Vector2(transform.position.x + xInput * moveSpeed * Time.deltaTime, transform.position.y);
            isMoved = true;

            if (xInput < 0 && isleft != true)
            {
                transform.Rotate(0f, 180f, 0f);
                isleft = true;
            }
            else if (xInput > 0 && isleft != false) {
                transform.Rotate(0f, 180f, 0f);
                isleft = false; 
            }
                

        }
        else { isMoved = false; }


        if (yInput>0 && JumpCount < 1)//�������� �ɱ����� �Է��� �̸� �Ǵܿ��� 1�̸� w,��Ű, -1�̸� s,��Ű
        {
            JumpCount ++;
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
        } else if (yInput == 0 && playerRigidbody.velocity.y > 0)
        { // �� ���� ���� && y���� ������ �ӵ� ����
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        }

        if (yInput < 0 && JumpCount < 1) //���� ���ϰ� s,��Ű�ϋ� 
        {
            isCrouched = true;
        } else { isCrouched = false; }
            
            animator.SetBool("Grounded", isGrounded);
            animator.SetBool("Moved", isMoved);
            animator.SetBool("Crouched", isCrouched);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�ٴڿ� ������� ����
        if (collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            JumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //�ٴڿ��� ������� ����
        isGrounded = false;
    }
}
