using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    // 좀비 스폰 위치 정보
    ZombieSpawner zomSpawn;
    float range;

    NavMeshAgent navMA;
    public float startWaitTime = 4;
    public float timeToRotate = 2;
    public float speedWalk = 2;
    public float speedRun = 3;

    public float viewRadius = 15;
    public float viewAngle = 90;
    public LayerMask playerM;
    public LayerMask obstacleM;
    public float meshResolution = 1f;
    public int edgeIterations = 4;
    public float edgeDistance = 0.5f;

    public Transform[] wayPoints;
    int m_CurrentWaypointIndex;

    Vector3 playerLastPosition = Vector3.zero;
    Vector3 m_PlayerPosition;

    float m_WaitTime;
    float m_TimeToRotate;
    bool m_PlayerInRange;
    bool m_PlayerNear;
    bool m_IsPatrol;
    bool m_CaughtPlayer;

    private void Start()
    {
        zomSpawn = GameObject.Find("ZombieSpawner").GetComponent<ZombieSpawner>();
        range = zomSpawn.spawnRange;
        speedWalk = 2;
        speedRun = 3;
        playerM = LayerMask.GetMask("Player");
        m_PlayerPosition = Vector3.zero;
        m_IsPatrol = true;
        m_CaughtPlayer = false;
        m_PlayerInRange = false;
        m_WaitTime = startWaitTime;
        m_TimeToRotate = timeToRotate;

        m_CurrentWaypointIndex = 0;
        navMA = GetComponent<NavMeshAgent>();

        navMA.isStopped = false;
        navMA.speed = speedWalk;
        
        //스폰된 지역 기준으로 스폰장소, 상, 오른, 오른쪽위 4곳(사각형)
        wayPoints[0].position = new Vector3(zomSpawn.spX, zomSpawn.spY, zomSpawn.spZ);
        wayPoints[1].position = new Vector3(zomSpawn.spX+range, zomSpawn.spY, zomSpawn.spZ);
        wayPoints[2].position = new Vector3(zomSpawn.spX, zomSpawn.spY, zomSpawn.spZ + range);
        wayPoints[3].position = new Vector3(zomSpawn.spX + range, zomSpawn.spY, zomSpawn.spZ + range);
        
        navMA.SetDestination(wayPoints[m_CurrentWaypointIndex].position);
    }

    private void Update()
    {
        EnvironmentView();
        if (!m_IsPatrol)
        {
            Chasing();
        }
        else
        {
            Patrolling();
        }
    }

    private void Chasing()
    {
        m_PlayerNear = false;
        playerLastPosition = Vector3.zero;
        if (!m_CaughtPlayer)
        {
            Move(speedRun);
            navMA.SetDestination(m_PlayerPosition);
        }
        if(navMA.remainingDistance<= navMA.stoppingDistance)
        {
            if(m_WaitTime<=0 && !m_CaughtPlayer && Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 6f)
            {
                m_IsPatrol = true;
                m_PlayerNear = false;
                Move(speedWalk);
                m_TimeToRotate = timeToRotate;
                m_WaitTime = startWaitTime;
                navMA.SetDestination(wayPoints[m_CurrentWaypointIndex].position);
            }
            else
            {
                if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 2.5f)
                {
                    Stop();
                    m_WaitTime -= Time.deltaTime;
                }
            }
        }
    }

    private void Patrolling()
    {
        if (m_PlayerNear)
        {
            if(m_TimeToRotate <= 0)
            {
                Move(speedWalk);
                LookingPlayer(playerLastPosition);
            }
            else
            {
                Stop();
                m_TimeToRotate -= Time.deltaTime;
            }
        }
        else
        {
            m_PlayerNear = false;
            playerLastPosition = Vector3.zero;
            navMA.SetDestination(wayPoints[m_CurrentWaypointIndex].position);
            if (navMA.remainingDistance <= navMA.stoppingDistance)
            {
                if (m_WaitTime <= 0)
                {
                    NextPoint();
                    Move(speedWalk);
                    m_WaitTime = startWaitTime;
                }
                else
                {
                    Stop();
                    m_WaitTime -= Time.deltaTime;
                }
            }
        }
    }

    void Move(float speed)
    {
        navMA.isStopped = false;
        navMA.speed = speed;
    }
    void Stop()
    {
        navMA.isStopped = true;
        navMA.speed = 0;
    }
    public void NextPoint()
    {
        m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % wayPoints.Length;
        navMA.SetDestination(wayPoints[m_CurrentWaypointIndex].position);
    }
    void CaughtPlayer()
    {
        m_CaughtPlayer = true;
    }

    void LookingPlayer(Vector3 player)
    {
        navMA.SetDestination(player);
        if(Vector3.Distance(transform.position, player) <= 0.3)
        {
            if (m_WaitTime <= 0)
            {
                m_PlayerNear = false;
                Move(speedWalk);
                navMA.SetDestination(wayPoints[m_CurrentWaypointIndex].position);
                m_WaitTime = startWaitTime;
                m_TimeToRotate = timeToRotate;
            }
            else
            {
                Stop();
                m_WaitTime -= Time.deltaTime;
            }
        }
    }
    
    void EnvironmentView()
    {
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerM);

        for(int i = 0; i<playerInRange.Length; i++)
        {
            Transform player = playerInRange[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
            {
                float dstToPlayer = Vector3.Distance(transform.position, player.position);
                if(!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleM))
                {
                    m_PlayerInRange = true;
                    m_IsPatrol = false;
                }
                else
                {
                    m_PlayerInRange = false;
                }
            }
            if(Vector3.Distance(transform.position, player.position) > viewRadius)
            {
                m_PlayerInRange = false;
            }
            if (m_PlayerInRange)
            {
                m_PlayerPosition = player.transform.position;
            }
        }

    }

}
