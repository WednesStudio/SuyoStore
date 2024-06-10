using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ZombieAI : MonoBehaviour
{
    private GameObject target;
    private PlayerStatus targetStatus;
    private PlayerController targetController;
    public Image healthbar;
    private float timer;

    public int hp;
    private int curHp;
    public int detection; //감지 범위
    public int curSpeed;
    public int speed; //속도
    public int power; //공격력
    public int coolTime; //쿨타임
    public int infection = 5; //감염률
    public Vector3 spawn; //스폰 위치
    public bool isDetect;// 플레이어 추격 여부
    public bool isRandom;
    float range;
    bool isAttacking = false; // 플레이어와 닿아서 플레이어를 공격 중인지
    public Animator zombieAnim;
    public GameObject child;
    ZombieSpawner zombieSp;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        targetStatus = target.GetComponent<PlayerStatus>();
        targetController = target.GetComponent<PlayerController>();
    }
    void Start()
    {
        hp = 100;
        detection = 6;
        speed = 2;

        timer = 0;
        isDetect = false;
        isRandom = false;
        spawn = transform.position;
        curHp = hp;
        curSpeed = speed;
        zombieSp = GameObject.Find("ZombieSpawner").GetComponent<ZombieSpawner>();
        range = zombieSp.spawnRange;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        Move();
        Debug.Log(child.name);
    }

    //Player Tag를 가진 객체에 닿았을 떄
    void OnTriggerStay(Collider other)
    {
        //if (other.tag == "Player" && !targetController.isSafe)
        //{
        //    zombieAnim.SetBool("isAttack", true);
        //    curSpeed = 0;
        //    if (timer <= 0)
        //    {
        //        Attack();
        //    }
        //    timer = coolTime;
        //}
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            curSpeed = speed;
            zombieAnim.SetBool("isAttack", false);
        }
    }

    void Move()
    {
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
        Vector3 randomPos = new Vector3(randomX, zombieSp.spY, randomZ);
        transform.LookAt(randomPos);
        isRandom = true;
        yield return new WaitForSeconds(Random.Range(0.5f, 3f));
        isRandom = false;
    }
}