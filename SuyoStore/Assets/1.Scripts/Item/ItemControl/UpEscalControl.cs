using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpEscalControl : MonoBehaviour
{
    public PlayerStatus playerStatus;
    private DataManager _dataManager;
    private bool isNearPlayer = false;
    private bool nearSwitch = true;
    private List<int> itemID;
    void Start()
    {
        playerStatus = FindObjectOfType<PlayerStatus>();
        _dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
        itemID = _dataManager.GetItemIDMyList("SM_Item_Battery");
    }
    void Update()
    {
        if (isNearPlayer)
        {
            int total = CountTotal();
            if (nearSwitch)
            {
                if (total == 0)
                {
                    StartCoroutine(WaitToDisappear("옥상문의 보안장치가 꺼져있다. 보안장치를 키려면 배터리가 필요한 것 같다."));
                }
                else if (total < 10)
                {
                    string message = "배터리의 양이 부족한 것 같다. (" + total + "/10)";
                    StartCoroutine(WaitToDisappear(message));
                }
                else if (_dataManager.dateControl.GetDays() < 7)
                {
                    StartCoroutine(WaitToDisappear("아직은 구조대가 도착하지 않아 지금은 위험할 것 같다."));
                }
                nearSwitch = false;
            }
        }
    }
    private int CountTotal()
    {
        Dictionary<int, int> itemList = _dataManager.GetMyItems();
        int total = 0;

        foreach (int i in itemID)
        {
            if (_dataManager.IsContainItem(i))
                total += itemList[i];
        }
        return total;
    }
    IEnumerator WaitToDisappear(string text)
    {
        GameManager.GM.msg.text = text;
        GameManager.GM.mustItemCanvas.SetActive(true);
        yield return new WaitForSeconds(4);
        GameManager.GM.mustItemCanvas.SetActive(false);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            isNearPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isNearPlayer = false;
            nearSwitch = true;
        }
    }
}
