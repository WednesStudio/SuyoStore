using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // ÇöÀç ÀÖ´Â ¾À
    Scene scene;

    enum eGateType { GoUp, GoDown, NotUse };
    [SerializeField]
    eGateType gateType;

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (gateType == eGateType.GoUp)
            {
                UpstairsByGate();
            }
            else if(gateType == eGateType.GoDown)
            {
                DownstairsByGate();
            }
            else
            {
            }
        }
    }

    public void DownstairsByGate()
    {
        switch (scene.name)
        {
            case "04.F3":
                SceneManager.LoadScene("03.F2");
                break;
            case "03.F2":
                SceneManager.LoadScene("02.F1");
                break;
            case "02.F1":
                SceneManager.LoadScene("01.B1");
                break;
            case "01.B1":
                SceneManager.LoadScene("00.B2");
                break;
            default:
                break;
        }
    }
    public void UpstairsByGate()
    {
        switch (scene.name)
        {
            case "00.B2":
                SceneManager.LoadScene("01.B1");
                break;
            case "01.B1":
                SceneManager.LoadScene("02.F1");
                break;
            case "02.F1":
                SceneManager.LoadScene("03.F2");
                break;
            case "03.F2":
                SceneManager.LoadScene("04.F3");
                break;
            default:
                break;
        }
    }
}
