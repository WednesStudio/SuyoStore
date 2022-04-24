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
    
    void Start()
    {
        B2Spawn();
    }

    void Spawn(int x, int z, int[] count)
    {
        for (int i=0; i < count.Length; i++)
        {
            for (int j=0; j < count[i]; j++)
            {
                float randomX = Random.Range(x, x + range) - range/2;
                float randomY = Random.Range(z, z + range) - range/2;
                Vector3 randomPos = new Vector3(randomX, 0.5f, randomY);
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
        range = 10;
        int[] zCount = {5, 5, 5, 5, 5, 5, 5}; 
        Spawn(44, 33, zCount);
        range = 55;
        int[] zCount2 = {5, 5, 5, 5, 5, 5, 5}; 
        Spawn(22, 27, zCount2);
    } 
    
    void B1Spawn()
    {
        range = 50;
        int[] zCount = {5, 5, 5, 5, 5, 5, 5}; 
        Spawn(25, 27, zCount);
    } 

    void F1Spawn()
    {
        range = 48;
        int[] zCount = {5, 5, 5, 5, 5, 5, 5}; 
        Spawn(24, 22, zCount);
    } 

    void F2Spawn()
    {
        range = 48;
        int[] zCount = {5, 5, 5, 5, 5, 5, 5}; 
        Spawn(24, 22, zCount);
    }
}
