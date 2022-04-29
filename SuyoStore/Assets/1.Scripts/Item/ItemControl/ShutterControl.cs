using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShutterControl : MonoBehaviour
{
    public PlayerStatus playerStatus;
    private DataManager _dataManager;
    private bool isNearPlayer = false;
    private bool nearSwitch = true;

    // void Start()
    // {
    //     playerStatus = FindObjectOfType<PlayerStatus>();
    //     _dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
    // }
    // void Update()
    // {
    //     if (isNearPlayer && nearSwitch)
    //     {
    //         StartCoroutine(WaitToDisappear("셔터가 내려와 있어 나갈 수 없다. 셔터를 움직일 방법을 찾아보자"));
    //         nearSwitch = false;
    //     }
    // }
    // IEnumerator WaitToDisappear(string text)
    // {
    //     GameManager.GM.msg.text = text;
    //     GameManager.GM.mustItemCanvas.SetActive(true);
    //     yield return new WaitForSeconds(4);
    //     GameManager.GM.mustItemCanvas.SetActive(false);
    // }
    // private void OnTriggerStay(Collider other)
    // {
    //     if (other.tag == "Player")
    //     {
    //         isNearPlayer = true;
    //     }
    // }

    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.tag == "Player")
    //     {
    //         isNearPlayer = false;
    //         nearSwitch = true;
    //     }
    // }
}
