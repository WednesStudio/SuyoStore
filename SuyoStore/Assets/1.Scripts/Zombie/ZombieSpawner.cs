using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public float range;
    public int n1;
    public int n2;
    public int n3;
    public int n4;
    public int g;
    public int y;
    public int r;
    public int allCount;
    public bool allDestroy;
    GameObject[] zombies;
    public int spX;
    public float spY;
    public int spZ;

    void Start()
    {
        spX = 0;
        spY = 0;
        spZ = 0;
        B2Spawn();
    }

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
        range = 60;
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

    void AllZombieDelete()
    {
        zombies = GameObject.FindGameObjectsWithTag("Zombie");
        for(int i = 0; i < zombies.Length; i++)
        {
            Destroy(zombies[i]);
        }
    }
}
