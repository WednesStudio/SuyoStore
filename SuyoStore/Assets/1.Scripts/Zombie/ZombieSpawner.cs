using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public float range;
    public int n1;
    //public int n2;
    //public int n3;
    //public int n4;
    public int g;
    public int y;
    public int r;
    public int allCount;
    public bool allDestroy;
    
    void Start()
    {
        First();
    }

    void Spawn(int[] count)
    {
        for (int i=0; i < count.Length; i++)
        {
            for (int j=0; j < count[i]; j++)
            {
                float randomX = Random.Range(0, range) - range/2;
                float randomY = Random.Range(0, range) - range/2;
                Vector3 randomPos = new Vector3(randomX, 0.5f, randomY);
                Instantiate(prefabs[i], randomPos, Quaternion.identity);
                allCount++;
            }
        }
    }

    void First()
    {
        int[] zCount = {n1, g, y, r};
        //int[] zCount = {n1, n2, n3, n4, g, y, r}; 
        Spawn(zCount);
    }

    void Last()
    {
        if (allCount < 1)
        {

        }

    }
}
