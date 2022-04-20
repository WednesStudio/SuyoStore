using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    public PlayerStatus pStatus;
    GameObject playerObj;

    public bool isSafe = false;

    [SerializeField]
    private float rotationSpeed = 720f; // 회전(방향전환) 속도
    private Vector3 moveDirection; // 이동 방향
    float hAxis;
    float vAxis;

    // 액션
    public enum PlayerState{ Idle, Walk, Run, Sit, Attack, Lay, Dead };
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
        animator.SetBool("isWalk", moveDirection != Vector3.zero);
        animator.SetBool("isRun", pStatus.CurStamina != 0 && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)));

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

        if (state == PlayerState.Run)
        {
            pStatus.CurSpeed = pStatus.WalkSpeed + pStatus.RunAddSpeed;
        }

        if (state == PlayerState.Sit)
        {
            pStatus.CurSpeed = pStatus.SitSpeed;
        }
    }

    void GetInput()
    {
        // Move Input
        hAxis = Input.GetAxisRaw("Horizontal"); // 방향키 좌우
        vAxis = Input.GetAxisRaw("Vertical"); // 방향키 위아래

        if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
        {
            Sit();
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
        if (state != PlayerState.Sit)
        {
            state = PlayerState.Run;
            ChangeSpeed();
            pStatus.StaminaModifier();
            Debug.Log("[Anim] Run");
        }

    }

    void Move()
    {
        moveDirection = new Vector3(hAxis, 0, vAxis).normalized;
        moveDirection.Normalize();

        float magnitud = Mathf.Clamp01(moveDirection.magnitude) * pStatus.CurSpeed;
        characterController.SimpleMove(moveDirection * pStatus.CurSpeed);

        // 움직임 여부 체크
        if (moveDirection != Vector3.zero)
        {
            isMove = true;

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                RunInput();
            }
            else
            {
                state = PlayerState.Walk;
                ChangeSpeed();
            }

            if (pStatus.CurStamina <= 0)
            {
                state = PlayerState.Walk;
            }

            // 바라보는 방향으로 회전
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            characterController.transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        }
        else
        {
            isMove = false;
            state = PlayerState.Idle;
            /* 애니메이션 : Idle */
            Debug.Log("[Anim] Idle");
        }
    }

    void Sit()
    {
        if (state == PlayerState.Idle || state == PlayerState.Walk || state == PlayerState.Run)
        {
            if (isSit == false)
            {
                isSit = true;
                state = PlayerState.Sit;
                /* 애니메이션 : Sit */
                Debug.Log("[Move System] Player is Sitting");
                Debug.Log("[Anim] Sit");
            }
            else
            {
                // Release a sit state
                Debug.Log("[Move System] Player is Standing Up");
                isSit = false;
            }
        }
        else
        {
            Debug.Log("Player can't Sit");
            isSit = false;
        }
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
