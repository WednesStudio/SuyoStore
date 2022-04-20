using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    public PlayerStatus pStatus;
    GameObject playerObj;

    public bool isSafe = false;

    [SerializeField]
    private float rotationSpeed = 1000f; // 회전(방향전환) 속도
    private Vector3 moveDirection; // 이동 방향
    float hAxis;
    float vAxis;

    // 액션
    public enum PlayerState{ Idle, Walk, Run, Sit, SitWalk, Attack, Lay, Dead };
    public PlayerState state = PlayerState.Idle;
    
    public bool isMove = false;
    bool isSit = false;

    Animator animator;

    GameObject nearObject;

    public void Awake()
    {
        characterController = GetComponent<CharacterController>();
        pStatus = GetComponent<PlayerStatus>();
        animator = GetComponentInChildren<Animator>();
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        //animator.SetBool("isWalk", moveDirection != Vector3.zero);
        //animator.SetBool("isRun", pStatus.CurStamina != 0 && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)));

        GetInput();
        Move();
    }

    void ChangeSpeed()
    {
        //if (state == PlayerState.Walk) pStatus.CurSpeed = 0;
        if (state == PlayerState.Idle || state == PlayerState.Walk)
        {
            pStatus.CurSpeed = pStatus.WalkSpeed;
        }
        else if (state == PlayerState.Run)
        {
            pStatus.CurSpeed = pStatus.WalkSpeed + pStatus.RunAddSpeed;
        }
        else if (state == PlayerState.Sit || state == PlayerState.SitWalk)
        {
            pStatus.CurSpeed = pStatus.SitSpeed;
        }
        else
        {
            pStatus.CurSpeed = pStatus.WalkSpeed;
            Debug.Log("[Move System] ?? State");
        }
    }

    void GetInput()
    {
        // Move Input
        hAxis = Input.GetAxisRaw("Horizontal"); // 방향키 좌우
        vAxis = Input.GetAxisRaw("Vertical"); // 방향키 위아래

        if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
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
    void RunInput()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            state = PlayerState.Run;
            pStatus.UseStamina(2);
            Debug.Log("[Anim] Run");

            if (pStatus.CurStamina <= 0)
            {
                state = PlayerState.Walk;
            }
        }
        else
        {
            pStatus.RecoveryStamina(5);
        }
        ChangeSpeed();
    }

    void Move()
    {
        moveDirection = new Vector3(hAxis, 0, vAxis).normalized;
        moveDirection.Normalize();

        // 움직임 여부 체크
        if (moveDirection != Vector3.zero)
        {
            isMove = true;

            if (isSit == true)
            {
                state = PlayerState.SitWalk;
            }
            else
            {
                state = PlayerState.Walk;
                RunInput();
            }
            ChangeSpeed();

            // 바라보는 방향으로 회전
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            characterController.transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            isMove = false;

            if(isSit == true)
            {
                state = PlayerState.Sit;
            }
            else
            {
                state = PlayerState.Idle;
            }
            ChangeSpeed();
        }

        float magnitud = Mathf.Clamp01(moveDirection.magnitude) * pStatus.CurSpeed;
        characterController.SimpleMove(moveDirection* pStatus.CurSpeed);
    }

    void SitInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
        {
            if(isSit == false)
            {
                isSit = true;
                if (moveDirection != Vector3.zero)
                {
                    state = PlayerState.SitWalk;
                    Debug.Log("[Move System] Player is Sitting");
                    /* 애니메이션 : Sit */
                }
                else
                {
                    state = PlayerState.Sit;
                }
            }
            else
            {
                isSit = false;
            }
        }
        ChangeSpeed();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Item")
        {
            Debug.Log("[Move System] Item : " + hit.gameObject.name);

            hit.gameObject.GetComponent<ItemControl>().GetThisItem();
            Destroy(hit.gameObject);
        }
    }

    void GetItem()
    {
        if (state == PlayerState.Idle || state == PlayerState.Sit)
        {
            // 파밍 가능
            pStatus.CurFatigue--;
            Debug.Log("[Move System] Get Item");


            // 타겟 아이템 위치가 바닥일 때:
            /* 애니메이션 : PickUpItem */

            // 타겟 아이템 위치가 바닥이 아닐 때:
            /* 애니메이션 : CatchingItem */


            /*
             * 타겟 아이템 사라짐
             * 인벤토리에 타겟 아이템 추가
             */

            // 아이템 루팅 완료 시 기본 자세로 전환
        }
        else
        {
            Debug.Log("[Move System] Can't Get Item");
            // 파밍 불가능
        }

    }

    void Attack()
    {
        if (state == PlayerState.Idle || state == PlayerState.Sit)
        {
            pStatus.CurFatigue -= 2;
            Debug.Log("[Move System] player attack zombie");

            // 무기 착용 상태
            /* 애니메이션 : WeaponAttack */

            // 무기 미착용 상태
            /* 애니메이션 : FistAttack */

            state = PlayerState.Idle;
        }
        else
        {
            Debug.Log("[Move System] player can't attack");
            return; // 공격 불가능
        }
    }

    void LayDown()
    {
        //if(사용 아이템 == 가구템 || 침낭템)

        if (state == PlayerState.Idle || state == PlayerState.Sit)
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
        else {
            return; // 눕기 불가능
        }
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
