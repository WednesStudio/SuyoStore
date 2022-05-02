using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDetection : MonoBehaviour
{
    BoxCollider zombieDetect;
    GameObject nearZombie;
    void Start()
    {
        zombieDetect = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Zombie")
        {
            nearZombie = other.gameObject;
            Debug.Log("[Sound System] Zombie Nearby");
            if (!SoundManager.SM.isPlayingEnvironmentalSound())
            {
                SoundManager.SM.PlayEnvironmentalSound(EnvironmentalSoundName.ZombieSound);
            }
            else
            {
                SoundManager.SM.StopSfxSound();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Zombie")
            SoundManager.SM.StopEnvironmentalSound();
    }

}
