using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BGMSoundName
{
    MainMusic
}
public enum SfxSoundName
{
    ButtonClick
}

public enum EnvironmentalSoundName
{
    ZombieSound, WalkSound, GetItemSoound
}
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _bgmAudio = null;
    [SerializeField] private AudioSource _EnvironmentAudio = null;
    [SerializeField] private AudioSource _sfxAudio = null;
    [SerializeField] private AudioClip[] _bgms = null;
    [SerializeField] private AudioClip[] _sfxs = null;
    [SerializeField] private AudioClip[] _environmentals = null;
    public static SoundManager SM;
    private SoundController _soundController;

    private void Awake()
    {
        SM = this;
        _soundController = gameObject.GetComponent<SoundController>();
    }

    public void PlayBGMSound(BGMSoundName bName)
    {
        if(_bgmAudio.clip != null) _bgmAudio.PlayOneShot(_bgms[(int)bName]);
    }

    public void PlaySfxSound(SfxSoundName sName)
    {
        if(_sfxAudio.clip != null) _sfxAudio.PlayOneShot(_sfxs[(int)sName]);
    }

    public void PlayEnvironmentalSound(EnvironmentalSoundName eName)
    {
        if(_EnvironmentAudio.clip != null) _EnvironmentAudio.PlayOneShot(_environmentals[(int)eName]);
    }

    public void StopBGMSound()
    {
        if (_bgmAudio.isPlaying) _bgmAudio.Stop();
    }

    public void StopSfxSound()
    {
        if (_sfxAudio.isPlaying) _sfxAudio.Stop();
    }

    public void StopEnvironmentalSound()
    {
        if (_EnvironmentAudio.isPlaying) _EnvironmentAudio.Stop();
    }

    public bool isPlayingBGMSound()
    {
        if (_bgmAudio.isPlaying) return true;
        else return false;
    }

    public bool isPlayingSfxSound()
    {
        if (_sfxAudio.isPlaying) return true;
        else return false;
    }

    public bool isPlayingEnvironmentalSound()
    {
        if (_EnvironmentAudio.isPlaying) return true;
        else return false;
    }

}
