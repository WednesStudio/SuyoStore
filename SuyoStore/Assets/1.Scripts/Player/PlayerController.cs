using UnityEngine;
using System.Collections;
public class PlayerController : MonoBehaviour
{
    OptionSettingUI _optionSettingUI;
    DataManager _dataManager;
    UIManager _uiManager;
    ScenarioEvent _scenarioEvent;
    Tutorial _tutorial;
    ItemControl itemControl;

    public PlayerStatus pStatus;
    Zombie zombieController;

    CharacterController characterController;
    Rigidbody rigid;

    GameObject nearItem;
    public GameObject nearZombie;
    GameObject hitObj;

    RaycastHit rayhit;
    bool isBorder;
    private GameObject hitZom;
    bool isZombie;
    public bool isSafe = false;
    public bool isChangeFloor = false;
    // Move
    private float rotationSpeed = 1000f; // 회전(방향전환) 속도
    private Vector3 moveDirection; // 이동 방향
    float hAxis;
    float vAxis;

    // Status
    int useStamina = 10;
    int recoverStamina = 5;

    //weapon
    public GameObject[] Weapons;
    public GameObject EquipWeapon;
    Weapon equipWeapon;
    float attackCoolTimer = 1.0f;

    //light
    public GameObject[] Lights;
    bool islightOn = false;
    //bag
    public GameObject[] Bags;

    // Item
    public bool hasWeapon = false;
    public bool hasFlashlight;
    public bool hasBag;
    public GameObject nearScenarioItem;
    public GameObject nearTutorialItem;


    // Action
    public enum PlayerState { Idle, Walk, Run, Sit, SitWalk, Lay, Dead };
    public PlayerState state = PlayerState.Idle;
    public bool isAttack;

    Animator animator;
    public bool isMove = false;
    bool isSit = false;
    public BoxCollider PunchMeleeArea;
    public bool isDamage;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        pStatus = GetComponent<PlayerStatus>();
        animator = GetComponentInChildren<Animator>();

