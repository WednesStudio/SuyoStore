using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;

    private float speed = 1f; // 이동 속도
    [SerializeField]
    private float moveSpeed = 10.0f; // 기본 상태일 때 이동 속도
    [SerializeField]
    private float runSpeed = 5.0f; // 달리기 상태일 때 이동 속도
    [SerializeField]
    private float sitSpeed = 3.0f; // 앉기 상태일 떄 이동 속도

    //private float gravity = -9.81f; // 중력 계수
    [SerializeField]
    private float rotationSpeed = 360f; // 회전(방향전환) 속도
    private Vector3 moveDirection; // 이동 방향
    float hAxis;
    float vAxis;

    // 액션
    enum PlayerState{ Idle, Walk, Run, Sit, Attack, Lay, Dead };
    PlayerState state = PlayerState.Idle;
    bool isIdle = true;
    bool isAlt; // alt 키를 눌렀는지 여부
    bool isAttack; 
    bool isRun;
    Animator animator;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        //if (characterController.isGrounded == false)
        //{
        //    moveDirection.y += gravity * Time.deltaTime;
        //}
        GetInput();

        Move();
        if (isAlt == true) SitAction();
        if (Input.GetKey(KeyCode.G)) GetItem();
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal"); // 방향키 좌우
        vAxis = Input.GetAxisRaw("Vertical"); // 방향키 위아래

        // 액션 관련
        //isAlt = Input.GetButtonDown("Sit"); // alt 키 입력 여부
        //isAttack = Input.GetButtonDown("Attack");
        //isRun = Input.GetButton("Run");
    }

    void Move()
    {
        //moveDirection = new Vector3(hAxis, moveDirection.y, vAxis);
        //moveDirection.Normalize();
        //characterController.Move(moveDirection * speed * Time.deltaTime);

        moveDirection = new Vector3(hAxis, 0, vAxis);
        float magnitud = Mathf.Clamp01(moveDirection.magnitude) * speed;
        moveDirection.Normalize();
        characterController.SimpleMove(moveDirection * speed);


        // 움직임 여부 체크
        if (moveDirection != Vector3.zero)
        {
            speed = moveSpeed;

            // 바라보는 방향으로 회전
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            characterController.transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

            if (isRun == true) Run();
            else {
                state = PlayerState.Walk;
                /* 애니메이션 : Walk */
            }
        }
        else
        {
            Idle();
        }
    }

    void Idle()
    {
        if (state == PlayerState.Idle)
        {
            isIdle = true;
            state = PlayerState.Idle;
            speed = moveSpeed;
            
            /* 애니메이션 : Idle */
        }
    }

    void Run()
    {
        state = PlayerState.Run;
        speed += runSpeed;

        /* 애니메이션 : Run */
    }

    void SitAction()
    {
        if (state == PlayerState.Sit)
        {
            state = PlayerState.Idle;

            /* 애니메이션 : Idle */

        }
        else
        {
            state = PlayerState.Sit;
            speed = sitSpeed;

            /* 애니메이션 : Sit */
        }
    }


    void GetItem()
    {
        if (state == PlayerState.Run) return; // 파밍 불가능
        else if(state == PlayerState.Idle || state == PlayerState.Sit)
        {
            // 파밍 가능

            // 타겟 아이템 위치가 바닥일 때:
            /* 애니메이션 : PickUpItem */

            // 타겟 아이템 위치가 바닥이 아닐 때:
            /* 애니메이션 : CatchingItem */


            /*
             * 타겟 아이템 사라짐
             * 인벤토리에 타겟 아이템 추가
             */


            // 아이템 루팅 완료 시 기본 자세로 전환
            state = PlayerState.Idle;
        }
        else
        {
            
        }
    }

    void Attack()
    {
        if (state == PlayerState.Run) return; // 공격 불가능
        else if (state == PlayerState.Idle || state == PlayerState.Sit)
        {
            // 무기 착용 상태
                /* 애니메이션 : WeaponAttack */

            // 무기 미착용 상태
                /* 애니메이션 : FistAttack */
        }
        else
        {
        }
    }

    void LayDown()
    {
        //if(사용 아이템 == 가구템 || 침낭템)

        if (state == PlayerState.Run) return; // 눕기 불가능
        else if (state == PlayerState.Idle || state == PlayerState.Sit)
        {
            if (state == PlayerState.Sit)
            {
                Idle();
            }

            // 눕기 가능
            state = PlayerState.Lay;

            /* 애니메이션 : LayDown */

        }
        else { }
    }

    void Sleep()
    {
        /* 
         * 하루 스킵(day++;)
         * 체력, 포만감 스테이터스 변화(아이템에 따라 값이 달라짐)
         * 하루가 스킵된 이후 눕기 자세에서 기본 자세로 전환
         */
    }
}
