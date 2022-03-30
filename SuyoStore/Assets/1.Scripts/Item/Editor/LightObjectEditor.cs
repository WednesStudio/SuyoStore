using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LightObject))]
public class LightObjectEditor : ItemObjectEditor
{
    private SerializedProperty sightRange;

    protected new void OnEnable()
    {
        base.OnEnable();
        sightRange = serializedObject.FindProperty("sightRange");
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUILayout.LabelField("Equipment Object Configuration", EditorStyles.boldLabel);
        EditorGUILayout.Space(4);
        EditorGUILayout.LabelField("Buffs", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(sightRange);
        serializedObject.ApplyModifiedProperties();
    }
}