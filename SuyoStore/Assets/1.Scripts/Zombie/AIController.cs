using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    // 좀비 스폰 위치 정보
    ZombieSpawner zomSpawn;
    ZombieController zomController;
    float range;
    public Vector3 spawnV; // 초기 스폰 장소

    NavMeshAgent navMA;
    public float startWaitTime = 3;             //  Wait time of every action
    public float timeToRotate = 2;              //  Wait time when AI detect near the player without seeing
    public float speedWalk = 3;                 //  Walking speed, speed in the nav mesh agent
    public float speedRun = 5;

    public float viewRadius = 15;
    public float viewAngle = 90;
    public LayerMask playerM;                   //  To detect the player with the raycast
    public LayerMask obstacleM;
    public float meshResolution = 1f;           //  How many rays will cast per degree
    public int edgeIterations = 4;              //  Number of iterations to get a better performance of the mesh filter when the raycast hit an obstacule
    public float edgeDistance = 0.5f;           //  Max distance to calcule the a minumun and a maximum raycast when hits something

    Vector3 randomPos;                          //  patrol random points
    public Transform[] wayPoints;               //  patrol points
    public Vector3[] patrolPoints = new Vector3[4];               //  patrol points

    int m_CurrentWaypointIndex;                 //  Current waypoint where AI is going to

    Vector3 playerLastPosition = Vector3.zero;  //  Last position of the player when was near AI
    Vector3 m_PlayerPosition;                   //  Last position of the player when the player is seen by AI

    public float m_WaitTime;
    public float m_TimeToRotate;                //  Variable of the wait time to rotate when the player is near that makes the delay
    public bool m_PlayerInRange;                //  If the player is in range of vision, state of chasing
    public bool m_PlayerNear;                   //  If the player is near, state of hearing
    public bool m_IsPatrol;                     //  If AI is patrol, state of patroling
    public bool m_CaughtPlayer;                 //  if AI has caught the player

    [SerializeField] bool isgrounded = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Floor")
        {
            isgrounded = true;
        }
    }

    private void Start()
    {
        spawnV = GetComponent<Transform>().position;
        zomSpawn = GameObject.Find("ZombieSpawner").GetComponent<ZombieSpawner>();
        zomController = GetComponent<ZombieController>();
        range = zomSpawn.spawnRange;
        playerM = LayerMask.GetMask("Player");

        m_PlayerPosition = Vector3.zero;
        m_IsPatrol = true;
        m_CaughtPlayer = false;
        m_PlayerInRange = false;
        m_WaitTime = startWaitTime;
        m_TimeToRotate = timeToRotate;

        m_CurrentWaypointIndex = 0;
        navMA = GetComponent<NavMeshAgent>();

        // 장애물이 있는 장소에 스폰되면 다른 곳으로 워프
        if (!isgrounded)
        {
            float respawnX = Random.Range(zomSpawn.spX, zomSpawn.spX + zomSpawn.spawnRange) - zomSpawn.spawnRange / 2;
            float respawnZ = Random.Range(zomSpawn.spZ, zomSpawn.spZ + zomSpawn.spawnRange) - zomSpawn.spawnRange / 2;
            Vector3 randomPos = new Vector3(respawnX, spawnV.y, respawnZ);
            navMA.Warp(randomPos);
            spawnV = randomPos;
        }

        navMA.isStopped = false;
        navMA.speed = speedWalk;
        //navMA.SetDestination(wayPoints[m_CurrentWaypointIndex].position); //  Set the destination to the first waypoint

        //스폰된 지역 기준으로 스폰장소, 상, 오른, 오른쪽위 4곳(사각형)
        //Debug.Log("spawnV (" + spawnV.x + ", " + spawnV.y + ", " + spawnV.z + ")");

        patrolPoints[0] = spawnV;
        patrolPoints[1] = new Vector3(wayPoints[0].position.x + range, zomSpawn.spY, zomSpawn.spZ);
        patrolPoints[2] = new Vector3(zomSpawn.spX, zomSpawn.spY, wayPoints[0].position.z + range);
        patrolPoints[3] = new Vector3(wayPoints[0].position.x + range, zomSpawn.spY, wayPoints[0].position.z + range);
        navMA.SetDestination(patrolPoints[m_CurrentWaypointIndex]); //  Set the destination to the first waypoint

        // 랜덤하게 돌아다니기
        //float randomX = Random.Range(zomController.spawnV.x, zomController.spawnV.x + 2 * range) - range;
        //float randomZ = Random.Range(zomController.spawnV.z, zomController.spawnV.z + 2 * range) - range;
        //randomPos = new Vector3(randomX, zomController.spawnV.y, randomZ);
        //navMA.SetDestination(randomPos);

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
        if(navMA.remainingDistance <= navMA.stoppingDistance)
        {
            if(m_WaitTime <= 0 && !m_CaughtPlayer && Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 6f)
            {
                //  Check if AI is not near to the player, returns to patrol after the wait time delay
                m_IsPatrol = true;
                m_PlayerNear = false;
                Move(speedWalk);
                m_TimeToRotate = timeToRotate;
                m_WaitTime = startWaitTime;

                //navMA.SetDestination(wayPoints[m_CurrentWaypointIndex].position);
                navMA.SetDestination(patrolPoints[m_CurrentWaypointIndex]);
            }
            else
            {
                if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 2.5f)
                {
                    //  Wait if the current position is not the player position
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
            //  Check if AI detect near the player, so the enemy will move to that position
            if (m_TimeToRotate <= 0)
            {
                Move(speedWalk);
                LookingPlayer(playerLastPosition);
            }
            else
            {
                //  AI wait for a moment and then go to the last player position
                Stop();
                m_TimeToRotate -= Time.deltaTime;
            }
        }
        else
        {
            m_PlayerNear = false;  //  The player is no near when AI is platroling
            playerLastPosition = Vector3.zero;

            //navMA.SetDestination(wayPoints[m_CurrentWaypointIndex].position); //  Set AI destination to the next waypoint
            navMA.SetDestination(patrolPoints[m_CurrentWaypointIndex]);

            if (navMA.remainingDistance <= navMA.stoppingDistance)
            {
                //  If AI arrives to the waypoint position then wait for a moment and go to the next
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

    public void NextPoint()
    {
        //m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % wayPoints.Length;
        //navMA.SetDestination(wayPoints[m_CurrentWaypointIndex].position);

        m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % patrolPoints.Length;
        navMA.SetDestination(patrolPoints[m_CurrentWaypointIndex]);
    }

    void Move(float speed)
    {
        navMA.isStopped = false;
        navMA.speed = speed;
    }
    void Stop()
    {
        navMA.isStopped = true;
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

                //navMA.SetDestination(wayPoints[m_CurrentWaypointIndex].position);
                navMA.SetDestination(patrolPoints[m_CurrentWaypointIndex]);

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
        //  Make an overlap sphere around AI to detect the playermask in the view radius
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerM);

        for (int i = 0; i < playerInRange.Length; i++)
        {
            Transform player = playerInRange[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
            {
                float disToPlayer = Vector3.Distance(transform.position, player.position);
                if(!Physics.Raycast(transform.position, dirToPlayer, disToPlayer, obstacleM))
                {
                    m_PlayerInRange = true; //  AI see player and starts to chasing the player
                    m_IsPatrol = false; //  Change the state to chasing the player
                }
                else
                {
                    //If the player is behind a obstacle the player position will not be registered
                    m_PlayerInRange = false;
                }
            }
            if(Vector3.Distance(transform.position, player.position) > viewRadius)
            {
                //If the player is further than the view radius, then AI will no longer keep the player's current position.
                //or AI is a safe zone, AI will no chase
                m_PlayerInRange = false;
            }
            if (m_PlayerInRange)
            {
                //If AI no longer see the player, then AI will go to the last position that has been registered
                m_PlayerPosition = player.transform.position; //  Save the player's current position if the player is in range of vision
            } 
        }
    }
}
