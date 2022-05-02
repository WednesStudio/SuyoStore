using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    // Related on Target(= Player)
    [SerializeField] GameObject target;
    PlayerStatus targetStatus;
    PlayerController targetController;

    // Relaated on Zombie
    Rigidbody zomRigid;
    BoxCollider zomMeleeArea;
    Animator zombieAnim;

    public int hp;
    public int curHp;
    public float curSpeed;
    public float speed;
    public int power;
    public float CoolTime;
    public int infection = 5; // 감염률
    public bool isZomAttack; // 좀비가 플레이어를 공격 중인지
    bool isgrounded;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        targetStatus = target.GetComponent<PlayerStatus>();
        targetController = target.GetComponent<PlayerController>();

        zomRigid = GetComponent<Rigidbody>();
        zomMeleeArea = GetComponentInChildren<BoxCollider>();
        zombieAnim = GetComponent<Animator>();
    }
    void Start()
    {
        hp = 100;
        speed = 4.0f;
        power = 2;
        CoolTime = 1.5f;
        curHp = hp;
        curSpeed = speed;
        zomMeleeArea.enabled = false;
    }
    private void FixedUpdate()
    {
        zomRigid.velocity = Vector3.zero;
        zomRigid.angularVelocity = Vector3.zero;
    }
    private void Update()
    {
        zombieAnim.SetBool("isWalk", true);
        if(curHp<=0) Die();
    }

    private void Move()
    {

    }

    void ZombieAttack()
    {
        // 이미 공격하고 있지 않다면 공격
        if (!isZomAttack)
        {
            StartCoroutine(AttackCoolTime());
        }
    }
    IEnumerator AttackCoolTime()
    {
        transform.LookAt(target.transform.position);
        isZomAttack = true;
        zombieAnim.SetTrigger("doAttack");
        if (!targetStatus.isInfect)
        {
            if (Random.Range(1, 101) <= infection)
            {
                targetStatus.isInfect = true;
                zombieAnim.SetTrigger("doInfect");
                Debug.Log("감염되었습니다");
            }
        }

        // anim 타이밍에 맞춰서 zomMelee 활성화
        yield return new WaitForSeconds(0.2f);
        zomMeleeArea.enabled = true;
        
        // anim 타이밍에 맞춰서 zomMelee 비활성화
        yield return new WaitForSeconds(0.02f);
        zomMeleeArea.enabled = false;

        yield return new WaitForSeconds(CoolTime); // 다음 공격까지의 쿨타임
        Debug.Log("좀비 쿨타임 종료");
        isZomAttack = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 무기 공격 범위에 닿으면 좀비 체력 감소
        if (other.tag == "Melee")
        {
            if (targetController.isAttack)
            {
                curHp -= targetStatus.CurAttack;
                Debug.Log("[Zombie System] Hit : " + curHp);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            zombieAnim.SetBool("isWalk", false);
            ZombieAttack();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            zombieAnim.SetBool("isWalk", true);
            isZomAttack = false;
        }
    }

    public void Die()
    {
        Debug.Log("[Zombie System] Die");
        zomMeleeArea.enabled = false; // 플레이어가 이미 죽은 좀비를 더 공격하지 않도록 콜라이더 끄기
        isZomAttack = false;
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
