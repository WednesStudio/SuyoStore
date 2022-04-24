using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // ÇöÀç ÀÖ´Â ¾À
    Scene scene;
    float gateTimer = 3.0f;
    float timer = 0.0f;
    enum eGateType { GoUp, GoDown, NotUse };
    [SerializeField]
    eGateType gateType;
    bool isInGate = false;
    int changeSceneNum;

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    private void Update()
    {
        if (isInGate)
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
            if (timer >= gateTimer)
            {
                if (gateType == eGateType.GoUp)
                {
                    UpstairsByGate();
                }
                else if (gateType == eGateType.GoDown)
                {
                    DownstairsByGate();
                }
                else
                {
                    timer = 0.0f;
                }

                timer = 0.0f;
            }
        }
        else
        {
            timer = 0.0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isInGate = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isInGate = false;
        }
    }

    public void DownstairsByGate()
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
        GameManager.GM.ChangeToOtherScene(changeSceneNum);
    }
    public void UpstairsByGate()
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
        GameManager.GM.ChangeToOtherScene(changeSceneNum);
    }
}
