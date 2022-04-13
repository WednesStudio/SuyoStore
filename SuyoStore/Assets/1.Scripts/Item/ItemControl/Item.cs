using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

public class Item
{
    public int ID;
    public string itemName;
    public int[] attributes = new int[(int)Attributes.TOTAL];
    public Item(string name, int[] attr)
    {
        itemName = name;
        attributes = attr;
    }
}