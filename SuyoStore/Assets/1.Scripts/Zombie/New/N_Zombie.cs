using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class N_Zombie : MonoBehaviour
{
    enum State 
    {
        Idle,
        Move,
        Chase,
        Attack,
        Die
    }
    State state;

    //인스펙터에서 기본 값 설정
    public string zombieName; //좀비명
    public int hp = Random.Range(50, 101); //체력
    public int attack = Random.Range(5, 11); //공격
    public int speed = Random.Range(5, 13); //속도
    public int infection; //감염률
    
    NavMeshAgent navMesh;
    GameObject target;
    

    void Awake() 
    {
        navMesh = GetComponent<NavMeshAgent>();  
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        state = State.Idle;
    }

    void Update()
    {
        switch (state) 
        {
            case State.Idle:
                break;
            case State.Move:
                break;
            case State.Chase:
                break;
            case State.Attack:
                break;
            case State.Die:
                break;
        }
    }

    //가만히 있기(애니메이션)
    //일정 시간이 지나면 움직임
    void IdleState() {

    }

    //랜덤한 방향으로 이동
    //만약 타겟이 엘리베이터를 탄다면 처음 생성된 위치로 이동
    //GameManager.GM.GetSceneName()으로 플레이어가 이동했는지 체크
    void MoveState() {
        
    }

    //타겟 추적
    void ChaseState() {
        
    }

    //타겟 공격
    //공격에 성공하면 infection 확률로 플레이어 감염
    //타겟의 <PlayerStatus>()에서 idInfect를 true로
    void AttackState() {

    }

    //죽음
    //죽을 때 이펙트 생성
    void DieState() {

    }

    //damage만큼 체력 깍임
    //플레이어에서 호출하거나 여기서 닿으면 무기에 닿으면 호출
    public void GetDamage(int damage) {

    }

    //무기(태그 "Melee")에 닿으면 GetDamage 호출
}
