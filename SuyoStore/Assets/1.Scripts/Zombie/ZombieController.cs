using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    AIController zomAI;
    NavMeshAgent navMA;

    public int hp;
    public int curHp;
    public float curSpeed;
    public float speed;
    public int power;
    public float CoolTime;
    public int infection = 5; // 감염률

    // Related on Target(= Player)
    [SerializeField] GameObject target;
    PlayerStatus targetStatus;
    PlayerController targetController;

    // Relaated on Zombie
    Rigidbody zomRigid;
    BoxCollider attackArea;
    Animator zombieAnim;
    bool isAttack = false; // 플레이어와 닿아서 플레이어를 공격 중인지

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        targetStatus = target.GetComponent<PlayerStatus>();
        targetController = target.GetComponent<PlayerController>();

        //zombieSp = GameObject.Find("ZombieSpawner").GetComponent<ZombieSpawner>();
        navMA = GetComponent<NavMeshAgent>();
        zomAI = GetComponent<AIController>();
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
    private void FixedUpdate()
    {
        zomRigid.velocity = Vector3.zero;
        zomRigid.angularVelocity = Vector3.zero;
    }
    public void Update()
    {
        if(navMA.speed > 0)
        {
            zombieAnim.SetBool("isWalk", true);
        }
        else
        {
            zombieAnim.SetBool("isWalk", false);
        }
        if (zomAI.m_CaughtPlayer)
        {
            StartCoroutine(Attack());
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
