using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CustomEditorWindow : EditorWindow
{
    [MenuItem("Tools/CustomWindow")]
    public static void ShowWindow()
    {
        GetWindow<CustomEditorWindow>("CustomEditorWindow");
    }
    void OnGUI()
    {
        GUILayout.Label("Reload Item Database", EditorStyles.boldLabel);
        if (GUILayout.Button("Reload Items"))
        {
            GameObject.Find("ItemDatabase").GetComponent<LoadExcel>().LoadItemData();
        }
    }
}
