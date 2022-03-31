using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    private int _currentSceneNum;
    private void Awake() 
    {
        GM = this;
    }

    public void GameStart()
    {
        //Initial Game Setting
        //_currentSceneNum = 1;
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    public void ChangeToOtherScene(int sceneNum)
    {
        //Keep data
        gameObject.GetComponent<SceneChanger>().SceneChange(sceneNum);
    }
}
