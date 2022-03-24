using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HealerObject))]
public class HealerObjectEditor : ItemObjectEditor
{
    private SerializedProperty restoreHealth;
    protected new void OnEnable()
    {
        base.OnEnable();
        restoreHealth = serializedObject.FindProperty("restoreHealth");
    }
    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUILayout.LabelField("Used Object Configuration", EditorStyles.boldLabel);
        EditorGUILayout.Space(4);
        EditorGUILayout.LabelField("Buffs", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(restoreHealth);
        serializedObject.ApplyModifiedProperties();
    }
}