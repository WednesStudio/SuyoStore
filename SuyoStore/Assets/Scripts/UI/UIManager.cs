using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject _gameStartUI;
    [SerializeField] GameObject _inGameUI;
    [SerializeField] GameObject _mainCamera;

    public void GameStartUI()
    {
        _gameStartUI.SetActive(false);
        _inGameUI.SetActive(true);
        _mainCamera.SetActive(true);
        GameManager.GM.GameStart();
    }

    public void ExitGameUI()
    {
        //Fade out
        GameManager.GM.ExitGame();
    }
}
