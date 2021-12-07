using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 50f; //점프할때 받는 힘
    public float moveSpeed = 5f; //좌우로 움직일 때의 속도

    private bool isleft = false; //왼쪽인지 확인
    private bool isGrounded = false; // 바닥에 닿았는지 여부
    private bool isMoved = false; // ad 움직임 여부

    [SerializeField] //private이지만 컴포넌트로 접근할 수 있다는 것을 말함
    private bool isCrouched = false; // 앉는지 여부

    private Rigidbody2D playerRigidbody; //리지드바디
    private Animator animator; // 애니메이터
    private PlayerHit PlayerHit; //스크립트
    private CapsuleCollider2D CapsuleCollider2D;


    private void Start()
    {
        //컴포넌트 가져와 변수에 할당
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
        float xInput = Input.GetAxis("Horizontal"); //사용자 입력의 x축을 감지
        //←→키, A D 키, 스틱 등의 키 등의 일반적인 입력뿐만이 아니라 원하면 원하는 키로 인식하도록 바꿀 수도 있음

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
                transform.Rotate(0f, 180f, 0f); //캐릭터를 180도 회전하여 왼쪽을 바라보는 것처럼 만듦
                isleft = true; //다시 왼쪽을 누를 경우 다시 회전하지 않도록 함
            }
            else if (PlayerLookright)
            {
                transform.Rotate(0f, 180f, 0f); //180도 회전하여 오른쪽을 바라보는 것처럼 만듦
                isleft = false;
            }
        }
        else { isMoved = false; }
    }

    private void PlayerJump()
    {
        float yInput = Input.GetAxis("Vertical"); //사용자 입력의 y축을 감지

        bool PlayerJump = (yInput > 0) && (isGrounded == true) && (!PlayerHit.isHit);  //점프 입력축 && 땅 위에 있을 시
        bool PlayerJumpEnd = (yInput == 0) && (playerRigidbody.velocity.y > 0) && (!PlayerHit.isHit); // 손 떼는 순간 && y값이 양수
        if (PlayerJump)
        {
            playerRigidbody.velocity = Vector2.zero; //점프할 때 속도 초기화
            //playerRigidbody.AddForce(new Vector2(0, jumpForce));
            playerRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); //jumpForce만큼의 힘으로 점프
        }
        else if (PlayerJumpEnd)
        {
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f; 
            //점프하다가 떼는 경우 올라가는 속도를 절반으로 만듦
            //이로 인해 키를 빨리 뗄 수록 더 빨리 떨어짐
        }

        bool OnPlayerSit = (yInput < 0) && (isGrounded);
        if (OnPlayerSit) //점프 안하고 s,↓키일떄 앉기
        {
 //circle 히트박스 제거하여 Capsule(일어서있을 때의 콜라이더)이 대신 사용중
            CapsuleCollider2D.enabled = false; //앉을경우 히트박스 비활성화
            isCrouched = true; // 앉았음을 감지
        } else { 
            CapsuleCollider2D.enabled = true; //일어날 경우 히트박스 활성화
            isCrouched = false;
        }

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
        if (collision.contacts[0].normal.y > 0.9f && collision.collider.CompareTag("Ground"))
        { //y축으로 물체 표면에 닿고, 이 물체의 태그가 Ground일 경우 
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