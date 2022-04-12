using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieAI : MonoBehaviour
{
    private GameObject target;
    public Image healthbar;
    private float timer;
    public int hp; //체력
    private int curHp; //현재 체력
    public int detection; //감지 범위
    public int speed; //속도
    public int power; //공격력
    public int coolTime; //쿨타임
    public int infection; //감염률
    public Vector3 spawn; //스폰 위치
    public bool isDetect;
    public bool isRandom;
    public float range;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        timer = 0;
        isDetect = false;
        isRandom = false;
        spawn = transform.position;
        curHp = hp;
        range = GameObject.Find("ZombieSpawner").GetComponent<ZombieSpawner>().range;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        Move();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && timer < 0)
        {
            Attack();
            timer = coolTime;
        }
    }

    void Move()
    {
        if (Vector3.Distance(target.transform.position, transform.position) < detection)
        {
            isDetect = true;
            transform.LookAt(target.gameObject.transform);
        }
        else if (Vector3.Distance(spawn, transform.position) < 0.3)
        {
            isDetect = false;
        }
        else if (!isDetect)
        {
            if (!isRandom)
                StartCoroutine("RandomMove");
        }
        else
        {
            transform.LookAt(spawn);
        }
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    IEnumerator RandomMove()
    {
        float randomX = Random.Range(0, 2 * range) - range;
        float randomY = Random.Range(0, 2 * range) - range;
        Vector3 randomPos = new Vector3 (randomX, 0.5f, randomY);
        transform.LookAt(randomPos);
        isRandom = true;
        yield return new WaitForSeconds(Random.Range(0.5f, 1f));
        isRandom = false;
    }

    void Attack()
    {
        target.GetComponent<PlayerController_>().hp -= power;
        if(Random.Range(1, 101) <= infection)
        {
            Debug.Log("감염되었습니다");
        }
        Debug.Log(target.GetComponent<PlayerController_>().hp);
    }

    void Die()
    {
        Destroy (gameObject);
    }
    
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
