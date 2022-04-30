using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour
{
    GameObject player;
    public GameObject[] UpArriveGatesArray;
    public GameObject[] DownArriveGatesArray;
    Vector3 moveFloorV;
    public int arriveGateNum;
    public enum GateType { GoUp, GoDown, ArriveUp, ArriveDown };
    [SerializeField] GateType gateType;
    enum TargetFloor { B2, B1, F1, F2, F3, Looftop };
    [SerializeField] TargetFloor targetFloor;
    bool isChange = false;

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
            case TargetFloor.F3:
                arriveGateNum = 3;
                break;
            case TargetFloor.F2:
                arriveGateNum = 2;
                break;
            case TargetFloor.F1:
                arriveGateNum = 1;
                break;
            case TargetFloor.B1:
                arriveGateNum = -1;
                break;
            case TargetFloor.B2:
                arriveGateNum = -2;
                break;
            default:
                break;
        }
    }

    void ChangeFloor()
    {
        if (isChange)
        {
            if (gateType == GateType.GoUp)
            {
                for (int i = 0; i < UpArriveGatesArray.Length; i++)
                {
                    if (UpArriveGatesArray[i].GetComponent<PlayerSpawner>().arriveGateNum == arriveGateNum)
                    {
                        Debug.Log("도착: " + UpArriveGatesArray[i].name);
                        player.transform.position = UpArriveGatesArray[i].transform.position; // 이동할 좌표
                        GameManager.GM.SetCurrentScene(arriveGateNum); // UI 층 변환
                    }
                }
            }
            else if (gateType == GateType.GoDown)
            {
                for (int i = 0; i < DownArriveGatesArray.Length; i++)
                {
                    if (DownArriveGatesArray[i].GetComponent<PlayerSpawner>().arriveGateNum == arriveGateNum)
                    {
                        Debug.Log("도착: " + UpArriveGatesArray[i].name);
                        player.transform.position = DownArriveGatesArray[i].transform.position; // 이동할 좌표
                        GameManager.GM.SetCurrentScene(arriveGateNum);// UI 층 변환
                    }
                }
            }
            else { }

            isChange = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isChange = true;
            GameManager.GM.ChangeToOtherScene(-1);
            Invoke("ChangeFloor", 1f);
        }
    }
}
