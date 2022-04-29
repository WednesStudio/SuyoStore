using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public GameObject player;
    public GameObject[] UpArriveGatesArray;
    public GameObject[] DownArriveGatesArray;
    public static SceneController instance = null;
    public int GateNum;
    public int GateTypeN;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }

        if (player == null) player = GameObject.FindGameObjectWithTag("Player");
        if(UpArriveGatesArray.Length == 0)
        {
            UpArriveGatesArray = GameObject.FindGameObjectsWithTag("UpGate");
        }
        if (DownArriveGatesArray.Length == 0)
        {
            DownArriveGatesArray = GameObject.FindGameObjectsWithTag("DownGate");
        }
    }

    //private void Start()
    //{
    //    player.transform.position = GatesArray[0].transform.position;
    //}

    private void Update()
    {
        ChangeFloor();
    }

    private void ChangeFloor()
    {
        Debug.Log("이거 자동은 하나");

        player = GameObject.FindGameObjectWithTag("Player");
        UpArriveGatesArray = GameObject.FindGameObjectsWithTag("UpGate");
        DownArriveGatesArray = GameObject.FindGameObjectsWithTag("DownGate");

        if (GateTypeN == 0)
        {
            for (int i = 0; i < UpArriveGatesArray.Length; i++)
            {
                if (UpArriveGatesArray[i].GetComponent<PlayerSpawner>().arriveGateNum == GateNum)
                {
                    player.transform.position = UpArriveGatesArray[i].transform.position;
                }

            }
        }
        else if (GateTypeN == 1)
        {
            for (int i = 0; i < DownArriveGatesArray.Length; i++)
            {
                if (DownArriveGatesArray[i].GetComponent<PlayerSpawner>().arriveGateNum == GateNum)
                {
                    Debug.Log("Go Down!");
                    player.transform.position = DownArriveGatesArray[i].transform.position;
                }
                else
                {
                    Debug.Log("not same!");
                }
            }
        }
        else {
            Debug.Log("왜안돼!");
        }
    }

    //public void LoadScene(int passedGateNum)
    //{
    //    currentGateNum = passedGateNum;
    //    GameManager.GM.ChangeToOtherScene(passedGateNum);
    //}


}