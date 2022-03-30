using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

[CreateAssetMenu(fileName = "New Sleep Item Object", menuName = "Items/Equipment/SleepItem")]

public class SleepItemObject : ItemObject
{
    // [Header("Sleep Item")]
    [Tooltip("사망률(%)")]
    public int deathRate;
    [Tooltip("체력 회복률")]
    public int restoreHealth;
    [Tooltip("포만감")]
    public int decreaseSatiety;

    public void Awake()
    {
        itemType = ItemType.Equipment;
        attributes[(int)Attributes.Health] = restoreHealth;
        attributes[(int)Attributes.Satiety] = decreaseSatiety;
    }
    public void OnValidate()
    {
        attributes[(int)Attributes.Health] = restoreHealth;
        attributes[(int)Attributes.Satiety] = decreaseSatiety;
    }
    public void onEquip()
    {
        // run random (death rate)
        // if (survive)
        // 
        //UseItem(this);
    }
}
