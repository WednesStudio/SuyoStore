using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SfxSoundName
{
    BtnExit, BtnGo, BtnPopUp
}

public enum EnvironmentalSoundName
{
    BtnExit, BtnGo, BtnPopUp
}
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _bgmAudio = null;
    [SerializeField] private AudioSource _EnvironmentAudio = null;
    [SerializeField] private AudioSource _sfxAudio = null;
    [SerializeField] private AudioClip[] _sfxs = null;
    [SerializeField] private AudioClip[] _environmentals = null;
    public static SoundManager SM;
    private SoundController _soundController;

    private void Awake()
    {
        SM = this;
        _soundController = gameObject.GetComponent<SoundController>();
    }

    public void PlaySfxSound(SfxSoundName sName)
    {
        _sfxAudio.PlayOneShot(_sfxs[(int)sName]);
    }

    public void PlayEnvironmentalSound(EnvironmentalSoundName eName)
    {
        _EnvironmentAudio.PlayOneShot(_environmentals[(int)eName]);
    }

}
