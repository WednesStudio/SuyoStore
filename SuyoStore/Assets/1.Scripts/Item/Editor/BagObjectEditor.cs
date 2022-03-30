using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BagObject))]
public class BagObjectEditor : ItemObjectEditor
{
    private SerializedProperty capacity;

    protected new void OnEnable()
    {
        base.OnEnable();
        capacity = serializedObject.FindProperty("capacity");
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUILayout.LabelField("Equipment Object Configuration", EditorStyles.boldLabel);
        EditorGUILayout.Space(4);
        EditorGUILayout.LabelField("Buffs", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(capacity);
        serializedObject.ApplyModifiedProperties();
    }
}