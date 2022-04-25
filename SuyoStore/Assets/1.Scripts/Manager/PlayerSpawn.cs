using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawn : MonoBehaviour
{
    Scene scene; // 현재 있는 씬
    int changeSceneNum; // 전환할 씬 번호
    public int playerIntoGateNum;
    float gateTimer = 2.0f; // 씬 전환까지의 시간
    float timer = 0.0f;

    // 게이트의 종류
    public enum GateType { GoUp, GoDown, NotUseUp, NotUseDown };
    public GateType gateType;
    //bool isInGate = false; // 플레이어가 게이트에 진입했는지 판단

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            // 작동하는 게이트라면
            if (gateType == GateType.GoUp || gateType == GateType.GoDown)
            {
                // 타이머 작동
                timer += Time.deltaTime;

                // 일정 시간 후 씬 전환
                if (timer >= gateTimer)
                {
                    timer = 0.0f;
                    if (gateType == GateType.GoUp)
                    {
                        UpstairsByGate(playerIntoGateNum);
                    }
                    else if (gateType == GateType.GoDown)
                    {
                        DownstairsByGate(playerIntoGateNum);
                    }
                    else
                    {
                        // 예외처리
                        timer = 0.0f;
                    }
                }
            }
            else
            {
                // 작동하지 않는 게이트라면 타이머 리셋
                timer = 0.0f;
            }
        }
    }

    public void DownstairsByGate(int passedGateNum)
    {
        switch (scene.name)
        {
            case "04.F3":
                changeSceneNum = 3;
                break;
            case "03.F2":
                changeSceneNum = 2;
                break;
            case "02.F1":
                changeSceneNum = 1;
                break;
            case "01.B1":
                changeSceneNum = 0;
                break;
            default:
                break;
        }
        SceneController.instance.currentGateNum = passedGateNum;
        GameManager.GM.ChangeToOtherScene(changeSceneNum);
    }
    public void UpstairsByGate(int passedGateNum)
    {
        switch (scene.name)
        {
            case "00.B2":
                changeSceneNum = 1;
                break;
            case "01.B1":
                changeSceneNum = 2;
                break;
            case "02.F1":
                changeSceneNum = 3;
                break;
            case "03.F2":
                changeSceneNum = 4;
                break;
            default:
                break;
        }
        SceneController.instance.currentGateNum = passedGateNum;
        GameManager.GM.ChangeToOtherScene(changeSceneNum);
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        isInGate = true;
    //    }
    //}


    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        isInGate = false;
    //    }
    //}


}
