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
<<<<<<< HEAD
            //GameObject.Find("ItemDatabase").GetComponent<LoadExcel>().LoadItemData();
=======
            GameObject.Find("ItemDatabase").GetComponent<LoadExcel>().LoadItemData();
>>>>>>> ba1e7674 ([BUG] merge error)
        }
        GUILayout.Label("Reload CellPhone Info Database", EditorStyles.boldLabel);
        if (GUILayout.Button("Reload Info"))
        {
<<<<<<< HEAD
            //GameObject.Find("Reader").GetComponent<LoadJson>().LoadMsgData();
=======
            GameObject.Find("Reader").GetComponent<LoadJson>().LoadMsgData();
>>>>>>> ba1e7674 ([BUG] merge error)
        }
    }
}
