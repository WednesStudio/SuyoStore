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

    //???????????? ???? ?? ????
    public string zombieName; //??????
    public int hp = Random.Range(50, 101); //????
    public int attack = Random.Range(5, 11); //????
    public int speed = Random.Range(5, 13); //????
    public int infection; //??????
    
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

    //?????? ????(??????????)
    //???? ?????? ?????? ??????
    //??? ???
    void IdleState() {
        
    }

    //?????? ???????? ????
    //???? ?????? ???????????? ?????? ???? ?????? ?????? ????
    //GameManager.GM.GetSceneName()???? ?????????? ?????????? ????
    void MoveState() {
        
    }

    //???? ????
    void ChaseState() {
        
    }

    //???? ????
    //?????? ???????? infection ?????? ???????? ????
    //?????? <PlayerStatus>()???? idInfect?? true??
    void AttackState() {

    }

    //????
    //???? ?? ?????? ????
    void DieState() {

    }

    //damage???? ???? ????
    //???????????? ?????????? ?????? ?????? ?????? ?????? ????
    public void GetDamage(int damage) {

    }

    //????(???? "Melee")?? ?????? GetDamage ????
}
