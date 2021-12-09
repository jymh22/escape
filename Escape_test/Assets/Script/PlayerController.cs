using UnityEngine;

public class PlayerController : MonoBehaviour
{ //플레이어 컨트롤 스크립트
    public float jumpForce = 50f; //점프할때 받는 힘
    public float moveSpeed = 5f; //좌우로 움직일 때의 속도

    private bool isleft = false; //왼쪽인지 확인
    private bool isGrounded = false; // 바닥에 닿았는지 여부
    private bool isMoved = false; // ad 움직임 여부

    [SerializeField] //private이지만 컴포넌트로 접근할 수 있다는 것을 말함
    private bool isCrouched = false; // 앉는지 여부

    private Rigidbody2D playerRigidbody; //위치 제어 컴포넌트
    private Animator animator; // 애니메이션 컴포넌트
    private PlayerHit PlayerHit; //플레이어 피격 스크립트
    private CapsuleCollider2D CapsuleCollider2D; //충돌 범위 컴포넌트
    private PropsAltar alter; //GameEnd 확인을 위한 스트립트


    private void Start() //생성자
    {
        //각 컴포넌트와 스크립트 변수 할당
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        PlayerHit = GetComponent<PlayerHit>();
        CapsuleCollider2D = GetComponent<CapsuleCollider2D>();
        alter = FindObjectOfType<PropsAltar>();

    }

    private void Update() //매 프레임 마다 실행
    {
        PlayerMove(); //플레이어 이동
        PlayerJump(); //플레이어 점프 && 앉기
        PlayerAni(); //플레이어 애니메이션
    }

    private void PlayerMove()
    {
        float xInput = Input.GetAxis("Horizontal"); //사용자 입력의 x축을 감지←→
        bool PlayerMove = (xInput != 0) && (!isCrouched) && (!PlayerHit.isHit);
        //←→ 입력 && 일어서 있을 시 && 피격 상태가 아닐 시
        if (PlayerMove)
        {
            isMoved = true; //움직이는 상태임을 알림
            transform.position = new Vector2(transform.position.x + xInput * moveSpeed * Time.deltaTime, transform.position.y);
            //moveSpeed만큼의 속도로 좌우 이동

            //캐릭터 방향 전환 설정
            bool PlayerLookleft = (xInput < 0) && (!isleft); //오른쪽을 보고 있을 때 왼쪽 버튼 누른 경우
            bool PlayerLookright = (xInput > 0) && (isleft); //왼쪽을 보고 있을 때 오른쪽 버튼 누른 경우
            if (PlayerLookleft || PlayerLookright)
            { 
                transform.Rotate(0f, 180f, 0f); //캐릭터를 180도 회전하여 반대쪽을 바라보는 것처럼 만듦
                isleft = !isleft; //반대쪽을 보고 있다고 저장(방량전환)
            }
        }
        else { isMoved = false; } //움직이지 않을 경우 isMoved 비활성화
    }

    private void PlayerJump()
    {
        float yInput = Input.GetAxis("Vertical"); //사용자 입력의 y축을 감지↑↓

        bool PlayerJump = (yInput > 0) && (isGrounded) && (!PlayerHit.isHit);
        //↑ 입력 && 땅 위에 있을 시 && 피격 상태 아닐 시
        bool PlayerJumpEnd = (yInput == 0) && (playerRigidbody.velocity.y > 0) && (!PlayerHit.isHit);
        //점프 후 ↑에서 손뗄 때 && 피격 상태 아닐 시
        if (PlayerJump)
        {
            playerRigidbody.velocity = Vector2.zero; //점프할 때 속도 초기화
            playerRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); //jumpForce만큼의 힘으로 점프
        }
        else if (PlayerJumpEnd)
        {
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f; 
            //점프하다가 떼는 경우 올라가는 속도를 절반으로 만듦
            //이로 인해 키를 빨리 뗄 수록 더 빨리 떨어짐
        }

        bool OnPlayerSit = (yInput < 0) && (isGrounded); //아래버튼 눌렀을 때, 땅 위에 있을 때
        if (OnPlayerSit)
        {
            CapsuleCollider2D.enabled = false; //앉을경우 서있을 때, 히트박스 비활성화
            isCrouched = true; // 앉았음을 감지
        } else { 
            CapsuleCollider2D.enabled = true; //일어날 경우 히트박스 다시 활성화
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
    { //다른 물체와 접촉하는 동안 (바닥에 닿았음을 감지)
        if (collision.contacts[0].normal.y > 0.7f && collision.collider.CompareTag("Ground"))
        { //물체의 위쪽에 닿고, 이 물체가 Ground일 경우 
            isGrounded = true; //isGrounded활성화
            PlayerHit.isHit = false; //피격 상태 제거
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    { //다른 물체와의 접촉을 벗어나는 순간 (바닥에서 벗어났음을 감지)
        isGrounded = false; //isGrounded비활성화
    }

}