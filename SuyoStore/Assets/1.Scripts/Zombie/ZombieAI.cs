using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieAI : MonoBehaviour
{
    private GameObject target;
    private PlayerStatus targetStatus;
    public Image healthbar;
    private float timer;
    public int hp; //체력
    private int curHp; //현재 체력
    public int detection; //감지 범위
    public int curSpeed;
    public int speed; //속도
    public int power; //공격력
    public int coolTime; //쿨타임
    public int infection; //감염률
    public Vector3 spawn; //스폰 위치
    public bool isDetect;
    public bool isRandom;
    public float range;
    bool isAttacking = false; // 플레이어와 닿아서 공격 중인지

    Animator zombieAnim;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        targetStatus = target.GetComponent<PlayerStatus>();
        timer = 0;
        isDetect = false;
        isRandom = false;
        spawn = transform.position;
        curHp = hp;
        curSpeed = speed;
        range = GameObject.Find("ZombieSpawner").GetComponent<ZombieSpawner>().range;
        zombieAnim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        Move();
    }


    //Player Tag를 가진 객체에 닿았을 떄
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && timer < 0)
        {
            curSpeed = 0;
            Attack();
            timer = coolTime;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            curSpeed = speed;
        }
    }

    void Move()
    {
        //target의 위치와 zombie의 객체 거리가 detection보다 작거나, 공격 당해서 hp가 깍였을 때 추격
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
        float randomX = Random.Range(0, 2 * range) - range;
        float randomY = Random.Range(0, 2 * range) - range;
        Vector3 randomPos = new Vector3(randomX, 0.5f, randomY);
        transform.LookAt(randomPos);
        isRandom = true;
        yield return new WaitForSeconds(Random.Range(0.5f, 1f));
        isRandom = false;
    }

    void Attack()
    {
        //Player 공격과 감염
        if (Random.Range(1, 101) <= infection)
        {
            Debug.Log("감염되었습니다");
        }

        targetStatus.ReduceHp(power);

        /*
        // 원래 코드
        target.GetComponent<PlayerController>().pStatus.CurHp -= power;
        Debug.Log(target.GetComponent<PlayerController>().pStatus.CurHp);
        */
    }

    public void Die()
    {
        GetComponent<MeshRenderer>().enabled = false;
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
        //지금은 3데미지를 받지만 나중에 무기 공격력 가져오기
        curHp -= 3;
        healthbar.fillAmount = (float)curHp / (float)hp;
        if (curHp <= 0)
        {
            Die();
        }
    }

    //테스트용
    void OnMouseDown()
    {
        curHp -= 3;
        Debug.Log(curHp);
        Debug.Log(hp);
        healthbar.fillAmount = (float)curHp / (float)hp;
        Debug.Log("좀비 체력: " + curHp);
   if (curHp <= 0)
            Die();
    }
}