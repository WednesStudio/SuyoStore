using System.Collections;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    GameObject zombie;
    // Player object & script related Player
    private GameObject target;
    private PlayerStatus targetStatus;
    private PlayerController targetController;

    private float timer;

    public int hp;
    private int curHp;
    public int detection; //감지 범위
    Vector3 moveVec;
    public float curSpeed;
    public float speed;
    public int power;
    public int coolTime;
    public int infection = 5; // 감염률
    public Vector3 spawn; // 스폰 위치
    public bool isDetect;
    //public bool isChase; // 플레이어 추격 여부
    public bool isRandom;
    public float range;
    public Animator zombieAnim;
    public GameObject child;
    ZombieSpawner zombieSp;
    Rigidbody zomRigid;

    bool isBorder;

    //Animation Controller Transition
    bool isAttacking = false; // 플레이어와 닿아서 플레이어를 공격 중인지
    bool isWalk = true;

    private void Awake()
    {
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

    // Update is called once per frame
    void Update()
    {
        zombieAnim.SetBool("isWalk", curSpeed > 0);

        timer -= Time.deltaTime;
        Move();
    }

    //Player Tag를 가진 객체에 닿았을 때 공격
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            curSpeed = 0;
            if (timer <= 0)
            {
                Attack();
                isAttacking =  true;
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

    void Move()
    {
        //target의 위치와 zombie의 객체 거리가 detection보다 작거나, 플레이어를 공격 중일 때 추격
        if ((!target.GetComponent<PlayerController>().isSafe)
            && ((Vector3.Distance(target.transform.position, transform.position) < detection)
            || isAttacking))
        {
            isDetect = true;
            transform.LookAt(target.gameObject.transform);
        }
        //스폰 된 지역과 가까워지면 탐색을 계속할지 판단
        else if (Vector3.Distance(spawn, transform.position) < 0.3)
        {
            isDetect = false;
        }
        //랜덤 이동
        else if (!isDetect)
        {
            if (!isRandom)
                StartCoroutine("RandomMove");
        }
        //스폰 된 지역으로 이동
        else
        {
            transform.LookAt(spawn);
        }
        transform.Translate(Vector3.forward * Time.deltaTime * curSpeed);
    }

    IEnumerator RandomMove()
    {
        //range 범위 안에서 움직임
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
                    Debug.Log("감염되었습니다");
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

    //피격
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