using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WeaponObject))]
public class WeaponObjectEditor : ItemObjectEditor
{
    private SerializedProperty attackBonus;

    protected new void OnEnable()
    {
        base.OnEnable();
        attackBonus = serializedObject.FindProperty("attackBonus");
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUILayout.LabelField("Equipment Object Configuration", EditorStyles.boldLabel);
        EditorGUILayout.Space(4);
        EditorGUILayout.LabelField("Buffs", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(attackBonus);
        serializedObject.ApplyModifiedProperties();
    }
}