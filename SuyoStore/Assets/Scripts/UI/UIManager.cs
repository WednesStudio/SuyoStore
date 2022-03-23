using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject _gameStartUI;
    [SerializeField] GameObject _inGameUI;
    [SerializeField] GameObject _mainCamera;

    public void GameStart()
    {
        _gameStartUI.SetActive(false);
        _inGameUI.SetActive(true);
        _mainCamera.SetActive(true);
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
