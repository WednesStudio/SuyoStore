using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject[] zombiePrefabs; //좀비 프래팹
    public float spawnRange; //소환 할 범위
    int n1; //노말 좀비 1 개수
    int n2; //노말 좀비 2 개수
    int n3; //노말 좀비 3 개수
    int n4; //노말 좀비 4 개수
    int g; //그린 좀비 개수
    int y; //옐로 좀비 개수
    int r; //레드 좀비 개수
    int allCount;
    bool allDestroy; 
    GameObject[] zombies; //소환 된 모든 좀비를 담음
    public float spX; //맵 중앙 X좌표
    public float spY; //맵 중앙 Y좌표
    public float spZ; //맵 중앙 Z좌표
    float yDiffer = 6.5f;

    [SerializeField] int zombieNum;
    int floors = 5; // 건물 총 층수
    int[] zCount = new int[4];

    void Start()
    {
        zombieNum = 1;
        //오류날 수도 있어서 초기화
        spX = 0; 
        spY = 0;
        spZ = 0;
        SpawnAllFloor();
    }

    //(x, y, z)를 중심으로 범위 range에 각 좀비를 몇마리씩 소환시킬건지 담은 count를 사용해서 소환
    void Spawn(float x, float y, float z, int[] count)
    {
        spawnRange = 30;
        spX = x;
        spY = y;
        spZ = z;
        for (int i = 0; i < count.Length; i++) 
        {
            for (int j = 0; j < count[i]; j++)
            {
                float randomX = Random.Range(x, x + spawnRange) - spawnRange / 2;
                float randomZ = Random.Range(z, z + spawnRange) - spawnRange / 2;
                Vector3 randomPos = new Vector3(randomX, y, randomZ);
                Instantiate(zombiePrefabs[i], randomPos, Quaternion.identity);
                allCount++;
            }
        }
    }

    void First()
    {
        //Spawn(zCount);
    }

    void SpawnAllFloor()
    {
        // 좀비 종류에 따라 각각 생성할 개수
        for(int floor = 0; floor < floors; floor++)
        {
            for (int i = 0; i < zombiePrefabs.Length; i++)
            {
                zCount[i] = zombieNum;
            }
            Spawn(0, 0 + (yDiffer * floor), 0, zCount);
        }
    }

    //void Spawnfloor()
    //{
    //    spawnRange = 60;
    //    int[] zCount = { 5, 5, 5, 5, 5, 5, 5 };
    //    Spawn(0, 0, 0, zCount);
    //}


    //좀비 전체 삭제
    void AllZombieDelete()
    {
        //태그가 Zombie인 객체를 찾음
        zombies = GameObject.FindGameObjectsWithTag("Zombie");
        for(int i = 0; i < zombies.Length; i++)
        {
            Destroy(zombies[i]);
        }
    }
}
