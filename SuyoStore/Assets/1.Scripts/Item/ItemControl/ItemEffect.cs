using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

public class ItemEffect : MonoBehaviour
{
    [SerializeField]
    private PlayerStatus playerStatus;
    // need to get the player status from the player object not the playerStatus class itself
    public void UseItem(ItemObject _item)
    {
        if (_item.itemType != ItemType.Furniture)
        {
            for (int i = 0; i < (int)Attributes.Total; i++)
            {
                switch (i)
                {
                    case (int)Attributes.Health:
                        playerStatus.IncreaseHP(_item.attributes[i]);
                        break;
                    case (int)Attributes.Satiety:
                        playerStatus.IncreaseSatiety(_item.attributes[i]);
                        break;
                    case (int)Attributes.Attack:
                        playerStatus.IncreaseAttack(_item.attributes[i]);
                        break;
                    case (int)Attributes.SightRange:
                        playerStatus.IncreaseSight(_item.attributes[i]);
                        break;
                    default:
                        break;
                }
                Debug.Log(((Attributes)i).ToString() + " : " + _item.attributes[i].ToString());
            }
            Debug.Log("\" " + _item.itemName + "\" 사용");
        }
    }
}