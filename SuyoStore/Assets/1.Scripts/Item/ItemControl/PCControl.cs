using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PCControl : MonoBehaviour
{
    public PlayerStatus playerStatus;
    private DataManager _dataManager;
    private bool isNearPlayer = false;
    private bool printSwitch = true;
    private bool nearSwitch = true;

    // void Start()
    // {
    //     playerStatus = FindObjectOfType<PlayerStatus>();
    //     _dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
    // }
    // void Update()
    // {
    //     if (isNearPlayer)
    //     {
    //         if (Input.GetKeyUp(KeyCode.G))
    //         {
    //             printSwitch = false;
    //             if (_dataManager.IsContainItem(_dataManager.GetItemID("카드키")))
    //             {
    //                 if (_dataManager.dateControl.GetDays() < 7)
    //                     StartCoroutine(WaitToDisappear("아직은 구조대가 도착하지 않아 지금은 위험할 것 같다."));
    //                 else
    //                 {
    //                     // StartCoroutine(WaitToDisappear("셔터가 올라가는 소리가 백화점에 울린다"));
    //                     GameManager.GM.SetEndEventTrigger();
    //                 }
    //             }
    //             else
    //                 StartCoroutine(WaitToDisappear("조작하려면 카드키가 필요한 것 같다. 카드키를 찾아보자"));
    //         }
    //         else if (printSwitch && nearSwitch)
    //         {
    //             StartCoroutine(WaitToDisappear("전원이 들어와있다. 1층의 셔터를 조작할 수 있을 것 같다."));
    //             nearSwitch = false;
    //         }
    //     }
    // }
    // IEnumerator WaitToDisappear(string text)
    // {
    //     GameManager.GM.msg.text = text;
    //     GameManager.GM.mustItemCanvas.SetActive(true);
    //     yield return new WaitForSeconds(4);
    //     GameManager.GM.mustItemCanvas.SetActive(false);
    //     printSwitch = true;
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
