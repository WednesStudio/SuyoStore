using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public GameObject player;
    public GameObject[] GatesArray;
    public static SceneController instance = null;
    public int currentGateNum;

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
        if(GatesArray.Length == 0)
        {
            GatesArray = GameObject.FindGameObjectsWithTag("SceneGate");
        }
    }


    private void OnLevelWasLoaded()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GatesArray = GameObject.FindGameObjectsWithTag("SceneGate");

        for (int i = 0; i < GatesArray.Length; i++)
        {
            if (GatesArray[i].GetComponent<PlayerSpawn>().playerIntoGateNum == currentGateNum)
            {
                player.transform.position = GatesArray[i].transform.position;
            }
        }
    }

    //public void LoadScene(int passedGateNum)
    //{
    //    currentGateNum = passedGateNum;
    //    GameManager.GM.ChangeToOtherScene(passedGateNum);
    //}


}