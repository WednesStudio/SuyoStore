// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// [System.Serializable]
// public class ItemEffect
// {
//     public string itemName; // 아이템 이름 (키값)
//     [Tooltip("HP, SP, DP, HUNGRY 만 가능합니다")] // part 를 위한 툴팁
//     public string[] part; // 부위
//     public int[] num; // 수치
// }

// public class ItemEffectDatabase : MonoBehaviour
// {
//     private ItemEffect[] itemEffects;

//     // 필요한 컴포넌트
//     private StatusController thePlayerStatus;
//     private const string HP = "HP", SP = "SP", DP = "DP", HUNGRY = "HUNGRY";
//     public void UseItem(Item _item)
//     {
//         if (_item.itemType == Item.ItemType.Used)
//         {
//             for (int x = 0; x < itemEffects.Length; x++)
//             {
//                 if (itemEffects[x].itemName == _item.itemName)
//                 {
//                     for (int y = 0; y < itemEffects[x].part.Length; y++)
//                     {
//                         switch (itemEffects[x].part[y])
//                         {
//                             case HP:
//                                 thePlayerStatus.IncreaseHP(itemEffects[x].num[y]);
//                                 break;
//                             case SP:
//                                 thePlayerStatus.IncreaseSP(itemEffects[x].num[y]);
//                                 break;
//                             case DP:
//                                 thePlayerStatus.IncreaseDP(itemEffects[x].num[y]);
//                                 break;
//                             case HUNGRY:
//                                 thePlayerStatus.IncreaseHungry(itemEffects[x].num[y]);
//                                 break;
//                             default:
//                                 Debug.log("잘못된 Status 부위");
//                                 break;
//                         }
//                         Debug.Log(_item.itemName + " 을 사용했습니다");
//                     }
//                     return;
//                 }
//             }
//             Debug.log("ItemEffectDatabase 에 일치하는 itemName 이 없음");
//         }
//     }
// }