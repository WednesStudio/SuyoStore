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

    //�ν����Ϳ��� �⺻ �� ����
    public string zombieName; //�����
    public int hp = Random.Range(50, 101); //ü��
    public int attack = Random.Range(5, 11); //����
    public int speed = Random.Range(5, 13); //�ӵ�
    public int infection; //������
    
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

    //������ �ֱ�(�ִϸ��̼�)
    //���� �ð��� ������ ������
    void IdleState() {

    }

    //������ �������� �̵�
    //���� Ÿ���� ���������͸� ź�ٸ� ó�� ������ ��ġ�� �̵�
    //GameManager.GM.GetSceneName()���� �÷��̾ �̵��ߴ��� üũ
    void MoveState() {
        
    }

    //Ÿ�� ����
    void ChaseState() {
        
    }

    //Ÿ�� ����
    //���ݿ� �����ϸ� infection Ȯ���� �÷��̾� ����
    //Ÿ���� <PlayerStatus>()���� idInfect�� true��
    void AttackState() {

    }

    //����
    //���� �� ����Ʈ ����
    void DieState() {

    }

    //damage��ŭ ü�� ����
    //�÷��̾�� ȣ���ϰų� ���⼭ ������ ���⿡ ������ ȣ��
    public void GetDamage(int damage) {

    }

    //����(�±� "Melee")�� ������ GetDamage ȣ��
}
