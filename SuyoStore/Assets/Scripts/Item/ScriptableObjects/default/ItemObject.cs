using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Types;

public abstract class ItemObject : ScriptableObject
{
    [ReadOnly]
    public string itemName; // 아이템 이름
    [ReadOnly]
    public string itemId;
    [TextArea(6, 10)]
    public string description; // 아이템 정보
    [ReadOnly]
    public ItemType itemType; // 아이템 유형
    public Sprite itemImage; // 아이템 이미지
    public GameObject itemPrefab; // 아이템 프리팹
    [Header("Basics")]
    [Tooltip("내구도")]
    public int durability; // 아이템 내구도
    [Tooltip("무게")]
    public int itemWeight; // 아이템 무게
    private void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(this);
        itemName = Path.GetFileNameWithoutExtension(path);
        long time = long.Parse(File.GetCreationTime(path).ToString("yyyMMddHHmmss"));
        itemId = itemName + "_" + time.ToString();
    }
}

public class Item
{
    public ItemObject itemObject;
}
