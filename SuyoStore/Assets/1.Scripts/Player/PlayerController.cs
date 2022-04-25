using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private float gravity = -9.81f;

    CharacterController characterController;
    DataManager _dataManager;
    UIManager _uiManager;
    ScenarioEvent _scenarioEvent;
    public PlayerStatus pStatus;
    GameObject nearItem;
    GameObject nearZombie;
    GameObject playerObj;
    GameObject itemObj;
    ItemControl itemControl;
    GameObject zombieObj;
    ZombieAI zombieAI;
    // Related Zombie
    public bool isSafe = false;

    // Move
    private float rotationSpeed = 1000f; // 회전(방향전환) 속도
    private Vector3 moveDirection; // 이동 방향
    float hAxis;
    float vAxis;

    // Action
    public enum PlayerState{ Idle, Walk, Run, Sit, SitWalk, Lay, Dead };
    public PlayerState state = PlayerState.Idle;
    
    Animator animator;
    public bool isMove = false;
    bool isSit = false;

    // Status
    int useStamina = 10;
    int recoverStamina = 5;


    //weapon
    public GameObject[] Weapons;

    //light
    public GameObject[] Lights;

    //bag
    public GameObject[] Bags;

    // Attack
    public bool hasWeapon;
    Weapon equipWeapon;
    public GameObject nearScenarioItem;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        pStatus = GetComponent<PlayerStatus>();
        animator = GetComponentInChildren<Animator>();
        itemObj = GameObject.FindGameObjectWithTag("Item");
        itemControl = itemObj.GetComponent<ItemControl>();
        _dataManager = FindObjectOfType<DataManager>();
        _uiManager = FindObjectOfType<UIManager>();
        _scenarioEvent = _uiManager.GetComponent<ScenarioEvent>();
    }

    private void Update()
    {
        Anim();
        GetInput();
        if(state != PlayerState.Dead)
        {
            Move();
        }
    }

    void Anim()
    {
        animator.SetBool("isIdle", state == PlayerState.Idle);
        animator.SetBool("isWalk", state == PlayerState.Walk);
        animator.SetBool("isRun", state == PlayerState.Run);
        animator.SetBool("isSit", state == PlayerState.Sit);
        animator.SetBool("isSitWalk", state == PlayerState.SitWalk);
        animator.SetBool("isDie", state == PlayerState.Dead);
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

        if (Input.GetMouseButtonUp(0)) {
            Attack();
        }
    }


    void RunInput()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            if (isSit)
            {
                if (isMove) state = PlayerState.SitWalk;
                else state = PlayerState.Sit;
            } 
            else
            {
                state = PlayerState.Run;
                if (pStatus.CurStamina > 0)
                {
                    pStatus.UseStamina(useStamina);
                }
                else
                {
                    if (isMove) state = PlayerState.SitWalk;
                    else state = PlayerState.Sit;
                }
            }
        }
        ChangeSpeed();
    }

    void Move()
    {
        // 캐릭터에 중력 적용
        if (characterController.isGrounded == false)
        {
            moveDirection.y += gravity * Time.deltaTime;
        }

        moveDirection = new Vector3(hAxis, 0, vAxis).normalized;
        moveDirection.Normalize();

        if (state != PlayerState.Run)
        {
            if (pStatus.CurStamina < pStatus.Stamina)
            {
                pStatus.RecoveryStamina(recoverStamina);
            }
        }

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
                if(state == PlayerState.Idle || state == PlayerState.Walk)
                {
                    RunInput();
                }
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

        if (state == PlayerState.Run)
        {
            // When stop running, Reset useTime to Time for prevent to stop (ex.)0.54 second
            pStatus.UseRecoveryStaminaTime = pStatus.RecoveryStaminaTime;
        }
        else {
            // When stop running, Reset useTime to Time for prevent to stop (ex.)0.54 second
            pStatus.UseStaminaTime = pStatus.StaminaTime;
        }

        float magnitud = Mathf.Clamp01(moveDirection.magnitude) * pStatus.CurSpeed;
        characterController.Move(moveDirection* pStatus.CurSpeed * Time.deltaTime);
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

    // Judge Item ojbect near Player For GetItem()
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Item")
        {
            Debug.Log("[Trigger System] Item : " + hit.gameObject.name);
            nearItem = hit.gameObject;
        }
        else
        {
            nearItem = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ScenarioAsset")
        {
            nearScenarioItem = other.gameObject;
            _scenarioEvent.GetScenarioItem(nearScenarioItem);
        }
        else
        {
            nearScenarioItem = null;
        }

        if (other.tag == "Zombie")
        {
            Debug.Log("[Trigger System] Zombie!!!!");
            nearZombie = other.gameObject;
        }
        else
        {
            nearZombie = null;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "SenarioAsset")
        {
            nearScenarioItem = null;
        }

        if (other.tag == "Zombie")
        {
            Debug.Log("[Trigger System] Zombie Out!!!!");
            nearZombie = null;
        }
    }

    void GetItem()
    {
        if (state == PlayerState.Idle || state == PlayerState.Sit)
        {
            if(nearItem.tag == "Item")
            {
                // 파밍 가능
                pStatus.CurFatigue--;

                // 타겟 아이템 위치가 바닥일 때:
                animator.SetTrigger("PickUp");
                // 타겟 아이템 위치가 바닥이 아닐 때:
                /* 애니메이션 : CatchingItem */
                StopCoroutine(WaitGetItemTime(1.0f));
                StartCoroutine(WaitGetItemTime(1.0f));
            }
        }
        else
        {
            // 파밍 불가능
            Debug.Log("[Move System] Can't Get Item");
        }

    }
    IEnumerator WaitGetItemTime(float time)
    {
        yield return new WaitForSeconds(time);
        nearItem.GetComponent<ItemControl>().GetThisItem();
        //Destroy(hit.gameObject);
    }

    void SwitchWeapon()
    {
        //int weaponindex = itemID;
        //hasweapons[weaponindex] = true;
        //weapons[weaponindex].SetActive(true);
    }

    void Attack()
    {
        if (state == PlayerState.Idle || state == PlayerState.Sit)
        {
            if (nearZombie.tag == "Zombie")
            {
                isSit = false;
                pStatus.CurFatigue -= 2;

                //if(equipWeapon == null)
                if(!hasWeapon)
                {
                    // 무기 미착용 상태
                    animator.SetTrigger("PunchNearZombie");
                    //pStatus.EquipItemsList
                }
                else
                {
                    // 무기 착용 상태
                    animator.SetTrigger("SwingNearZombie");
                    ItemUse itemUse = _dataManager.GetComponent<ItemUse>();

                    //공격할 때마다 장착한 무기 내구도 줄어들음
                    foreach(int i in pStatus.EquipItemsList)
                    {
                        if(_dataManager.GetItemSubCategory(i) == "무기")
                        {
                            itemUse.SetItemDURABILITY(i);
                        }
                    }
                }

                state = PlayerState.Idle;
                zombieAI = nearZombie.GetComponent<ZombieAI>();
                zombieAI.Hit();
            }
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
