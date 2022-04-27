using System.Net.Mime;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public enum GameState
{
    GameStart,
    GameOver,
    GameWin
}

public class GameManager : MonoBehaviour
{
    [SerializeField] DataManager _dataManager;
    [SerializeField] UIManager _uiManager;
    [SerializeField] ItemUse _itemUse;
    [SerializeField] ScenarioEvent _scenarioEvent;
    [NonSerialized]
    public GameObject mustItemCanvas;
    [NonSerialized]
    public TextMeshProUGUI msg;
    private Image backgroundPanel;
    public static GameManager GM = null;
    private int _currentSceneNum;
    [NonSerialized]
    public GameState state;
    private string _sceneName;
    public static event Action<GameState> OnGameStateChanged;
    private bool EndEventTrigger = false;
    private bool coroutineSwitch = true;
    public void SetEndEventTrigger()
    {
        EndEventTrigger = true;
    }
    private void Awake()
    {
        if (GM != null)
            Destroy(gameObject);
        else
        {
            GM = this;
            _dataManager.SetData();
            SetWholeUI();
            _dataManager.LoadJsonData();
            // SetPopUp();
        }
    }
    private void Start()
    {
        GameStart();
        UpdateGameState(GameState.GameStart);
    }

    private void Update()
    {
        if (_dataManager.dateControl.GetDays() >= 7 && EndEventTrigger)
            // if (_dataManager.dateControl.GetDays() >= 7)
            CheckCondition();
    }

    public void GameStart()
    {
        //Initial Game Setting
        //UI Scene에서 생성된 오브젝트들(UI, Player, Managers)을 갖고 첫 스폰 씬에 생성
        ChangeToOtherScene(4);  //빌드 번호가 바로 4인 지상 3층으로 스폰
    }
    private void SetPopUp()
    {
        mustItemCanvas = GameObject.Find("==POPUP==/[MustItemPopUp]/MustItemCanvas");
        msg = GameObject.Find("==POPUP==/[MustItemPopUp]/MustItemCanvas/Background_Panel/Text_Panel/MessageText").GetComponent<TextMeshProUGUI>();
        backgroundPanel = GameObject.Find("==POPUP==/[MustItemPopUp]/MustItemCanvas/Background_Panel").GetComponent<Image>();
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.GameStart:
                //GameStart();
                break;
            case GameState.GameOver:
                GameOver();
                break;
            case GameState.GameWin:
                GameWin();
                break;
            default:
                UnityEngine.Debug.Log("Error changing state");
                break;
        }
        OnGameStateChanged?.Invoke(newState);
    }
    IEnumerator WaitForEnding(string text, GameState gameState)
    {
        coroutineSwitch = false;
        gameObject.GetComponent<SceneEffect>().FadeEffect(-1);
        yield return new WaitForSeconds(2);
        Color tempColor = backgroundPanel.color;
        tempColor.a = 255;
        backgroundPanel.color = tempColor;
        msg.text = text;
        mustItemCanvas.SetActive(state == gameState);
        yield return new WaitForSeconds(4);
        msg.text = "축하합니다.\n당신은 살아남았습니다.";
    }

    public void GameOver()
    {
        if (coroutineSwitch)
            StartCoroutine(WaitForEnding("당신은 죽었습니다", GameState.GameOver));
    }
    public void GameWin()
    {
        if (coroutineSwitch)
            StartCoroutine(WaitForEnding(_dataManager.GetConditionMsg(), GameState.GameWin));
    }
    IEnumerator WaitToChangeDate()
    {
        yield return new WaitForSeconds(2);
        System.DateTime result = System.DateTime.Parse(_dataManager.dateControl.GetDate());
        result = result.AddDays(1);
        _dataManager.dateControl.SetDate(result.ToString("yyyy/MM/dd"));
    }
    public void DateSetting()
    {
        gameObject.GetComponent<SceneEffect>().FadeEffect(-1);
        StartCoroutine(WaitToChangeDate());
    }
    public int GetCurrentDay()
    {
        int day = _dataManager.dateControl.GetDays();
        return day;
    }
    private void SetWholeUI()
    {
        _uiManager.SetTopBarUI(10f, 50f, 50f, 10, 10);
        _uiManager.SetCurrentStateUI("2022/04/06", "F3 휴게공간");
        _uiManager.SetInitialInventory();
    }

    public void AddItem(int itemID, int count = 1)
    {
        _dataManager.AddItem(itemID, count);
    }

    public void CheckUseItem(int ID)
    {
        if (_dataManager.GetItemCategory(ID) == "소모") _uiManager.CheckUseItem(_dataManager.GetItemName(ID));
        else UseItem(ID);
    }

    public void UseItem(int itemID)
    {
        Item temp = _dataManager.SetNewItem(itemID);
        string category = _dataManager.GetItemSubCategory(itemID);
        print(category);
        if (category != "가방" && category != "스마트폰" && category != "침낭" && category != "보조배터리" && category != "카드키") _dataManager.AddItem(itemID, -1);
        _itemUse.UseItem(itemID);
    }

    public string GetItemSubCategory(int id)
    {
        return _dataManager.GetItemSubCategory(id);
    }

    public int GetItemID(string name)
    {
        return _dataManager.GetItemID(name);
    }
    public int GetItemCount(int itemID)
    {
        return _dataManager.GetItemCount(itemID);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void ChangeToOtherScene(int sceneNum)
    {
        //Keep data
        gameObject.GetComponent<SceneEffect>().SceneChange(sceneNum);
        GameObject[] cameras = GameObject.FindGameObjectsWithTag("MainCamera");
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject c in cameras)
        {
            if (!c.activeSelf) Destroy(c);
        }

        foreach (GameObject p in players)
        {
            if (!p.activeSelf) Destroy(p);
        }
    }

    public void SetCurrentScene(string sceneName)
    {
        _sceneName = sceneName;
        _dataManager.SetCurrentInfo("", _sceneName);
        if(_sceneName == "02.F1")
        {
            if(_scenarioEvent.isShelterClear) SetEndEventTrigger();
        }
    }

    public string GetSceneName()
    {
        return _sceneName;
    }

    private bool CheckMustItem()
    {
        Dictionary<int, int> itemList = _dataManager.GetMyItems();
        string must = _dataManager.GetConditionMust();
        List<int> itemId = _dataManager.GetItemIDMyList(must);
        int total = 0;

        // UnityEngine.Debug.Log("must-have: " + must + " ,  found: " + itemId.Count);
        foreach (int i in itemId)
        {
            if (_dataManager.IsContainItem(i))
                total += itemList[i];
        }
        if (total >= _dataManager.GetConditionCount())
            return true;
        return false;
    }
    public void CheckCondition()
    {
        string location = _dataManager.GetLocation();
        string exit = _dataManager.GetConditionExit();
        // if (exit == location && CheckMustItem())
        if (CheckMustItem())
            UpdateGameState(GameState.GameWin);
        else UpdateGameState(GameState.GameOver);
    }
}
