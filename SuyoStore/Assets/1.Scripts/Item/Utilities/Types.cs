using System.Collections;
using UnityEngine;

namespace Types
{
    public enum ItemType
    {
        Furniture,
        Equipment,
        Used
    }
    public enum EquipmentType
    {
        Bag = 1,
        SleepItem,
        Weapon,
        Light,
        CellPhone
    }
    public enum UsedType
    {
        Food = 1,
        Healer,
        Battery
    }
    public enum Attributes
    {
        Health,
        Satiety,
        Attack,
        SightRange,
        Total
    }
}
