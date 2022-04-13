using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

public class Item
{
<<<<<<< HEAD
    public int ID;
=======
>>>>>>> ba1e7674 ([BUG] merge error)
    public string itemName;
    public int[] attributes = new int[(int)Attributes.TOTAL];
    public Item(string name, int[] attr)
    {
        itemName = name;
        attributes = attr;
    }
}
