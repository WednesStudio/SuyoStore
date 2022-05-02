using System.Collections;
using UnityEngine;

public class WallOnOff : MonoBehaviour
{
    GameObject player;
    PlayerSpawner playerSpawn;
    public GameObject[] Walls;

    enum WallFloorPos { B2, B1, F1, F2, none };
    [SerializeField] WallFloorPos wallPos;
    private void Start()
    {
        Walls = GameObject.FindGameObjectsWithTag("Wall");
        player = GameObject.FindGameObjectWithTag("Player");
        playerSpawn = player.GetComponent<PlayerSpawner>();
    }

    void OffWall()
    {
        switch (playerSpawn.arriveGateNum)
        {
            case 3:
                if(wallPos == WallFloorPos.F1)
                {
                    gameObject.SetActive(true);
                }
                break;
            case 2:
                break;
            case 1:
                break;
            case -1:
                break;
            case -2:
                break;
        }
    }
}
