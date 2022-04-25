using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawn : MonoBehaviour
{
    Scene scene; // ���� �ִ� ��
    int changeSceneNum; // ��ȯ�� �� ��ȣ
    public int playerIntoGateNum;
    float gateTimer = 2.0f; // �� ��ȯ������ �ð�
    float timer = 0.0f;

    // ����Ʈ�� ����
    public enum GateType { GoUp, GoDown, NotUseUp, NotUseDown };
    public GateType gateType;
    //bool isInGate = false; // �÷��̾ ����Ʈ�� �����ߴ��� �Ǵ�

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            // �۵��ϴ� ����Ʈ���
            if (gateType == GateType.GoUp || gateType == GateType.GoDown)
            {
                // Ÿ�̸� �۵�
                timer += Time.deltaTime;

                // ���� �ð� �� �� ��ȯ
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
                        // ����ó��
                        timer = 0.0f;
                    }
                }
            }
            else
            {
                // �۵����� �ʴ� ����Ʈ��� Ÿ�̸� ����
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