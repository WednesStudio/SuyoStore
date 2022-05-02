using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieControllering : MonoBehaviour
{
    public int hp;
    public int curHp;
    public float curSpeed;
    public float speed;
    public int power;
    public float CoolTime;
    public int infection = 5; // 감염률
    [SerializeField] float targetFindRange = 8f; // 감지범위
    [SerializeField] float targetRadius = 0.7f; // 공격 위치
    [SerializeField] float rayMaxDis = 0.7f;

    // Related on Target(= Player)
    [SerializeField] GameObject target;
    PlayerStatus targetStatus;
    PlayerController targetController;

    // Relaated on Zombie
    NavMeshAgent nav;
    Rigidbody zomRigid;
    BoxCollider attackArea;
    Animator zombieAnim;
    
    bool isAttack = false; // 플레이어와 닿아서 플레이어를 공격 중인지
    bool isTutorial = true; // 튜토리얼 진행 중인지
    private bool isFindPlayer = false;
    private bool isAttackRange = false;
    RaycastHit rayhit;
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        targetStatus = target.GetComponent<PlayerStatus>();
        targetController = target.GetComponent<PlayerController>();

        //zombieSp = GameObject.Find("ZombieSpawner").GetComponent<ZombieSpawner>();
        
        nav = GetComponent<NavMeshAgent>();
        zomRigid = GetComponent<Rigidbody>();
        attackArea = GetComponentInChildren<BoxCollider>();
        zombieAnim = GetComponent<Animator>();
    }

    void Start()
    {
        hp = 100;
        speed = 4.0f;
        power = 3;
        CoolTime = 1.0f;
        curHp = hp;
        curSpeed = speed;
        attackArea.enabled = false;
    }

    void Update()
    {
        if (nav.enabled)
        {
            nav.SetDestination(target.transform.position); // ai로 도착할 장소(타겟 대상)
            nav.isStopped = isAttackRange; //플레이어를 공격 범위 안이면 네비게이션 종료
        }

        if (curHp <= 0) Die();
    }

    private void FixedUpdate()
    {
        Targeting();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, targetFindRange); // 감지범위
    }
    void Targeting()
    {
        // 감지 범위 내에서 플레이어 탐색
        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position,
                                targetFindRange,
                                transform.up,
                                0.0f,
                                LayerMask.GetMask("Player"));

        // 플레이어가 감지 범위 안에 있는지
        if (rayHits.Length > 0) isFindPlayer = true;
        else isFindPlayer = false;



        // 플레이어가 공격 범위 안에 있는지
        //RaycastHit[] rayAtkHits = Physics.SphereCastAll(transform.position,
        //                targetRadius,
        //                transform.up,
        //                0.0f,
        //                LayerMask.GetMask("Player"));
        //if (rayAtkHits.Length > 0) isAttackRange = true;
        //else isAttackRange = false;

        isAttackRange = Physics.SphereCast(transform.position,
                        targetRadius,
                        transform.forward,
                        out rayhit,
                        0,
                        LayerMask.GetMask("Player"));

        if (isFindPlayer)
        {
            Debug.Log("탐색 중 : 플레이어 감지");
            zombieAnim.SetBool("isWalk", true);

            if (isAttackRange && !isAttack && !targetController.isSafe)
            {
                Debug.Log("추적 완료 : 플레이어를 공격 중");
                StartCoroutine(Attack());
                curSpeed = 0;
                zomRigid.velocity = Vector3.zero;
                zomRigid.angularVelocity = Vector3.zero;
            }
            else
            {
                curSpeed = speed;
            }
        }
        else
        {
            Debug.Log("탐색 중 : 감지 범위에 없음");
            zombieAnim.SetBool("isWalk", false);
        }
    }
    
    IEnumerator Attack()
    {
        isAttack = true;
        if (!targetStatus.isInfect)
        {
            if (Random.Range(1, 101) <= infection)
            {
                targetStatus.isInfect = true;
                zombieAnim.SetTrigger("doInfect");
                Debug.Log("감염되었습니다");
            }
        }
        yield return new WaitForSeconds(0.2f);
        zombieAnim.SetBool("isAttack", isAttack);
        Debug.Log("공격 중");
        attackArea.enabled = true;
        targetController.isDamage = true;

        yield return new WaitForSeconds(CoolTime);
        attackArea.enabled = false;
        isAttack = false;
        zombieAnim.SetBool("isAttack", isAttack);

        yield return new WaitForSeconds(CoolTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // 무기 공격 범위에 닿으면 좀비 체력 감소
        if (other.tag == "Melee")
        {
            curHp -= targetStatus.CurAttack;
            Debug.Log("[Zombie System] weapon Hit : " + curHp);
        }
    }

    public void Die()
    {
        Debug.Log("[Zombie System] Die");
        attackArea.enabled = false; // 플레이어가 이미 죽은 좀비를 더 공격하지 않도록 콜라이더 끄기
        isAttack = false;
        nav.enabled = false;
        zombieAnim.SetTrigger("doDie");
        GetComponent<ParticleSystem>().Play();
        StartCoroutine(DieEffect());
    }

    IEnumerator DieEffect()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
