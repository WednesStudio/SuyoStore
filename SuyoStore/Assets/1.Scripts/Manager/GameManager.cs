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
    private CellPhoneMsgs cellphone;
    public static GameManager GM = null;
    private int _currentSceneNum;
    public GameState state;
    public static event Action<GameState> OnGameStateChanged;
    private string directory = "Data/";
    private string[] routes = { "route1", "route2", "route3" };
    private int selectedRoute;
    private DateControl _dateControl;
    private void Awake()
    {
        if (GM != null)
            Destroy(gameObject);
        else
        {
            GM = this;
            DontDestroyOnLoad(gameObject);
            _dataManager.SetData();
            _dateControl = FindObjectOfType<DateControl>();
            SetWholeUI();
            selectedRoute = UnityEngine.Random.Range(0, 3);
            GameObject.Find("Reader").GetComponent<LoadJson>().LoadMsgData(directory + routes[selectedRoute]);
            cellphone = FindObjectOfType<CellPhoneMsgs>();
        }
    }
    private void Start()
    {
        UpdateGameState(GameState.GameStart);
        GameObject.Find("Reader").GetComponent<LoadJson>().LoadConditionData(directory + routes[selectedRoute]);
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
        if (_dataManager.dateControl.GetDays() == 7)
            CheckCondition();
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

    public void GameStart()
    {
        //Initial Game Setting
        //_currentSceneNum = 1;
        //date = ..
    }
    public void GameOver()
    {
        cellphone.infoText.text = "당신은 죽었습니다";
        cellphone.canvas.SetActive(state == GameState.GameOver);
        // ExitGame();
    }
    public void GameWin()
    {
        cellphone.infoText.text = _dataManager.GetConditionRoute();
        cellphone.canvas.SetActive(state == GameState.GameWin);
        // ExitGame();
    }
    IEnumerator WaitToChangeDate()
    {
        yield return new WaitForSeconds(2);
        System.DateTime result = System.DateTime.Parse(_dateControl.GetDate());
        result = result.AddDays(1);
        _dateControl.SetDate(result.ToString("yyyy/MM/dd"));
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
            if (_dataManager.IsContainItem(i) && itemList[i] == _dataManager.GetConditionCount())
                return true;
        }
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

    public int GetSelectedRoute() => selectedRoute;
}
