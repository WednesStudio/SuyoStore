using UnityEditor;
using UnityEngine;

using Types;

[CustomEditor(typeof(ItemObject))]
public class ItemObjectEditor : Editor
{
    private SerializedProperty itemName, itemId, itemType, description, itemImage, itemPrefab, durability, itemWeight;
    protected void OnEnable()
    {
        itemName = serializedObject.FindProperty("itemName");
        itemId = serializedObject.FindProperty("itemId");
        itemType = serializedObject.FindProperty("itemType");
        description = serializedObject.FindProperty("description");
        itemImage = serializedObject.FindProperty("itemImage");
        itemPrefab = serializedObject.FindProperty("itemPrefab");
        durability = serializedObject.FindProperty("durability");
        itemWeight = serializedObject.FindProperty("itemWeight");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(itemName);
        EditorGUILayout.PropertyField(itemId);
        EditorGUILayout.PropertyField(itemType);
        EditorGUILayout.PropertyField(description);
        EditorGUILayout.PropertyField(itemImage);
        EditorGUILayout.PropertyField(itemPrefab);
        EditorGUILayout.Space(15);
        EditorGUILayout.PropertyField(durability);
        EditorGUILayout.PropertyField(itemWeight);
        EditorGUILayout.Space(8);
        serializedObject.ApplyModifiedProperties();
    }
}

