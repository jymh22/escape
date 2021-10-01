using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 50f;
    public float moveSpeed = 5f;
    public float hitForce = 10f;

 //   private bool bHit = false;
    private bool isleft = false;
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
        //����� �Է��� ���� 
        //�¿� �ɱ� ���� 

        if (xInput != 0 && isCrouched != true)// x�� �Է��� �Ǵܿ��� && �Ͼ ������ 
        {
            transform.position = new Vector2(transform.position.x + xInput * moveSpeed * Time.deltaTime, transform.position.y); //�¿� �̵�
            isMoved = true;

            if (xInput < 0 && isleft != true) //�������� �̵��Ҷ� ĳ���͵� ���� �ٶ󺸱�
            {
                transform.Rotate(0f, 180f, 0f);
                isleft = true;
            }
            else if (xInput > 0 && isleft != false) { // ���������� �̵��Ҷ� ĳ���͵� �ٽ� ������ �ٶ󺸱�
                transform.Rotate(0f, 180f, 0f);
                isleft = false; 
            }
        }
        else { isMoved = false; } 


        if (yInput>0 && isGrounded != false)//���� �Է��� && �� ���� ���� ��
        {
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
        } else if (yInput == 0 && playerRigidbody.velocity.y > 0)
        { // �� ���� ���� && y���� ������ �ӵ� ����
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        }

        if (yInput < 0 && isGrounded != false) //���� ���ϰ� s,��Ű�ϋ� �ɱ�
        {
            isCrouched = true;
        } else { isCrouched = false; }
            
            animator.SetBool("Grounded", isGrounded); //���� �ִϸ��̼� 
            animator.SetBool("Moved", isMoved); //�̵� �ִϸ��̼�
            animator.SetBool("Crouched", isCrouched); //�ɱ� �ִϸ��̼�
    }


    public void Hit() //�ǰ� ����
    {
  //      if (!bHit) return;
        playerRigidbody.velocity = Vector2.zero;
        playerRigidbody.AddForce(new Vector2( -400*hitForce, 100 * jumpForce)); // ��ġ�� - �̵������� �ݴ�������� ��ġ�� �ؾ���. ��������.
        animator.SetTrigger("hit"); // �ǰ� �ִϸ��̼�

        //       bHit = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //�ٴڿ� ������� ����
        if (collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //�ٴڿ��� ������� ����
        isGrounded = false;
    }

/*    public void fHit()
    {
        bHit = true;
    }
*/
}