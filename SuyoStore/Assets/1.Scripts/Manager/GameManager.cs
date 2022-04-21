using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] DataManager _dataManager;
    [SerializeField] UIManager _uiManager;
    [SerializeField] ItemUse _itemUse;
    public static GameManager GM;
    private int _currentSceneNum;
    private DateControl _dateControl;
    private void Awake() 
    {
        GM = this;
        
        _dataManager.SetData();
        _dateControl = FindObjectOfType<DateControl>();
        SetWholeUI();
    }

    private void Update() 
    {
        //Detect picking up some item
        //Get Item -> Add Item()  

        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.transform.gameObject.tag == "Item")
                {
                    hit.transform.gameObject.GetComponent<ItemControl>().GetThisItem();
                }
            } 
        }
    }

    public void GameStart()
    {
        //Initial Game Setting
        //_currentSceneNum = 1;
        //date = ..
    }
    
    public void DateSetting()
    {
        System.DateTime result = System.DateTime.Parse(_dateControl.GetDate());
        result = result.AddDays(1);
        _dateControl.SetDate(result.ToString("yyyy/MM/dd"));
        gameObject.GetComponent<SceneEffect>().FadeEffect(-1);
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
        _uiManager.CheckUseItem(_dataManager.GetItemName(ID));
    }

    public void UseItem(int itemID)
    {
        Item temp = _dataManager.SetNewItem(itemID);
        string category = _dataManager.GetItemSubCategory(itemID);
        
        if(category != "가방") _dataManager.AddItem(itemID, -1);
        _itemUse.UseItem(itemID);
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
}
