using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SfxSoundName
{
    ButtonClick, WalkSound, GetItemSoound
}

public enum EnvironmentalSoundName
{
    ZombieSound
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

    public void StopSfxSound()
    {
        if (_sfxAudio.isPlaying) _sfxAudio.Stop();
    }

    public void StopEnvironmentalSound()
    {
        if (_sfxAudio.isPlaying) _EnvironmentAudio.Stop();
    }
}
