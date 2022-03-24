using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FoodObject))]
public class FoodObjectEditor : ItemObjectEditor
{
    private SerializedProperty restoreSatiety;
    protected new void OnEnable()
    {
        base.OnEnable();
        restoreSatiety = serializedObject.FindProperty("restoreSatiety");
    }
    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUILayout.LabelField("Used Object Configuration", EditorStyles.boldLabel);
        EditorGUILayout.Space(4);
        EditorGUILayout.LabelField("Buffs", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(restoreSatiety);
        serializedObject.ApplyModifiedProperties();
    }
}