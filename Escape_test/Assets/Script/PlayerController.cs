using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 50f;
    public float moveSpeed = 5f;

    private bool isleft = false;
    private bool isGrounded = false; // 바닥에 닿았는지 여부
    private bool isCrouched = false; // 앉는지 여부
    private bool isMoved = false; // ad 움직임 여부

    private Rigidbody2D playerRigidbody; //리지드바디
    private Animator animator; // 애니메이터
    private PlayerHit PlayerHit; //스크립트


    private void Start()
    {
        //컴포넌트 가져와 변수에 할당
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
        float xInput = Input.GetAxis("Horizontal"); //사용자 입력을 감지



        bool PlayerMove = (xInput != 0) && (isCrouched != true) && !PlayerHit.isHit; //x축 입력 && 일어서 있으면 
        if (PlayerMove)
        {
            isMoved = true;
            transform.position = new Vector2(transform.position.x + xInput * moveSpeed * Time.deltaTime, transform.position.y); //좌우 이동

            //캐릭터 방향 설정
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
        float yInput = Input.GetAxis("Vertical"); //사용자 입력을 감지

        bool PlayerJump = yInput > 0 && isGrounded == true && !PlayerHit.isHit;  //점프 입력축 && 땅 위에 있을 시
        bool PlayerJumpEnd = yInput == 0 && playerRigidbody.velocity.y > 0 && !PlayerHit.isHit; // 손 떼는 순간 && y값이 양수
        if (PlayerJump)
        {
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.AddForce(new Vector2(0, jumpForce)); //jumpForce만큼의 힘으로 점프
        }
        else if (PlayerJumpEnd)
        {
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f; //속도 절반
        }

        bool OnPlayerSit = yInput < 0 && isGrounded != false;
        if (OnPlayerSit) //점프 안하고 s,↓키일떄 앉기
        {
            isCrouched = true;
        }
        else { isCrouched = false; }

    }

    private void PlayerAni()
    {
        animator.SetBool("Grounded", isGrounded); //서기 애니메이션 
        animator.SetBool("Moved", isMoved); //이동 애니메이션
        animator.SetBool("Crouched", isCrouched); //앉기 애니메이션
    }



    private void OnCollisionStay2D(Collision2D collision)
    {
        //바닥에 닿았음을 감지
        if (collision.contacts[0].normal.y > 0.7f && collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            PlayerHit.isHit = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //바닥에서 벗어났음을 감지
        isGrounded = false;
    }
}