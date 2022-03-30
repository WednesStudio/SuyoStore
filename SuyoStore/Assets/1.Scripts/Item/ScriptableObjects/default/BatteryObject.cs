using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

[CreateAssetMenu(fileName = "New Battery Object", menuName = "Items/Used/Battery")]
public class BatteryObject : ItemObject
{
    // [Header("Battery")]
    [Tooltip("배터리 충전률")]
    public int restoreBattery;
    public CellPhoneObject cellPhone;
    public void Awake()
    {
        itemType = ItemType.Used;
    }
    public void onEquip()
    {
        cellPhone.chargeBattery(restoreBattery);
    }
}