        _dataManager = FindObjectOfType<DataManager>();
        _uiManager = FindObjectOfType<UIManager>();
        _scenarioEvent = _uiManager.GetComponent<ScenarioEvent>();
        _optionSettingUI = _uiManager.GetComponent<OptionSettingUI>();
        _tutorial = _uiManager.GetComponent<Tutorial>();
    }

    void FreezeRoation()
    {
        rigid.angularVelocity = Vector3.zero;
    }
    Vector3 collision;
    float targetRadius = 0.5f;

    void RayObject()
    {
        float rayMaxDis = 2f;
        var ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward * rayMaxDis, Color.red);

        isBorder = Physics.Raycast(ray, out rayhit, rayMaxDis, LayerMask.GetMask("Wall"));

        if (Physics.SphereCast(transform.position, targetRadius, transform.forward ,out rayhit, rayMaxDis, LayerMask.GetMask("Zombie")))
        {
            hitObj = rayhit.transform.gameObject;
            collision = rayhit.point;
            if (hitObj.tag == "Zombie")
            {
                isZombie = true;
            }
            else
            {
                isZombie = false;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(collision, targetRadius);
    }
    private void FixedUpdate()
    {
        FreezeRoation();
    }

    private void Update()
    {
        Physics.SyncTransforms();

        // 장착한 무기에 대한 정보
        if (EquipWeapon != null)
        {
            equipWeapon = EquipWeapon.GetComponent<Weapon>();
            PunchMeleeArea.enabled = false;
        }
        else
        {
            PunchMeleeArea.enabled = true;
        }

        GetInput();
        if(state != PlayerState.Dead)
        {
            Move();
        }
        Anim();

        if(_scenarioEvent.isShelterClear &&
            gameObject.GetComponent<Transform>().position.y > -13 &&
            gameObject.GetComponent<Transform>().position.y < 20)
        {
            GameManager.GM.SetEndEventTrigger();
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
        if (Input.GetKeyDown(KeyCode.I))
        {
            _optionSettingUI.OnStatusAndInventoryWindow();
        }
        if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
        {
            SitInput();
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            GetItem();
        }

        if (Input.GetMouseButtonUp(0)) {
            if (state == PlayerState.Idle || state == PlayerState.Sit)
            {
                if(nearZombie != null)
                {
                    if(nearZombie.tag == "Zombie" && !isAttack)
                    {
                        StartCoroutine(AttackCoolTime());
                    }
                }
            }
        }
    }


    void RunInput()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            if (isSit)
            {
                // Can't Run, only Sit
                if (isMove) state = PlayerState.SitWalk;
                else state = PlayerState.Sit;
            } 
            else
            {
                state = PlayerState.Run;

                if (pStatus.CurStamina > 0)
                {
                    // stamina 감소
                    pStatus.UseStamina(useStamina);
                }
                else
                {
                    // only walk, not Run
                    if (isMove) state = PlayerState.Walk;
                    else state = PlayerState.Idle;
                    // 앉는 거 아님
                    //if (isMove) state = PlayerState.SitWalk;
                    //else state = PlayerState.Sit;
                }
            }
        }
        ChangeSpeed();
    }

    void Move()
    {
        moveDirection = new Vector3(hAxis, 0, vAxis).normalized;
        if (!isBorder)
        {
            transform.position += moveDirection * pStatus.CurSpeed * Time.deltaTime;
        }

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
            transform.LookAt(transform.position + moveDirection);
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
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Zombie")
        {
            nearZombie = other.gameObject;
            zombieController = nearZombie.GetComponent<Zombie>();
        }

        if (other.tag == "Item")
        {
            Debug.Log("[Trigger System] Item : " + other.gameObject.name);
            nearItem = other.gameObject;
        }

        if (other.tag == "ScenarioAsset")
        {
            nearScenarioItem = other.gameObject;
            if(Input.GetMouseButtonUp(0))
            {
                _scenarioEvent.GetClickItemName(nearScenarioItem.name);
            }
            nearScenarioItem = null;
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

        if(other.tag == "ZombieMelee")
        {
            if (zombieController.isZomAttack)
            {
                pStatus.ReduceHp(zombieController.power);
                Debug.Log("Player Hit - hp : " + pStatus.CurHp);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Item")
        {
            nearItem = null;
        }

        if (other.tag == "SenarioAsset")
        {
            nearScenarioItem = null;
        }

        if(other.tag == "TutorialSpot")
        {
            nearTutorialItem = other.gameObject;
            Destroy(nearTutorialItem);
            _tutorial.GetExactTutorial();
        }

        if (other.tag == "Zombie")
        {
            nearZombie = null;
        }
    }

    void GetItem()
    {
        if (nearItem.tag == "Item")
        {
            // 파밍 가능
            pStatus.CurFatigue--;

            // 타겟 아이템 위치가 바닥일 때:
            animator.SetTrigger("PickUp");
            // 타겟 아이템 위치가 바닥이 아닐 때:
            /* 애니메이션 : CatchingItem */
            nearItem.GetComponent<ItemControl>().GetThisItem();
        }
        else if (nearItem.tag == "ScenarioAsset")
        {
            _scenarioEvent.GetScenarioItem(nearItem.gameObject);
        }
        else { }

        if (state == PlayerState.Idle || state == PlayerState.Sit)
        {
            Debug.Log("[Move System] Can't Get Item222");

        }
        else
        {
            // 파밍 불가능
            Debug.Log("[Move System] Can't Get Item");
        }

    }

    void Attack()
    {
        isSit = false;
        rigid.velocity = Vector3.zero;
        pStatus.CurFatigue -= 2;
        if (!hasWeapon)
        {
            // 무기 미착용 상태
            animator.SetTrigger("PunchNearZombie");
            //SoundManager.SM.sour
        }
        else
        {
            // 무기 착용 상태
            animator.SetTrigger("SwingNearZombie");
            ItemUse itemUse = _dataManager.GetComponent<ItemUse>();
            equipWeapon = EquipWeapon.GetComponent<Weapon>();
            // 무기로 공격
            equipWeapon.Use();
            //공격할 때마다 장착한 무기 내구도 줄어들음
            foreach (int i in pStatus.EquipWeaponList)
            {
                if (_dataManager.GetItemSubCategory(i) == "무기")
                {
                    itemUse.SetItemDURABILITY(i);
                }
            }
        }
        state = PlayerState.Idle;
    }

    IEnumerator AttackCoolTime()
    {
        isAttack = true;
        Attack();
        yield return new WaitForSeconds(attackCoolTimer);
        isAttack = false;
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


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
        }
    }
}
