using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

public class Item
{
    private string itemName;
    private int[] attributes = new int[(int)Attributes.TOTAL];
    public Item(string name, int[] attr)
    {
        itemName = name;
        attributes = attr;
    }
    public string GetItemName() => itemName;
    public int[] GetAttributes() => attributes;
    public int GetATTACK() => attributes[(int)Attributes.ATTACK];
    public int GetHEAL() => attributes[(int)Attributes.HEAL];
    public int GetSATIETY() => attributes[(int)Attributes.SATIETY];
    public int GetBATTERYCHARGE() => attributes[(int)Attributes.BATTERYCHARGE];
    public int GetSIGHTRANGE() => attributes[(int)Attributes.SIGHTRANGE];
    public int GetCAPACITY() => attributes[(int)Attributes.CAPACITY];
    public int GetDEATHRATE() => attributes[(int)Attributes.DEATHRATE];
    public int GetDURABILITY() => attributes[(int)Attributes.DURABILITY];
    public int GetWEIGHT() => attributes[(int)Attributes.WEIGHT];
    public void SetItemName(string name)
    {
        itemName = name;
    }
    public void SetATTACK(int amount)
    {
        attributes[(int)Attributes.ATTACK] += amount;
    }
    public void SetHEAL(int amount)
    {
        attributes[(int)Attributes.HEAL] += amount;
    }
    public void SetSATIETY(int amount)
    {
        attributes[(int)Attributes.SATIETY] += amount;
    }
    public void SetBATTERYCHARGE(int amount)
    {
        attributes[(int)Attributes.BATTERYCHARGE] += amount;
    }
    public void SetSIGHTRANGE(int amount)
    {
        attributes[(int)Attributes.SIGHTRANGE] += amount;
    }
    public void SetCAPACITY(int amount)
    {
        attributes[(int)Attributes.CAPACITY] += amount;
    }
    public void SetDEATHRATE(int amount)
    {
        attributes[(int)Attributes.DEATHRATE] += amount;
    }
    public void SetDURABILITY(int amount)
    {
        attributes[(int)Attributes.DURABILITY] = amount;
    }
    public void SetWEIGHT(int amount)
    {
        attributes[(int)Attributes.WEIGHT] += amount;
    }
}