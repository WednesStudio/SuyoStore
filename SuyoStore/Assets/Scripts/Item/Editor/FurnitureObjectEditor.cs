using UnityEditor;
using UnityEngine;

using Types;

[CustomEditor(typeof(FurnitureObject))]
public class FurnitureObjectEditor : ItemObjectEditor
{
    // public UsedType usedType;
    protected new void OnEnable()
    {
        base.OnEnable();
    }
    public override void OnInspectorGUI()
    {
        // Debug.Log("Furniture selected");
        base.OnInspectorGUI();
        serializedObject.Update();
        // EditorGUILayout.LabelField("Furniture Object Configuration", EditorStyles.boldLabel);
        serializedObject.ApplyModifiedProperties();
    }
}