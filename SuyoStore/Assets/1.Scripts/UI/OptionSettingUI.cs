using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionSettingUI : MonoBehaviour
{
    [SerializeField] GameObject _statusInventoryWindow, _manipulationWindow, _optionWindow;
    [SerializeField] GameObject[] _statusPanels;
    [SerializeField] GameObject[] _inventoryPanels;
    [SerializeField] GameObject[] _manipulationPanels;
    [SerializeField] GameObject[] _optionPanels;
    [SerializeField] GameObject _exitGameWindow;
    private GameObject[] _panels;

    //Screen Panel
    [SerializeField] GameObject _resolutionScrollview;
    [SerializeField] Slider _mouseSensitivitySlider;
    private bool _mouseInitialized = false;

    //Service Panel
    [SerializeField] GameObject _creditPanel;

    private void Awake() 
    {
        // _musicSource.volume = _musicVolSlider.value;
        // _environmentSource.volume = _environmentVolSlider.value;
        // _sfxSource.volume = _sfxVolSlider.value;
    }

    private void Start() 
    {
        if(PlayerPrefs.HasKey("Sensitivity"))
        {
            _mouseSensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity");
        }
        _mouseInitialized = true;
    }

    //On window
    
    public void OnStatusAndInventoryWindow()
    {
        SoundManager.SM.PlaySfxSound(SfxSoundName.ButtonClick);
        _statusInventoryWindow.SetActive(true);
    }
    public void OnManipulationWindow()
    {
        SoundManager.SM.PlaySfxSound(SfxSoundName.ButtonClick);
        _manipulationWindow.SetActive(true);
    }
    public void OnOptionWindow()
    {
        SoundManager.SM.PlaySfxSound(SfxSoundName.ButtonClick);
        _optionWindow.SetActive(true);
    }
    
    //Off window
    public void OffStatusAndInventoryWindow()
    {
        SoundManager.SM.PlaySfxSound(SfxSoundName.ButtonClick);
        gameObject.GetComponent<InventoryUI>().ChangeScrollView(0);
        _statusInventoryWindow.SetActive(false);
    }
    public void OffManipulationWindow()
    {
        SoundManager.SM.PlaySfxSound(SfxSoundName.ButtonClick);
        _manipulationWindow.SetActive(false);
    }
    public void OffOptionWindow()
    {
        SoundManager.SM.PlaySfxSound(SfxSoundName.ButtonClick);
        _optionWindow.SetActive(false);
        OnSoundPanel();
    }

    //Exit game
    public void ExitGame()
    {
        SoundManager.SM.PlaySfxSound(SfxSoundName.ButtonClick);
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
        SoundManager.SM.PlaySfxSound(SfxSoundName.ButtonClick);
        _exitGameWindow.SetActive(false);
    }

    /// <summary> Open exact panels for the window </summary>
    /// <param = "windowIndex"> The current window (Status / Inventory / Manipulation / Option) </param>
    /// <param = "panelIndex"> The number of the panels of the window </param>
    /// <param = "index"> The index of the panel of the window </param>
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
        //OnPanels(3, 3, 0);
    }


    public void OnResolutionSetting()
    {
        SoundManager.SM.PlaySfxSound(SfxSoundName.ButtonClick);
        _resolutionScrollview.SetActive(true);
    }

    public void OffResolutionSetting()
    {
        _resolutionScrollview.SetActive(false);
    }

    // public void WindowModeSetting()
    // {

    // }

    // public void MouseSetting(float val)
    // {
    //     // Vector2 sensitivity = new Vector2(0.5f, 0.5f);
    //     // Vector2 mouseMovement = new Vector2(Input.GetAxisRaw("Mouse X") * sensitivity.x,
    //     //                                     Input.GetAxisRaw("Mouse Y") * sensitivity.y);
    //     // print(mouseMovement);
    //     if(!_mouseInitialized) return;
    //     if(!Application.isPlaying) return;
    //     PlayerPrefs.SetFloat("Sensitivity", val);
    //     Debug.Log(val);
    // }

    //Sound Panel
    public void OnSoundPanel()
    {
        OnPanels(3, 2, 0);
        SoundManager.SM.PlaySfxSound(SfxSoundName.ButtonClick);
    }

    //Service Panel
    public void OnServicePanel()
    {
        OnPanels(3, 2, 1);
        SoundManager.SM.PlaySfxSound(SfxSoundName.ButtonClick);
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
