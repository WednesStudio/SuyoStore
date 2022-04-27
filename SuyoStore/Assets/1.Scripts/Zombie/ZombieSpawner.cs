using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject[] prefabs; //좀비 프래팹 7개
    public float range; //소환 할 범위
    public int n1; //노말 좀비 1 개수
    public int n2; //노말 좀비 2 개수
    public int n3; //노말 좀비 3 개수
    public int n4; //노말 좀비 4 개수
    public int g; //그린 좀비 개수
    public int y; //옐로 좀비 개수
    public int r; //레드 좀비 개수
    public int allCount;
    public bool allDestroy; 
    GameObject[] zombies; //소환 된 모든 좀비를 담음
    public int spX; //맵 중앙 X좌표
    public float spY; //맵 중앙 Y좌표
    public int spZ; //맵 중앙 Z좌표

    void Start()
    {
        spX = 0; //오류날 수도 있어서 초기화
        spY = 0;
        spZ = 0;
        B2Spawn();
    }

    //(x, y, z)를 중심으로 범위 range에 각 좀비를 몇마리씩 소환시킬건지 담은 count를 사용해서 소황
    void Spawn(int x, float y, int z, int[] count)
    {
        spX = x;
        spY = y;
        spZ = z;
        for (int i=0; i < count.Length; i++) 
        {
            for (int j=0; j < count[i]; j++)
            {
                float randomX = Random.Range(x, x + range) - range/2;
                float randomY = Random.Range(z, z + range) - range/2;
                Vector3 randomPos = new Vector3(randomX, y, randomY);
                Instantiate(prefabs[i], randomPos, Quaternion.identity);
                allCount++;
            }
        }
    }

    void First()
    {
        int[] zCount = {n1, n2, n3, n4, g, y, r}; 
        //Spawn(zCount);
    }

    void B2Spawn()
    {
        //범위가 60
        range = 60;
        //n1 5마리, n2 5마리... r 5마리를 소환할 것
        int[] zCount = {5, 5, 5, 5, 5, 5, 5}; 
        Spawn(8, 1.19f, 20, zCount);
    } 
    
    void B1Spawn()
    {
        range = 60;
        int[] zCount = {5, 5, 5, 5, 5, 5, 5}; 
        Spawn(-8, 1.13f, 20, zCount);
    } 

    void F1Spawn()
    {
        range = 120;
        int[] zCount = {5, 5, 5, 5, 5, 5, 5}; 
        Spawn(-34, 1.13f, -5, zCount);
    } 

    void F2Spawn()
    {
        range = 80;
        int[] zCount = {5, 5, 5, 5, 5, 5, 5}; 
        Spawn(8, 1.13f, 22, zCount);
    }

    void F3Spawn()
    {
        range = 80;
        int[] zCount = {5, 5, 5, 5, 5, 5, 5}; 
        Spawn(5, 1.13f, 25, zCount);
    }

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
