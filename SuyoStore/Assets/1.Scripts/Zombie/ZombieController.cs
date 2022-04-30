using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    GameObject zombie;
    // Player object & script related Player
    private GameObject target;
    private PlayerStatus targetStatus;
    private PlayerController targetController;

    private float timer;

    public int hp;
    private int curHp;
    public int detection; //���� ����
    Vector3 moveVec;
    public float curSpeed;
    public float speed;
    public int power;
    public int coolTime;
    public int infection = 5; // ������
    public Vector3 spawn; // ���� ��ġ
    public bool isDetect;
    //public bool isChase; // �÷��̾� �߰� ����
    public bool isRandom;
    public float range;
    public Animator zombieAnim;
    public GameObject child;
    ZombieSpawner zombieSp;
    Rigidbody zomRigid;

    bool isBorder;

    //Animation Controller Transition
    bool isAttacking = false; // �÷��̾�� ��Ƽ� �÷��̾ ���� ������
    bool isWalk = true;

    NavMeshAgent nav;

    private void Awake()
    {
        //mat = GetComponent<MeshRenderer>().material;
        nav = GetComponent<NavMeshAgent>();

        zombieSp = GameObject.Find("ZombieSpawner").GetComponent<ZombieSpawner>();
        zombie = gameObject;
        zomRigid = GetComponent<Rigidbody>();
        zombieAnim = GetComponent<Animator>();

        target = GameObject.FindGameObjectWithTag("Player");
        targetStatus = target.GetComponent<PlayerStatus>();
        targetController = target.GetComponent<PlayerController>();
    }
    void Start()
    {
        hp = 100;
        detection = 6;
        speed = 3.5f;

        timer = 1;
        isDetect = false;
        isRandom = false;
        curHp = hp;
        curSpeed = speed;
        spawn = transform.position;
        range = zombieSp.spawnRange;
    }

    void StopToWall()
    {
        Debug.DrawRay(transform.position, transform.forward * 5, Color.green);
        isBorder = Physics.Raycast(transform.position, transform.forward, 5, LayerMask.GetMask("Wall"));
    }
    void FreezeRotation()
    {
        zomRigid.angularVelocity = Vector3.zero;
    }
    private void FixedUpdate()
    {
        FreezeRotation();
    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(target.transform.position);
        zombieAnim.SetBool("isWalk", curSpeed > 0);

        timer -= Time.deltaTime;
        Move();
    }

    //Player Tag�� ���� ��ü�� ����� �� ����
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            curSpeed = 0;
            if (timer <= 0)
            {
                Attack();
                isAttacking = true;
            }
            else
            {
                isAttacking = false;
            }
            timer = coolTime;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            curSpeed = speed;
            isAttacking = false;
            zombieAnim.SetBool("isAttack", isAttacking);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // ���� ���� ������ ������ ���� ü�� ����
        if (other.tag == "Melee")
        {
            Hit();
        }
    }

    void Move()
    {
        //moveVec = new Vector3(
        if (!isBorder)
        {
            //transform.position += curSpeed * Time.deltaTime;
        }

        //target�� ��ġ�� zombie�� ��ü �Ÿ��� detection���� �۰ų�, �÷��̾ ���� ���� �� �߰�
        if ((!target.GetComponent<PlayerController>().isSafe)
            && ((Vector3.Distance(target.transform.position, transform.position) < detection)
            || isAttacking))
        {
            isDetect = true;
            transform.LookAt(target.gameObject.transform);
        }
        //���� �� ������ ��������� Ž���� ������� �Ǵ�
        else if (Vector3.Distance(spawn, transform.position) < 0.3)
        {
            isDetect = false;
        }
        //���� �̵�
        else if (!isDetect)
        {
            if (!isRandom)
                StartCoroutine("RandomMove");
        }
        //���� �� �������� �̵�
        else
        {
            transform.LookAt(spawn);
        }
        transform.Translate(Vector3.forward * Time.deltaTime * curSpeed);
    }

    IEnumerator RandomMove()
    {
        //range ���� �ȿ��� ������
        float randomX = Random.Range(zombieSp.spX, zombieSp.spX + 2 * range) - range;
        float randomY = Random.Range(zombieSp.spZ, zombieSp.spZ + 2 * range) - range;
        Vector3 randomPos = new Vector3(randomX, zombieSp.spY, randomY);
        transform.LookAt(randomPos);
        isRandom = true;
        yield return new WaitForSeconds(Random.Range(0.5f, 3f));
        isRandom = false;
    }

    void Attack()
    {
        if (!targetController.isSafe)
        {
            zombieAnim.SetBool("isAttack", isAttacking); // animtion
            targetStatus.ReduceHp(power);

            if (!targetStatus.isInfect)
            {
                if (Random.Range(1, 101) <= infection)
                {
                    targetStatus.isInfect = true;
                    zombieAnim.SetTrigger("doInfect");
                    Debug.Log("�����Ǿ����ϴ�");
                }
            }
        }
    }

    public void Die()
    {
        zombieAnim.SetTrigger("doDie");
        GetComponent<ParticleSystem>().Play();
        StartCoroutine("DieEffect");
    }

    IEnumerator DieEffect()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    //�ǰ�
    public void Hit()
    {
        curHp -= targetStatus.CurAttack;
        Debug.Log("[Zombie System] Hit : " + curHp);
        if (curHp <= 0)
        {
            Die();
            Debug.Log("[Zombie System] Die");
        }
    }
}
