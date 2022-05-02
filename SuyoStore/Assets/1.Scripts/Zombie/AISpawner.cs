using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawner : MonoBehaviour
{
    public Transform Player;
    public int NumToSpawn;
    public float SpawnDelay = 0f;
    public List<ZombieController> EnemyPrefabs = new List<ZombieController>();

    private Dictionary<int, ObjectPool> ZEnemyObjectPools = new Dictionary<int, ObjectPool>();

    private void Awake()
    {
        for(int i = 0; i< EnemyPrefabs.Count; i++)
        {
            //ZEnemyObjectPools.Add(i, ZEnemyObjectPools.)
        }
    }

}
