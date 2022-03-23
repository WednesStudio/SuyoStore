using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionSettingUI : MonoBehaviour
{
    [SerializeField] GameObject _statusWindow, _inventoryWindow, _manipulationWindow, _optionWindow;
    [SerializeField] GameObject[] _statusPanels;
    [SerializeField] GameObject[] _inventoryPanels;
    [SerializeField] GameObject[] _manipulationPanels;
    [SerializeField] GameObject[] _optionPanels;
    [SerializeField] GameObject _exitGameWindow;
    private GameObject[] _panels;

    //Screen Panel
    [SerializeField] GameObject _resolutionScrollview;

    //Service Panel
    [SerializeField] GameObject _contactPanel, _creditPanel;

    private void Awake() 
    {
        // _musicSource.volume = _musicVolSlider.value;
        // _environmentSource.volume = _environmentVolSlider.value;
        // _sfxSource.volume = _sfxVolSlider.value;
    }

    //On window
    
    public void OnStatusWindow()
    {
        _statusWindow.SetActive(true);
    }
    public void OnInventoryWindow()
    {
        _inventoryWindow.SetActive(true);
    }
    public void OnManipulationWindow()
    {
        _manipulationWindow.SetActive(true);
    }
    public void OnOptionWindow()
    {
        _optionWindow.SetActive(true);
    }
    
    //Off window
    public void OffStatusWindow()
    {
        _statusWindow.SetActive(false);
    }
    public void OffInventoryWindow()
    {
        _inventoryWindow.SetActive(false);
    }
    public void OffManipulationWindow()
    {
        _manipulationWindow.SetActive(false);
    }
    public void OffOptionWindow()
    {
        _optionWindow.SetActive(false);
        OnScreenPanel();
    }

    //Exit game
    public void ExitGame()
    {
        _exitGameWindow.SetActive(true);
    }

    public void GoExitGame()
    {
        _exitGameWindow.SetActive(false);
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    } 

    public void CancleExitGame()
    {
        _exitGameWindow.SetActive(false);
    }

    /// <summary>
    /// Open exact panels for the window
    /// </summary>
    /// <param = "windowIndex"> The current window (Status / Inventory / Manipulation / Option) </param>
    /// <param = "windowIndex"> The number of the panels of the window </param>
    /// <param = "windowIndex"> The index of the panel of the window </param>
    public void OnPanels(int windowIndex, int panelIndex, int index)
    {
        if(windowIndex == 0) _panels = _statusPanels;
        else if (windowIndex == 1) _panels = _inventoryPanels;
        else if (windowIndex == 2) _panels = _manipulationPanels;
        else if (windowIndex == 3) _panels = _optionPanels;

        for(int i = 0; i < panelIndex; i++)
        {
            if(i == index)  _panels[i].SetActive(true);
            else _panels[i].SetActive(false);
        }
    }

    //Status Window
    
    //Inventory Window
    
    //Manipulation Window
    
    //Option Window
    #region 
    //Screen panel
    public void OnScreenPanel()
    {
        OnPanels(3, 3, 0);
    }


    public void OnResolutionSetting()
    {
        _resolutionScrollview.SetActive(true);
    }

    public void OffResolutionSetting()
    {
        _resolutionScrollview.SetActive(false);
    }

    public void WindowModeSetting()
    {

    }

    public void MouseSetting()
    {
        Vector2 sensitivity = new Vector2(0.5f, 0.5f);
        Vector2 mouseMovement = new Vector2(Input.GetAxisRaw("Mouse X") * sensitivity.x,
                                            Input.GetAxisRaw("Mouse Y") * sensitivity.y);
        print(mouseMovement);
    }

    //Sound Panel
    public void OnSoundPanel()
    {
        OnPanels(3, 3, 1);
    }

    //Service Panel
    public void OnServicePanel()
    {
        OnPanels(3, 3, 2);
    }

    public void OnContactUs()
    {
        _contactPanel.SetActive(true);
    }

    public void OffContact()
    {
        _contactPanel.SetActive(false);
    }

    public void OnCredit()
    {
        _creditPanel.SetActive(true);
    }

    public void OffCredit()
    {
        _creditPanel.SetActive(false);
    }

    #endregion
}
