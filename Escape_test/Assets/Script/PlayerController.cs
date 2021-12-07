using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 50f; //�����Ҷ� �޴� ��
    public float moveSpeed = 5f; //�¿�� ������ ���� �ӵ�

    private bool isleft = false; //�������� Ȯ��
    private bool isGrounded = false; // �ٴڿ� ��Ҵ��� ����
    private bool isMoved = false; // ad ������ ����

    [SerializeField] //private������ ������Ʈ�� ������ �� �ִٴ� ���� ����
    private bool isCrouched = false; // �ɴ��� ����

    private Rigidbody2D playerRigidbody; //������ٵ�
    private Animator animator; // �ִϸ�����
    private PlayerHit PlayerHit; //��ũ��Ʈ
    private CapsuleCollider2D CapsuleCollider2D;


    private void Start()
    {
        //������Ʈ ������ ������ �Ҵ�
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        PlayerHit = GetComponent<PlayerHit>();
        CapsuleCollider2D = GetComponent<CapsuleCollider2D>();

    }

    private void Update()
    {
        PlayerMove();
        PlayerJump();
        PlayerAni();
    }

    private void PlayerMove()
    {
        float xInput = Input.GetAxis("Horizontal"); //����� �Է��� x���� ����
        //���Ű, A D Ű, ��ƽ ���� Ű ���� �Ϲ����� �Է»Ӹ��� �ƴ϶� ���ϸ� ���ϴ� Ű�� �ν��ϵ��� �ٲ� ���� ����

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
                transform.Rotate(0f, 180f, 0f); //ĳ���͸� 180�� ȸ���Ͽ� ������ �ٶ󺸴� ��ó�� ����
                isleft = true; //�ٽ� ������ ���� ��� �ٽ� ȸ������ �ʵ��� ��
            }
            else if (PlayerLookright)
            {
                transform.Rotate(0f, 180f, 0f); //180�� ȸ���Ͽ� �������� �ٶ󺸴� ��ó�� ����
                isleft = false;
            }
        }
        else { isMoved = false; }
    }

    private void PlayerJump()
    {
        float yInput = Input.GetAxis("Vertical"); //����� �Է��� y���� ����

        bool PlayerJump = (yInput > 0) && (isGrounded == true) && (!PlayerHit.isHit);  //���� �Է��� && �� ���� ���� ��
        bool PlayerJumpEnd = (yInput == 0) && (playerRigidbody.velocity.y > 0) && (!PlayerHit.isHit); // �� ���� ���� && y���� ���
        if (PlayerJump)
        {
            playerRigidbody.velocity = Vector2.zero; //������ �� �ӵ� �ʱ�ȭ
            //playerRigidbody.AddForce(new Vector2(0, jumpForce));
            playerRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); //jumpForce��ŭ�� ������ ����
        }
        else if (PlayerJumpEnd)
        {
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f; 
            //�����ϴٰ� ���� ��� �ö󰡴� �ӵ��� �������� ����
            //�̷� ���� Ű�� ���� �� ���� �� ���� ������
        }

        bool OnPlayerSit = (yInput < 0) && (isGrounded);
        if (OnPlayerSit) //���� ���ϰ� s,��Ű�ϋ� �ɱ�
        {
 //circle ��Ʈ�ڽ� �����Ͽ� Capsule(�Ͼ���� ���� �ݶ��̴�)�� ��� �����
            CapsuleCollider2D.enabled = false; //������� ��Ʈ�ڽ� ��Ȱ��ȭ
            isCrouched = true; // �ɾ����� ����
        } else { 
            CapsuleCollider2D.enabled = true; //�Ͼ ��� ��Ʈ�ڽ� Ȱ��ȭ
            isCrouched = false;
        }

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
        if (collision.contacts[0].normal.y > 0.9f && collision.collider.CompareTag("Ground"))
        { //y������ ��ü ǥ�鿡 ���, �� ��ü�� �±װ� Ground�� ��� 
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