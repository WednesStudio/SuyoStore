using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CellPhoneObject))]
public class CellPhoneEditor : ItemObjectEditor
{
    private SerializedProperty batteryCharge;

    protected new void OnEnable()
    {
        base.OnEnable();
        batteryCharge = serializedObject.FindProperty("batteryCharge");
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUILayout.LabelField("Equipment Object Configuration", EditorStyles.boldLabel);
        EditorGUILayout.Space(4);
        EditorGUILayout.LabelField("Buffs", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(batteryCharge);
        serializedObject.ApplyModifiedProperties();
    }
}