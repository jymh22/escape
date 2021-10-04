using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 50f;
    public float moveSpeed = 5f;

    private bool isleft = false;
    private bool isGrounded = false; // �ٴڿ� ��Ҵ��� ����
    private bool isCrouched = false; // �ɴ��� ����
    private bool isMoved = false; // ad ������ ����

    private Rigidbody2D playerRigidbody; //������ٵ�
    private Animator animator; // �ִϸ�����
    private PlayerHit PlayerHit; //��ũ��Ʈ


    private void Start()
    {
        //������Ʈ ������ ������ �Ҵ�
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        PlayerHit = GetComponent<PlayerHit>();
    }

    private void Update()
    {
        PlayerMove();
        PlayerJump();
        PlayerAni();
    }

    private void PlayerMove()
    {
        float xInput = Input.GetAxis("Horizontal"); //����� �Է��� ����



        bool PlayerMove = (xInput != 0) && (isCrouched != true) && !PlayerHit.isHit; //x�� �Է� && �Ͼ ������ 
        if (PlayerMove)
        {
            isMoved = true;
            transform.position = new Vector2(transform.position.x + xInput * moveSpeed * Time.deltaTime, transform.position.y); //�¿� �̵�

            //ĳ���� ���� ����
            bool PlayerLookleft = xInput < 0 && isleft != true;
            bool PlayerLookright = xInput > 0 && isleft != false;
            if (PlayerLookleft)
            {
                transform.Rotate(0f, 180f, 0f);
                isleft = true;
            }
            else if (PlayerLookright)
            {
                transform.Rotate(0f, 180f, 0f);
                isleft = false;
            }
        }
        else { isMoved = false; }
    }

    private void PlayerJump()
    {
        float yInput = Input.GetAxis("Vertical"); //����� �Է��� ����

        bool PlayerJump = yInput > 0 && isGrounded == true && !PlayerHit.isHit;  //���� �Է��� && �� ���� ���� ��
        bool PlayerJumpEnd = yInput == 0 && playerRigidbody.velocity.y > 0 && !PlayerHit.isHit; // �� ���� ���� && y���� ���
        if (PlayerJump)
        {
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.AddForce(new Vector2(0, jumpForce)); //jumpForce��ŭ�� ������ ����
        }
        else if (PlayerJumpEnd)
        {
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f; //�ӵ� ����
        }

        bool OnPlayerSit = yInput < 0 && isGrounded != false;
        if (OnPlayerSit) //���� ���ϰ� s,��Ű�ϋ� �ɱ�
        {
            isCrouched = true;
        }
        else { isCrouched = false; }

    }

    private void PlayerAni()
    {
        animator.SetBool("Grounded", isGrounded); //���� �ִϸ��̼� 
        animator.SetBool("Moved", isMoved); //�̵� �ִϸ��̼�
        animator.SetBool("Crouched", isCrouched); //�ɱ� �ִϸ��̼�
    }



    private void OnCollisionStay2D(Collision2D collision)
    {
        //�ٴڿ� ������� ����
        if (collision.contacts[0].normal.y > 0.7f && collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            PlayerHit.isHit = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //�ٴڿ��� ������� ����
        isGrounded = false;
    }
}