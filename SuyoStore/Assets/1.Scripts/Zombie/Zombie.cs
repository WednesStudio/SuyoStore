using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public Vector3 spawn; //스폰 위치
    public bool isDetect;// 플레이어 추격 여부
    public bool isRandom;
    float range;
    ZombieSpawner zombieSp;

    // Related on Target(= Player)
    [SerializeField] GameObject target;
    PlayerStatus targetStatus;
    PlayerController targetController;

    // Relaated on Zombie
    Rigidbody zomRigid;
    BoxCollider zomMeleeArea;
    Animator zombieAnim;
    public int detection; //감지 범위
    public int hp;
    public int curHp;
    public float curSpeed;
    public float speed;
    public int power;
    public float CoolTime;
    public int infection = 5; // 감염률
    public bool isZomAttack; // 좀비가 플레이어를 공격 중인지
    
    bool isground;
    float disToGround = 1f;
    bool isFindPlayer;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        targetStatus = target.GetComponent<PlayerStatus>();
        targetController = target.GetComponent<PlayerController>();

        zomRigid = GetComponent<Rigidbody>();
        zomMeleeArea = GetComponentInChildren<BoxCollider>();
        zombieAnim = GetComponent<Animator>();
        zombieSp = GameObject.Find("ZombieSpawner").GetComponent<ZombieSpawner>();

    }
    void Start()
    {
        detection = 6;
        isDetect = false;
        isRandom = false;
        spawn = transform.position;
        range = zombieSp.spawnRange;

        hp = 100;
        speed = 1.0f;
        power = 1;
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
        Move();
        if (curHp<=0) Die();
    }

    void Move()
    {
        zombieAnim.SetBool("isWalk", true);
        // 애니메이션 작동
        //target의 위치와 zombie의 객체 거리가 detection보다 작거나, 플레이어를 공격 중일 때 추격
        if ((!target.GetComponent<PlayerController>().isSafe)
            && ((Vector3.Distance(target.transform.position, transform.position) < detection)
            || (curHp < hp)))
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
        float randomZ = Random.Range(zombieSp.spZ, zombieSp.spZ + 2 * range) - range;
        Vector3 randomPos = new Vector3(randomX, transform.position.y, randomZ);
        transform.LookAt(randomPos);
        isRandom = true;
        yield return new WaitForSeconds(Random.Range(0.5f, 3f));
        isRandom = false;
    }


    bool IsGrounded()
    {
        RaycastHit rayhit;
        var ray = new Ray(transform.position, -transform.up);
        isground = Physics.Raycast(ray, out rayhit, disToGround, LayerMask.GetMask("Floor"));
        Debug.DrawRay(transform.position, -transform.up * disToGround, Color.red);
        return isground;
    }

    private void Respawn()
    {
        int spawnRange = 40;
        float randomX = Random.Range(0, 0 + spawnRange) - spawnRange / 2;
        float randomZ = Random.Range(0, 0 + spawnRange) - spawnRange / 2;
        Vector3 respawnPos = new Vector3(randomX, gameObject.transform.position.y, randomZ);
        gameObject.transform.position = respawnPos;
    }

    bool RayObejct()
    {
        float targetFindRange = 3f;
        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position,
                                targetFindRange,
                                transform.up,
                                0.0f,
                                LayerMask.GetMask("Player"));

        // 플레이어가 감지 범위 안에 있는지
        if (rayHits.Length > 0) return true;
        else return false;
    }
    private void Chase()
    {
        zombieAnim.SetBool("isWalk", true);
        transform.LookAt(target.gameObject.transform);

        transform.Translate(Vector3.forward * Time.deltaTime * curSpeed);
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
        if (!targetStatus.isInfect)
        {
            if (Random.Range(1, 101) <= infection)
            {
                targetStatus.isInfect = true;
                zombieAnim.SetTrigger("doInfect");
                Debug.Log("감염되었습니다");
            }
        }
        zombieAnim.SetBool("isAttack", isZomAttack);

        // anim 타이밍에 맞춰서 zomMelee 활성화
        yield return new WaitForSeconds(0.2f);
        zomMeleeArea.enabled = true;
        
        // anim 타이밍에 맞춰서 zomMelee 비활성화
        yield return new WaitForSeconds(0.02f);
        zomMeleeArea.enabled = false;
        yield return new WaitForSeconds(CoolTime); // 다음 공격까지의 쿨타임
        Debug.Log("좀비 쿨타임 종료");
        isZomAttack = false;
        zombieAnim.SetBool("isAttack", isZomAttack);
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
