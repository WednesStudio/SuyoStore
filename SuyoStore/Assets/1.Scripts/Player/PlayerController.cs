using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private float gravity = -9.81f;

    CharacterController characterController;
    DataManager _dataManager;
    UIManager _uiManager;
    ScenarioEvent _scenarioEvent;
    Tutorial _tutorial;
    public PlayerStatus pStatus;
    GameObject nearItem;
    GameObject nearZombie;
    GameObject playerObj;
    GameObject itemObj;
    ItemControl itemControl;
    GameObject zombieObj;
    ZombieAI zombieAI;

    RaycastHit rayhit;

    // Related Zombie
    public bool isSafe = false;
    public GameObject nearSenarioItem;
    public bool isChangeFloor = false;

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
    public GameObject EquipWeapon;
    Weapon equipWeapon;
    float attackCoolTime;
    bool isAttackReady;

    //light
    public GameObject[] Lights;
    bool islightOn = false;
    //bag
    public GameObject[] Bags;

    // Item
    public bool hasWeapon;
    public bool hasFlashlight;
    public bool hasBag;
    public GameObject nearScenarioItem;
    public GameObject nearTutorialItem;

    // Spawn Floor

    private void Start()
    {
        //itemObj = GameObject.FindGameObjectWithTag("Item");
        //itemControl = itemObj.GetComponent<ItemControl>();
        //playerObj = GameObject.FindGameObjectWithTag("Player");
        characterController = GetComponent<CharacterController>();
        pStatus = GetComponent<PlayerStatus>();
        animator = GetComponentInChildren<Animator>();

        _dataManager = FindObjectOfType<DataManager>();
        _uiManager = FindObjectOfType<UIManager>();
        _scenarioEvent = _uiManager.GetComponent<ScenarioEvent>();
        _tutorial = _uiManager.GetComponent<Tutorial>();
    }

    void FreezeRoation()
    {
        //characterController.velocity = Vector3.zero;
    }
    private void FixedUpdate()
    {
    }

    private void Update()
    {
        Physics.SyncTransforms();

        // 장착한 무기에 대한 정보
        if (EquipWeapon != null)
        {
            equipWeapon = EquipWeapon.GetComponent<Weapon>();
        }
        //if (isMove && (!SoundManager.SM.isPlayingEnvironmentalSound()))
        //{
        //    SoundManager.SM.PlayEnvironmentalSound(EnvironmentalSoundName.WalkSound);
        //}
        //else
        //{
        //    SoundManager.SM.StopEnvironmentalSound();
        //}
        // When change scene, player don't be attacked by zombie

        Anim();
        GetInput();
        if(state != PlayerState.Dead)
        {
            Move();
        }

        if(_scenarioEvent.isShelterClear && gameObject.GetComponent<Transform>().position.y > -13 && gameObject.GetComponent<Transform>().position.y < 20)
        {
            GameManager.GM.SetEndEventTrigger();
        }

        //if (Physics.Raycast(this.transform.position, this.transform.forward, out rayhit, 10f))
        //{
        //    rayhit.transform;
        //}
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
        if (Input.GetKeyUp(KeyCode.F))
        {
            GetItem();
            //if (isMove && (!SoundManager.SM.isPlayingEnvironmentalSound()))
            //{
            //    SoundManager.SM.PlayEnvironmentalSound(EnvironmentalSoundName.GetItemSoound);
            //}
            //else
            //{
            //    SoundManager.SM.StopEnvironmentalSound();
            //}
        }

        if (Input.GetMouseButtonUp(0)) {
            if (state == PlayerState.Idle || state == PlayerState.Sit)
            {
                Attack();
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
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Item")
        {
            Debug.Log("[Trigger System] Item : " + other.gameObject.name);
            nearItem = other.gameObject;
        }
        else
        {
            nearItem = null;
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

        if (other.tag == "Zombie")
        {
            Debug.Log("[Trigger System] Zombie!!!!");
            nearZombie = other.gameObject;
            if (isMove && (!SoundManager.SM.isPlayingEnvironmentalSound()))
            {
                SoundManager.SM.PlayEnvironmentalSound(EnvironmentalSoundName.ZombieSound);
            }
            else
            {
                SoundManager.SM.StopSfxSound();
            }
        }
        else
        {
            nearZombie = null;
            SoundManager.SM.StopEnvironmentalSound();
        }
    }

    private void OnTriggerExit(Collider other)
    {
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
            Debug.Log("[Trigger System] Zombie Out!!!!");
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
            //StopCoroutine(WaitGetItemTime(1.0f));
            //StartCoroutine(WaitGetItemTime(1.0f));
            nearItem.GetComponent<ItemControl>().GetThisItem();
        }
        else if (nearItem.tag == "ScenarioAsset")
        {
            _scenarioEvent.GetScenarioItem(nearItem.gameObject);
        }
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
    IEnumerator WaitGetItemTime(float time)
    {
        yield return new WaitForSeconds(time);
        //Destroy(hit.gameObject);
    }
    // For 물리력이 NavAgent 이동을 방해하지 않게
    //void StopToWall()
    //{
    //    Debug.DrawRay(transform.position, transform.forward * 5, Color.green);
    //    isBorder = Physics.Raycast(transform.position, transform.forward, 5, LayerMask.GetMask("Wall"));
    //}
    void SwitchWeapon()
    {
        //int weaponindex = itemID;
        //hasweapons[weaponindex] = true;
        //weapons[weaponindex].SetActive(true);
    }

    void Attack()
    {
        if(nearZombie != null)
        {
            if (nearZombie.tag == "Zombie")
            {
                isSit = false;
                pStatus.CurFatigue -= 2;

                if (!hasWeapon)
                {
                    // 무기 미착용 상태
                    animator.SetTrigger("PunchNearZombie");
                    //pStatus.EquipItemsList
                    //SoundManager.SM.sour
                }
                else
                {
                    // 무기 착용 상태
                    animator.SetTrigger("SwingNearZombie");
                    ItemUse itemUse = _dataManager.GetComponent<ItemUse>();

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
                StartCoroutine(AttackCoolTime());
            
            
            state = PlayerState.Idle;
            }
        }
    }
    IEnumerator AttackCoolTime()
    {
        if (hasWeapon)
        {
            yield return new WaitForSeconds(0.75f); Debug.Log("atk2");
            nearZombie.GetComponent<ZombieController>().Hit();
        }
        else
        {
            yield return new WaitForSeconds(0.5f); Debug.Log("atk");
            nearZombie.GetComponent<ZombieController>().Hit();
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

    //void SafeTime()
    //{
    //    StopCoroutine(WaitSafeTime());
    //    isSafe = true;
    //    StartCoroutine(WaitSafeTime());
    //}

    //IEnumerator WaitSafeTime()
    //{
    //    yield return new WaitForSeconds(4.0f);
    //    isSafe = false;
    //    isChangeFloor = false;
    //}
}
