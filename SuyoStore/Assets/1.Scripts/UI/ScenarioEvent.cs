using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScenarioEvent : MonoBehaviour
{
    [SerializeField] GameObject _scenarioWindow;
    [SerializeField] TextMeshProUGUI _scenarioText;
    private DataManager _dataManager;
    private GameObject _scenarioAsset;
    bool isBasementEntered = false;
    public bool isShelterClear = false;

    private void Start() 
    {
        _dataManager = FindObjectOfType<DataManager>();
    }

    public void GetScenarioItemName(string name)
    {
        SetScenario(name);
    }

    public void GetScenarioItem(GameObject scenarioAsset)
    {
        this._scenarioAsset = scenarioAsset;
        SetScenario(_scenarioAsset.name);
    }

    private void SetScenario(string assetName)
    {
        //1층 출입구 셔터 앞
        if(assetName == "Shutter")
        {
            _scenarioText.text = "셔터가 내려와 있어 나갈 수 없다. 셔터를 움직일 방법을 찾아보자.";
        }
        //쉘터 컴퓨터 주변
        else if(assetName == "Shelter")
        {
            _scenarioText.text = "전원이 들어와있다. 1층의 셔터를 조작할 수 있을 것 같다.";
        }
        //쉘터 컴퓨터 클릭 시
        else if(assetName == "Computer")
        {
            int cardKeyID = GameManager.GM.GetItemID("카드키");
            //카드키 없음
            if(GameManager.GM.GetItemCount(cardKeyID) == 0)
            {
                _scenarioText.text = "조작하려면 카드키가 필요한 것 같다. 카드키를 찾아보자.";
            }
            else
            {
                //7일차 이전
                if(GameManager.GM.GetCurrentDay() < 7)
                {
                    _scenarioText.text = "아직은 구조대가 도착하지 않아 지금은 위험할 것 같다.";
                }
                //7일차
                else
                {
                    _scenarioText.text = "셔터가 올라가는 소리가 백화점에 울린다.";
                    isShelterClear = true;
                }
            }
        }
        else if(assetName == "Escalator")
        {
            int battery1ID = GameManager.GM.GetItemID("배터리1");
            int battery2ID = GameManager.GM.GetItemID("배터리2");
            int battery1Count = GameManager.GM.GetItemCount(battery1ID);
            int battery2Count = GameManager.GM.GetItemCount(battery2ID);

            //옥상으로 가는 에스컬레이터 앞(배터리 0개)
            if((battery1Count + battery2Count) == 0)
            {
                _scenarioText.text = "옥상문의 보안장치가 꺼져있다. 보안장치를 키려면 배터리가 필요한 것 같다.";
            }
            //1<배터리<10
            else if((battery1Count + battery2Count) > 1 && (battery1Count + battery2Count) < 10)
            {
                _scenarioText.text = "배터리의 양이 부족한 것 같다. (n/10)";
            }
            //배터리 10개
            else if((battery1Count + battery2Count) >= 10)
            {
                //7일차 이전
                if(GameManager.GM.GetCurrentDay() < 7)
                {
                    _scenarioText.text = "아직은 구조대가 도착하지 않아 지금은 위험할 것 같다.";
                }
                //7일차
                else
                {
                    _scenarioText.text = "밖에 헬기소리가 백화점 안까지 울린다.";
                    GameManager.GM.SetEndEventTrigger();
                }
            }
        }
        else if(assetName == "Basement2")
        {
            int bag1ID = GameManager.GM.GetItemID("침낭1");
            int bag2ID = GameManager.GM.GetItemID("침낭2");
            int bag1Count = GameManager.GM.GetItemCount(bag1ID);
            int bag2Count = GameManager.GM.GetItemCount(bag2ID);

            //지하 2층 처음 도착했을 때
            if(!isBasementEntered)
            {
                //침낭 없음
                if((bag1Count + bag2Count) == 0)
                {
                    _scenarioText.text = "지하 2층이지만 생각보다 깊은 것 같다. 백화점이 무너져도 이곳은 안전할까?";
                }
                //침낭 보유
                else
                {
                    _scenarioText.text = "침낭이 있다면 이곳에서 자도 좀비로부터 안전할까?";
                }
                isBasementEntered = true;
            }
        }
        else if(assetName == "Sleeping Bag")
        {
            //7일차 이전
            if(GameManager.GM.GetCurrentDay() < 7)
            {
                _scenarioText.text = "아직은 사용할 때가 아닌 것 같다.";
            }
            //7일차
            else
            {
                _scenarioText.text = "굉음을 내며 백화점이 무너진다. 살 수 있을까.";
                StartCoroutine(Waitfor3Seconds());
                //sound
                GameManager.GM.SetEndEventTrigger();
            }
        }
        _scenarioWindow.SetActive(true);
    }

    public void OffScenarioWindow()
    {
        _scenarioWindow.SetActive(false);
    }
    IEnumerator Waitfor3Seconds()
    {
        yield return new WaitForSeconds(3.0f);
    }
}
