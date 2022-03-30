using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BatteryObject))]
public class BatteryObjectEditor : ItemObjectEditor
{
    private SerializedProperty restoreBattery;
    private SerializedProperty cellPhone;

    protected new void OnEnable()
    {
        base.OnEnable();
        restoreBattery = serializedObject.FindProperty("restoreBattery");
        cellPhone = serializedObject.FindProperty("cellPhone");
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUILayout.PropertyField(cellPhone);
        EditorGUILayout.LabelField("Used Object Configuration", EditorStyles.boldLabel);
        EditorGUILayout.Space(4);
        EditorGUILayout.LabelField("Buffs", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(restoreBattery);
        serializedObject.ApplyModifiedProperties();
    }
}