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
    private GameObject[] _panels;
    
    //Sound Panel 
    [SerializeField] AudioSource[] _soundSources;
    [SerializeField] AudioSource _musicSource, _environmentSource, _sfxSource;
    [SerializeField] Sprite _soundOnImage, _soundOffImage;
    [SerializeField] Sprite _sfxOnImage, _sfxOffImage;
    [SerializeField] Button _masterButton, _musicButton, _environmentButton, _sfxButton;
    private bool _isMasterOn = true, _isMusicOn = true, _isEnvironmentOn = true, _isSfxOn = true;
    [SerializeField] Slider _masterVolSlider, _musicVolSlider, _environmentVolSlider, _sfxVolSlider;

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
    public void OnScreenPanel()
    {
        OnPanels(3, 3, 0);
    }
    public void OnSoundPanel()
    {
        OnPanels(3, 3, 1);
    }
    public void OnServicePanel()
    {
        OnPanels(3, 3, 2);
    }

    //Screen panel
    public void ResolutionSetting()
    {

    }

    public void WindowModeSetting()
    {

    }

    public void MouseSetting()
    {

    }

    //Sound Panel
    public void SetMasterVolume(float volume)
    {
        _musicSource.enabled = true;
        _environmentSource.enabled = true;
        _sfxSource.enabled = true;

        _musicSource.volume = volume;
        _environmentSource.volume = volume;
        _sfxSource.volume = volume;

        _masterButton.GetComponent<Image>().sprite = _soundOffImage;
    }
    public void SetMusicVolume(float volume)
    {
        _musicSource.enabled = true;
        _musicSource.volume = volume;
        _musicButton.GetComponent<Image>().sprite = _soundOffImage;
    }
    public void SetEnvironmentVolume(float volume)
    {
        _environmentSource.enabled = true;
        _environmentSource.volume = volume;
        _environmentButton.GetComponent<Image>().sprite = _sfxOffImage;
    }
    public void SetSFxVolume(float volume)
    {
        _sfxSource.enabled = true;
        _sfxSource.volume = volume;
        _sfxButton.GetComponent<Image>().sprite = _sfxOffImage;
    }

    public void MasterOnOff()
    {
        if(_isMasterOn)
        {
            _musicSource.enabled = false;
            _environmentSource.enabled = false;
            _sfxSource.enabled = false;
            _masterButton.GetComponent<Image>().sprite = _soundOnImage;
            _isMasterOn = false;
        }
        else
        {
            _musicSource.enabled = true;
            _environmentSource.enabled = true;
            _sfxSource.enabled = true;
            _musicButton.GetComponent<Image>().sprite = _soundOffImage;
            _isMasterOn = true;
        }
    }

    public void MusicOnOff()
    {
        if(_isMusicOn)
        {
            _musicSource.enabled = false;
            _musicButton.GetComponent<Image>().sprite = _soundOnImage;
            _isMusicOn = false;
        }
        else
        {
            _musicSource.enabled = true;
            _musicButton.GetComponent<Image>().sprite = _soundOffImage;
            _isMusicOn = true;
        }
    }

    public void EnvironmentOnOff()
    {
        if(_isEnvironmentOn)
        {
            _environmentSource.enabled = false;
            _environmentButton.GetComponent<Image>().sprite = _sfxOnImage;
            _isEnvironmentOn = false;
        }
        else
        {
            _environmentSource.enabled = true;
            _environmentButton.GetComponent<Image>().sprite = _sfxOffImage;
            _isEnvironmentOn = true;
        }
    }

    public void SFxOnOff()
    {
        if(_isSfxOn)
        {
            _sfxSource.enabled = false;
            _sfxButton.GetComponent<Image>().sprite = _sfxOnImage;
            _isSfxOn = false;
        }
        else
        {
            _sfxSource.enabled = true;
            _sfxButton.GetComponent<Image>().sprite = _sfxOffImage;
            _isSfxOn = true;
        }        
    }

    //Service Panel
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
