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
    public int infection = 5; // ������

    // Related on Target(= Player)
    [SerializeField] GameObject target;
    PlayerStatus targetStatus;
    PlayerController targetController;

    // Relaated on Zombie
    Rigidbody zomRigid;
    BoxCollider attackArea;
    Animator zombieAnim;
    bool isAttack = false; // �÷��̾�� ��Ƽ� �÷��̾ ���� ������

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
                Debug.Log("�����Ǿ����ϴ�");
            }
        }
        yield return new WaitForSeconds(0.2f);
        zombieAnim.SetBool("isAttack", isAttack);
        Debug.Log("���� ��");
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
        // ���� ���� ������ ������ ���� ü�� ����
        if (other.tag == "Melee")
        {
            curHp -= targetStatus.CurAttack;
            Debug.Log("[Zombie System] weapon Hit : " + curHp);
        }
    }

    public void Die()
    {
        Debug.Log("[Zombie System] Die");
        attackArea.enabled = false; // �÷��̾ �̹� ���� ���� �� �������� �ʵ��� �ݶ��̴� ����
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
