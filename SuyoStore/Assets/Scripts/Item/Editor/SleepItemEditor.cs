using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SleepItemObject))]
public class SleepItemEditor : ItemObjectEditor
{
    private SerializedProperty deathRate, restoreHealth, decreaseSatiety;

    protected new void OnEnable()
    {
        base.OnEnable();
        deathRate = serializedObject.FindProperty("deathRate");
        restoreHealth = serializedObject.FindProperty("restoreHealth");
        decreaseSatiety = serializedObject.FindProperty("decreaseSatiety");
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUILayout.LabelField("Equipment Object Configuration", EditorStyles.boldLabel);
        EditorGUILayout.Space(4);
        EditorGUILayout.LabelField("Buffs", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(deathRate);
        EditorGUILayout.PropertyField(restoreHealth);
        EditorGUILayout.PropertyField(decreaseSatiety);
        serializedObject.ApplyModifiedProperties();
    }
}