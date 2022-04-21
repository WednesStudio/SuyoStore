using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl
{
    private bool isLightOn = false;
    private int itemID;
    private Counter counter;
    private int current = -1;
    public LightControl(int light, int _itemID)
    {
        itemID = _itemID;
        if (light > 0)
        {
            counter = new Counter(light);
            isLightOn = true;
        }
    }
    public bool LightDurability()
    {
        if (isLightOn == false && current <= 0)
            return true;
        if (isLightOn)
        {
            current = counter.Update();
            if (current <= 0)
                isLightOn = false;
        }
        return false;
    }
    public int GetID() => itemID;
}
