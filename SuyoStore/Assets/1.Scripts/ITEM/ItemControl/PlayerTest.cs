using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    public int HP;
    public int satiety;
    public int attack;
    public int sightRange;
    void Awake()
    {
        HP = 100;
        satiety = 100;
        attack = 10;
        sightRange = 10;
    }
}
