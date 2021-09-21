using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 500f;
    public float moveSpeed = 7f;

    private bool isleft = false;
    private int JumpCount = 0;
    private bool isGrounded = false; // 바닥에 닿았는지 여부
    private bool isCrouched = false; // 앉는지 여부
    private bool isMoved = false; // ad 움직임 여부

    private Rigidbody2D playerRigidbody; //리지드바디
    private Animator animator; // 애니메이터


    private void Start()
    {
        //컴포넌트 가져와 변수에 할당
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
  
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        //    float xSpeed = xInput * moveSpeed * Time.deltaTime;

        //사용자 입력을 감지 
        //좌우 앉기 점프 

        if (xInput != 0)// 왼쪽인지 오른쪽인지 입력축 판단여부 1이면 d,→키, -1이면 a,←키
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


        if (yInput>0 && JumpCount < 1)//점프인지 앉기인지 입력축 이름 판단여부 1이면 w,↑키, -1이면 s,↓키
        {
            JumpCount ++;
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
        } else if (yInput == 0 && playerRigidbody.velocity.y > 0)
        { // 손 떼는 순간 && y값이 양수라면 속도 절반
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        }

        if (yInput < 0 && JumpCount < 1) //점프 안하고 s,↓키일떄 
        {
            isCrouched = true;
        } else { isCrouched = false; }
            
            animator.SetBool("Grounded", isGrounded);
            animator.SetBool("Moved", isMoved);
            animator.SetBool("Crouched", isCrouched);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //바닥에 닿았음을 감지
        if (collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            JumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //바닥에서 벗어났음을 감지
        isGrounded = false;
    }
}
