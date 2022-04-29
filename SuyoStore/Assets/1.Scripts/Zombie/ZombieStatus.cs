using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStatus : MonoBehaviour
{
    int maxHp;
    int curHp;
    int detection; // °¨Áö ¹üÀ§
    int speed;
    int curSpeed;
    int power;
    int infection; // °¨¿°·ü

    public enum eZombieStatus { eMaxHp, eDetection, eSpeed, ePower, eInfection };
}
