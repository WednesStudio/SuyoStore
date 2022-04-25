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
    [NonSerialized]
    public GameObject mustItemCanvas;
    [NonSerialized]
    public TextMeshProUGUI msg;
    private Image backgroundPanel;
    public static GameManager GM = null;
    private int _currentSceneNum;
    [NonSerialized]
    public GameState state;
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
            DontDestroyOnLoad(gameObject);
            _dataManager.SetData();
            SetWholeUI();
            _dataManager.LoadJsonData();
            SetPopUp();
        }
    }
    private void Start()
    {
        UpdateGameState(GameState.GameStart);
    }

    private void Update()
    {
        //Detect picking up some item
        //Get Item -> Add Item()  


        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "Item")
                {
                    hit.transform.gameObject.GetComponent<ItemControl>().GetThisItem();
                }
            }
        }
        if (_dataManager.dateControl.GetDays() >= 7 && EndEventTrigger)
            CheckCondition();
    }
    private void SetPopUp()
    {
        mustItemCanvas = GameObject.Find("==POPUP==/[MustItemPopUp]/MustItemCanvas");
        msg = GameObject.Find("==POPUP==/[MustItemPopUp]/MustItemCanvas/Background_Panel/Text_Panel/MessageText").GetComponent<TextMeshProUGUI>();
        backgroundPanel = GameObject.Find("==POPUP==/[MustItemPopUp]/MustItemCanvas/Background_Panel").GetComponent<Image>();
        UnityEngine.Debug.Log("pop up set " + mustItemCanvas + ", " + msg + ", " + backgroundPanel);
    }
    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.GameStart:
                GameStart();
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
    public void GameStart()
    {
        //Initial Game Setting
        //UI Scene에서 생성된 오브젝트들(UI, Player, Managers)을 갖고 첫 스폰 씬에 생성
        //ChangeToOtherScene(?);
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
    private void SetWholeUI()
    {
        _uiManager.SetTopBarUI(10f, 50f, 50f, 10, 10);
        _uiManager.SetCurrentStateUI("2022/04/05", "1F");
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
        if (category != "가방" && category != "스마트폰" && category != "침낭" && category != "보조배터리") _dataManager.AddItem(itemID, -1);
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
    }
    private bool CheckMustItem()
    {
        Dictionary<int, int> itemList = _dataManager.GetMyItems();
        string must = _dataManager.GetConditionMust();
        List<int> itemId = _dataManager.GetItemIDMyList(must);

        // UnityEngine.Debug.Log("must-have: " + must + " ,  found: " + itemId.Count);
        foreach (int i in itemId)
        {
            if (_dataManager.IsContainItem(i) && itemList[i] >= _dataManager.GetConditionCount())
                return true;
        }
        return false;
    }
    public void CheckCondition()
    {
        string location = _dataManager.GetLocation();
        UnityEngine.Debug.Log("location " + location);
        string exit = _dataManager.GetConditionExit();
        // if (exit == location && CheckMustItem())
        if (CheckMustItem())
            UpdateGameState(GameState.GameWin);
        else UpdateGameState(GameState.GameOver);
    }
}
