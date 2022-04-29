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
    // [NonSerialized]
    // public GameObject mustItemCanvas;
    // [NonSerialized]
    // public TextMeshProUGUI msg;
    [SerializeField] Image GameOverPanel;
    public static GameManager GM = null;
    private Tutorial _tutorial;
    private int _currentSceneNum;
    [NonSerialized]
    public GameState state;
    public bool isSceneLoadDone = false;
    private string _sceneName;
    public static event Action<GameState> OnGameStateChanged;
    private bool EndEventTrigger = false;
    private bool coroutineSwitch = true;
    public bool isGameStart = false;
    private int _tutorialItemCount = 0;
    public bool IsTutorialItemDone = false;

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
            _tutorial = _uiManager.GetComponent<Tutorial>();
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
        if(isGameStart)
        {
            if (_dataManager.dateControl.GetDays() == 7 && EndEventTrigger)
            {
                CheckCondition();
            }         
        }
        
    }

    public void GameStart()
    {
        //Initial Game Setting
        //UI Scene에서 생성된 오브젝트들(UI, Player, Managers)을 갖고 첫 스폰 씬에 생성
        UpdateGameState(GameState.GameStart);
        isGameStart = true;
        if(!SoundManager.SM.isPlayingBGMSound()) SoundManager.SM.PlayBGMSound(BGMSoundName.MainMusic);
        else
        {
            SoundManager.SM.StopBGMSound();
            SoundManager.SM.PlayBGMSound(BGMSoundName.MainMusic);
        }
    }
    // private void SetPopUp()
    // {
    //     mustItemCanvas = GameObject.Find("==POPUP==/[MustItemPopUp]/MustItemCanvas");
    //     msg = GameObject.Find("==POPUP==/[MustItemPopUp]/MustItemCanvas/Background_Panel/Text_Panel/MessageText").GetComponent<TextMeshProUGUI>();
    //     backgroundPanel = GameObject.Find("==POPUP==/[MustItemPopUp]/MustItemCanvas/Background_Panel").GetComponent<Image>();
    // }

    public void UpdateMonologue(string msg)
    {
        _uiManager.SetMonologuePanel(msg);
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
        Color tempColor = GameOverPanel.color;
        tempColor.a = 255;
        GameOverPanel.color = tempColor;
        if(state == gameState)
        {
            UpdateMonologue(text);
            GameOverPanel.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
            yield return new WaitForSeconds(4);
            if(gameState == GameState.GameWin)  UpdateMonologue("축하합니다.\n당신은 살아남았습니다.");
        }
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
        _uiManager.SetCurrentStateUI("2022/04/06", "3층 : 휴게 공간");
        _uiManager.SetInitialInventory();
    }

    public void AddItem(int itemID, int count = 1)
    {
        if(!IsTutorialItemDone && _tutorialItemCount < 4)
        {
            if(GetItemSubCategory(itemID) == "치료제" || GetItemSubCategory(itemID) == "스마트폰" || GetItemSubCategory(itemID) == "음식")
            {
                _tutorialItemCount += 1;
                if(_tutorialItemCount == 3)
                {
                    _tutorial.GetExactTutorial();
                }
            }
        }
        
        _dataManager.AddItem(itemID, count);
    }

    public void CheckUseItem(int ID)
    {
        if (_dataManager.GetItemCategory(ID) == "소모" && GetItemSubCategory(ID) != "보조배터리") _uiManager.CheckUseItem(_dataManager.GetItemName(ID));
        else UseItem(ID);
    }

    public void UseItem(int itemID)
    {
        string category = _dataManager.GetItemSubCategory(itemID);
        if(!IsTutorialItemDone && _tutorialItemCount < 6)
        {
            if(category == "치료제" || category == "스마트폰" || category == "음식")
            {
                _tutorialItemCount += 1;
                _tutorial.GetExactTutorial();
            }
        }
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

    public void SetCurrentScene(int num)
    {
        _sceneName = num.ToString();
        _dataManager.SetCurrentInfo("", _sceneName);
        if(_sceneName == "1")
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
        {
            UpdateGameState(GameState.GameWin);
        }
        else
        {
            UpdateGameState(GameState.GameOver);
        } 
    }
}
