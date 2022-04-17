using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDestroy : MonoBehaviour
{
    PlayerStatus status;

    public GameObject cube;

    private void Awake()
    {
        status = GetComponent<PlayerStatus>();
    }
    void Update()
    {
        if(status.isGet == true)
        {
            cube.SetActive(false);
        }
    }
}
