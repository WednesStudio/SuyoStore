using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BatteryObject))]
public class BatteryObjectEditor : ItemObjectEditor
{
    private SerializedProperty restoreBattery;

    protected new void OnEnable()
    {
        base.OnEnable();
        restoreBattery = serializedObject.FindProperty("restoreBattery");
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUILayout.LabelField("Used Object Configuration", EditorStyles.boldLabel);
        EditorGUILayout.Space(4);
        EditorGUILayout.LabelField("Buffs", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(restoreBattery);
        serializedObject.ApplyModifiedProperties();
    }
}