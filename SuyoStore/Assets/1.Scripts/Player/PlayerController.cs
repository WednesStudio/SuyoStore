using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    public PlayerStatus pStatus;
    GameObject playerObj;

    public bool isSafe = false;

    [SerializeField]
    private float speed = 1f; // 이동 속도

    [SerializeField]
    private float rotationSpeed = 360f; // 회전(방향전환) 속도
    private Vector3 moveDirection; // 이동 방향
    float hAxis;
    float vAxis;

    // 액션
    enum PlayerState{ Idle, Walk, Run, Sit, Attack, Lay, Dead };
    PlayerState state = PlayerState.Idle;
    Animator animator;

    public bool isMove = false;
    bool isSit = false;

    public void Awake()
    {
        characterController = GetComponent<CharacterController>();
        pStatus = GetComponent<PlayerStatus>();
        animator = GetComponent<Animator>();
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        GetInput();
        Move();
    }

    void ChangeSpeed()
    {
        if (state == PlayerState.Idle || state == PlayerState.Walk) speed = 0;
        if (state == PlayerState.Walk) speed = pStatus.WalkSpeed;
        if (state == PlayerState.Run) speed = pStatus.WalkSpeed + pStatus.RunAddSpeed;
        if (state == PlayerState.Sit) speed = pStatus.SitSpeed;
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal"); // 방향키 좌우
        vAxis = Input.GetAxisRaw("Vertical"); // 방향키 위아래

        if (Input.GetButtonUp("Sit"))
        {
            SitInput();
        }
        if (Input.GetKeyUp(KeyCode.G))
        {
            GetItem();
        }
        if (Input.GetMouseButton(0)) {
            Attack();
        }
    }

    void MoveInput()
    {
        state = PlayerState.Walk;
        isMove = true;

        if (Input.GetButton("Run"))
        {
            state = PlayerState.Run;
            /* 애니메이션 : Run */
            Debug.Log("[Anim] Run");
        }
    }

    void SitInput()
    {
        if (state == PlayerState.Idle || state == PlayerState.Walk || state == PlayerState.Run)
        {
            if (isSit == false)
            {
                isSit = true;
                state = PlayerState.Sit;
                /* 애니메이션 : Sit */
                Debug.Log("Player is Sitting");
                Debug.Log("[Anim] Sit");
            }
            else
            {
                // Release a sit state
                Debug.Log("Player is Standing Up");
                isSit = false;
            }
        }
        else
        {
            Debug.Log("Player can't Sit");
            isSit = false;
        }
    }

    void Move()
    {
        moveDirection = new Vector3(hAxis, 0, vAxis).normalized;
        moveDirection.Normalize();

        float magnitud = Mathf.Clamp01(moveDirection.magnitude) * speed;
        characterController.SimpleMove(moveDirection * speed);


        // 움직임 여부 체크
        if (moveDirection != Vector3.zero)
        {
            MoveInput();
            ChangeSpeed();

            // 바라보는 방향으로 회전
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            characterController.transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        }
        else
        {
            state = PlayerState.Idle;
            /* 애니메이션 : Idle */
            Debug.Log("[Anim] Idle");
        }
    }

    void GetItem()
    {
        if (state == PlayerState.Run)
        {
            Debug.Log("Can't Get Item");
            return; // 파밍 불가능
        }
        else if (state == PlayerState.Idle || state == PlayerState.Sit)
        {
            // 파밍 가능
            Debug.Log("Get Item");
            pStatus.isGet = true;
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
        if (state == PlayerState.Run)
        {
            Debug.Log("player can't attack");
         
            return; // 공격 불가능
        }
        else if (state == PlayerState.Idle || state == PlayerState.Sit)
        {
            Debug.Log("player is attack");
            Debug.Log("[Anim] Attack");

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
                //Idle();
            }

            // 눕기 가능
            state = PlayerState.Lay;

            /* 애니메이션 : LayDown */
            Debug.Log("[Anim] Lay");
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
