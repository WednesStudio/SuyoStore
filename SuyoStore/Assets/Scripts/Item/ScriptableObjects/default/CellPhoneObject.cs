using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

[CreateAssetMenu(fileName = "New Cell Phone Object", menuName = "Items/Equipment/CellPhone")]

public class CellPhoneObject : ItemObject
{
    // [Header("Cell Phone")]
    [Tooltip("배터리 충전량")]
    public int batteryCharge;
    public void Awake()
    {
        itemType = ItemType.Equipment;
    }

}
