using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour
{
    GameObject player;
    public GameObject[] UpArriveGatesArray;
    public GameObject[] DownArriveGatesArray; 
    
    public int arriveGateNum;
    public enum GateType { GoUp, GoDown, ArriveUp, ArriveDown };
    [SerializeField] GateType gateType;
    enum TargetFloor { B2, B1, F1, F2, F3, Looftop };
    [SerializeField] TargetFloor targetFloor;
    private int floorNumber = 3;

    //public GameObject ArrivePoint;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        UpArriveGatesArray = GameObject.FindGameObjectsWithTag("UpGate");
        DownArriveGatesArray = GameObject.FindGameObjectsWithTag("DownGate"); 
        GateNumSetting();
    }
    void GateNumSetting()
    {
        switch (targetFloor)
        {
            case TargetFloor.F2:
                arriveGateNum = 3;
                break;
            case TargetFloor.F1:
                arriveGateNum = 2;
                break;
            case TargetFloor.B1:
                arriveGateNum = 1;
                break;
            case TargetFloor.B2:
                arriveGateNum = 0;
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (gateType == GateType.GoUp)
            {
                for (int i = 0; i < UpArriveGatesArray.Length; i++)
                {
                    if (UpArriveGatesArray[i].GetComponent<PlayerSpawner>().arriveGateNum == arriveGateNum)
                    {
                        player.transform.position = UpArriveGatesArray[i].transform.position;
                        floorNumber += 1;
                        GameManager.GM.SetCurrentScene(floorNumber);
                    }
                }
            }
            else if (gateType == GateType.GoDown)
            {
                for (int i = 0; i < DownArriveGatesArray.Length; i++)
                {
                    if (DownArriveGatesArray[i].GetComponent<PlayerSpawner>().arriveGateNum == arriveGateNum)
                    {
                        player.transform.position = DownArriveGatesArray[i].transform.position;
                        floorNumber -= 1;
                        GameManager.GM.SetCurrentScene(floorNumber);
                    }
                }
            }
            else { }
        }
    }
}
