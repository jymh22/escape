using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 50f;
    public float moveSpeed = 5f;
    public float hitForce = 10f;

 //   private bool bHit = false;
    private bool isleft = false;
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
        //사용자 입력을 감지 
        //좌우 앉기 점프 

        if (xInput != 0 && isCrouched != true)// x축 입력축 판단여부 && 일어서 있으면 
        {
            transform.position = new Vector2(transform.position.x + xInput * moveSpeed * Time.deltaTime, transform.position.y); //좌우 이동
            isMoved = true;

            if (xInput < 0 && isleft != true) //왼쪽으로 이동할때 캐릭터도 왼쪽 바라보기
            {
                transform.Rotate(0f, 180f, 0f);
                isleft = true;
            }
            else if (xInput > 0 && isleft != false) { // 오른쪽으로 이동할때 캐릭터도 다시 오른쪽 바라보기
                transform.Rotate(0f, 180f, 0f);
                isleft = false; 
            }
        }
        else { isMoved = false; } 


        if (yInput>0 && isGrounded != false)//점프 입력축 && 땅 위에 있을 시
        {
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
        } else if (yInput == 0 && playerRigidbody.velocity.y > 0)
        { // 손 떼는 순간 && y값이 양수라면 속도 절반
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        }

        if (yInput < 0 && isGrounded != false) //점프 안하고 s,↓키일떄 앉기
        {
            isCrouched = true;
        } else { isCrouched = false; }
            
            animator.SetBool("Grounded", isGrounded); //서기 애니메이션 
            animator.SetBool("Moved", isMoved); //이동 애니메이션
            animator.SetBool("Crouched", isCrouched); //앉기 애니메이션
    }


    public void Hit() //피격 판정
    {
  //      if (!bHit) return;
        playerRigidbody.velocity = Vector2.zero;
        playerRigidbody.AddForce(new Vector2( -400*hitForce, 100 * jumpForce)); // 밀치기 - 이동방향의 반대방향으로 밀치게 해야함. 버그있음.
        animator.SetTrigger("hit"); // 피격 애니메이션

        //       bHit = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //바닥에 닿았음을 감지
        if (collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //바닥에서 벗어났음을 감지
        isGrounded = false;
    }

/*    public void fHit()
    {
        bHit = true;
    }
*/
}