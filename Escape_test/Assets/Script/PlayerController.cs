using UnityEngine;

public class PlayerController : MonoBehaviour
{ //�÷��̾� ��Ʈ�� ��ũ��Ʈ
    public float jumpForce = 50f; //�����Ҷ� �޴� ��
    public float moveSpeed = 5f; //�¿�� ������ ���� �ӵ�

    private bool isleft = false; //�������� Ȯ��
    private bool isGrounded = false; // �ٴڿ� ��Ҵ��� ����
    private bool isMoved = false; // ad ������ ����

    [SerializeField] //private������ ������Ʈ�� ������ �� �ִٴ� ���� ����
    private bool isCrouched = false; // �ɴ��� ����

    private Rigidbody2D playerRigidbody; //��ġ ���� ������Ʈ
    private Animator animator; // �ִϸ��̼� ������Ʈ
    private PlayerHit PlayerHit; //�÷��̾� �ǰ� ��ũ��Ʈ
    private CapsuleCollider2D CapsuleCollider2D; //�浹 ���� ������Ʈ
    private PropsAltar alter; //GameEnd Ȯ���� ���� ��Ʈ��Ʈ


    private void Start() //������
    {
        //�� ������Ʈ�� ��ũ��Ʈ ���� �Ҵ�
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        PlayerHit = GetComponent<PlayerHit>();
        CapsuleCollider2D = GetComponent<CapsuleCollider2D>();
        alter = FindObjectOfType<PropsAltar>();

    }

    private void Update() //�� ������ ���� ����
    {
        PlayerMove(); //�÷��̾� �̵�
        PlayerJump(); //�÷��̾� ���� && �ɱ�
        PlayerAni(); //�÷��̾� �ִϸ��̼�
    }

    private void PlayerMove()
    {
        float xInput = Input.GetAxis("Horizontal"); //����� �Է��� x���� �������
        bool PlayerMove = (xInput != 0) && (!isCrouched) && (!PlayerHit.isHit);
        //��� �Է� && �Ͼ ���� �� && �ǰ� ���°� �ƴ� ��
        if (PlayerMove)
        {
            isMoved = true; //�����̴� �������� �˸�
            transform.position = new Vector2(transform.position.x + xInput * moveSpeed * Time.deltaTime, transform.position.y);
            //moveSpeed��ŭ�� �ӵ��� �¿� �̵�

            //ĳ���� ���� ��ȯ ����
            bool PlayerLookleft = (xInput < 0) && (!isleft); //�������� ���� ���� �� ���� ��ư ���� ���
            bool PlayerLookright = (xInput > 0) && (isleft); //������ ���� ���� �� ������ ��ư ���� ���
            if (PlayerLookleft || PlayerLookright)
            { 
                transform.Rotate(0f, 180f, 0f); //ĳ���͸� 180�� ȸ���Ͽ� �ݴ����� �ٶ󺸴� ��ó�� ����
                isleft = !isleft; //�ݴ����� ���� �ִٰ� ����(�淮��ȯ)
            }
        }
        else { isMoved = false; } //�������� ���� ��� isMoved ��Ȱ��ȭ
    }

    private void PlayerJump()
    {
        float yInput = Input.GetAxis("Vertical"); //����� �Է��� y���� �������

        bool PlayerJump = (yInput > 0) && (isGrounded) && (!PlayerHit.isHit);
        //�� �Է� && �� ���� ���� �� && �ǰ� ���� �ƴ� ��
        bool PlayerJumpEnd = (yInput == 0) && (playerRigidbody.velocity.y > 0) && (!PlayerHit.isHit);
        //���� �� �迡�� �ն� �� && �ǰ� ���� �ƴ� ��
        if (PlayerJump)
        {
            playerRigidbody.velocity = Vector2.zero; //������ �� �ӵ� �ʱ�ȭ
            playerRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); //jumpForce��ŭ�� ������ ����
        }
        else if (PlayerJumpEnd)
        {
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f; 
            //�����ϴٰ� ���� ��� �ö󰡴� �ӵ��� �������� ����
            //�̷� ���� Ű�� ���� �� ���� �� ���� ������
        }

        bool OnPlayerSit = (yInput < 0) && (isGrounded); //�Ʒ���ư ������ ��, �� ���� ���� ��
        if (OnPlayerSit)
        {
            CapsuleCollider2D.enabled = false; //������� ������ ��, ��Ʈ�ڽ� ��Ȱ��ȭ
            isCrouched = true; // �ɾ����� ����
        } else { 
            CapsuleCollider2D.enabled = true; //�Ͼ ��� ��Ʈ�ڽ� �ٽ� Ȱ��ȭ
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
    { //�ٸ� ��ü�� �����ϴ� ���� (�ٴڿ� ������� ����)
        if (collision.contacts[0].normal.y > 0.7f && collision.collider.CompareTag("Ground"))
        { //��ü�� ���ʿ� ���, �� ��ü�� Ground�� ��� 
            isGrounded = true; //isGroundedȰ��ȭ
            PlayerHit.isHit = false; //�ǰ� ���� ����
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    { //�ٸ� ��ü���� ������ ����� ���� (�ٴڿ��� ������� ����)
        isGrounded = false; //isGrounded��Ȱ��ȭ
    }

}