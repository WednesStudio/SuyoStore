using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    [SerializeField] AudioSource _musicSource, _environmentSource, _sfxSource;
    [SerializeField] Sprite _soundOnImage, _soundOffImage;
    [SerializeField] Sprite _startOnImage, _startOffImage;
    [SerializeField] Button _masterButton, _musicButton, _environmentButton, _sfxButton;
    [SerializeField] Button _startMusicButton;
    [SerializeField] Slider _masterVolSlider, _musicVolSlider, _environmentVolSlider, _sfxVolSlider;
    
    private bool _isMasterOn = true, _isMusicOn = true, _isEnvironmentOn = true, _isSfxOn = true, _isStartMusicOn;
    private float _curMasterVol = 0.8f;

    public void SetMasterVolume(float volume)
    {
        _musicSource.enabled = true;
        _environmentSource.enabled = true;
        _sfxSource.enabled = true;

        _musicSource.volume = volume;
        _environmentSource.volume = volume;
        _sfxSource.volume = volume;

        _curMasterVol = volume;

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
        _environmentButton.GetComponent<Image>().sprite = _soundOnImage;
    }
    public void SetSFxVolume(float volume)
    {
        _sfxSource.enabled = true;
        _sfxSource.volume = volume;
        _sfxButton.GetComponent<Image>().sprite = _soundOffImage;
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
            SetMasterVolume(_curMasterVol);
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
            _environmentButton.GetComponent<Image>().sprite = _soundOnImage;
            _isEnvironmentOn = false;
        }
        else
        {
            _environmentSource.enabled = true;
            _environmentButton.GetComponent<Image>().sprite = _soundOffImage;
            _isEnvironmentOn = true;
        }
    }

    public void SFxOnOff()
    {
        if(_isSfxOn)
        {
            _sfxSource.enabled = false;
            _sfxButton.GetComponent<Image>().sprite = _soundOnImage;
            _isSfxOn = false;
        }
        else
        {
            _sfxSource.enabled = true;
            _sfxButton.GetComponent<Image>().sprite = _soundOffImage;
            _isSfxOn = true;
        }        
    }

    public void SetStartSound()
    {
        if(_isStartMusicOn)
        {
            _musicSource.enabled = false;
            _startMusicButton.GetComponent<Image>().sprite = _startOnImage;
            _isStartMusicOn = false;
        }
        else
        {
            _musicSource.enabled = true;
            _startMusicButton.GetComponent<Image>().sprite = _startOffImage;
            _isStartMusicOn = true;
        }
    }
}
