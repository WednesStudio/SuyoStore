using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour
{
    Scene scene;
    int changeSceneNum; 
    public int playerIntoGateNum;
    float gateTimer = 2.0f; 
    float timer = 0.0f;

    public enum GateType { GoUp, GoDown, NotUseUp, NotUseDown };
    public GateType gateType;

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (gateType == GateType.GoUp || gateType == GateType.GoDown)
            {
                timer += Time.deltaTime;

                if (timer >= gateTimer)
                {
                    timer = 0.0f;
                    Debug.Log("before : " + scene.name);

                    if (gateType == GateType.GoUp)
                    {
                        UpstairsByGate(playerIntoGateNum);
                    }
                    else if (gateType == GateType.GoDown)
                    {
                        DownstairsByGate(playerIntoGateNum);
                    }

                    scene = SceneManager.GetActiveScene();
                    Debug.Log("after : " + scene.name);
                    GameManager.GM.SetCurrentScene(scene.name);
                    SceneController.instance.player.GetComponent<PlayerController>().isChangeScene = true;
                    timer = 0.0f;
                }
            }
            else
            {
                // When gateType == notUse Up or Down
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
}
