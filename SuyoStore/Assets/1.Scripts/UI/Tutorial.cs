using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{
    [SerializeField] UIManager _uiManager;
    [SerializeField] GameObject _tutorialWindow;
    [SerializeField] GameObject _tutorialText;
    [SerializeField] GameObject _nextButton;
    [SerializeField] GameObject[] _manipulateTexts;
    [SerializeField] GameObject _deadLight;
    [SerializeField] GameObject[] _interactItems;
    private GameObject player;
    private GameObject playerParent;
    public bool is8done = false; 
    private bool[] isClear = {false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false};

    private void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerParent = player.transform.parent.gameObject;
        //2초 뒤에..
        StartCoroutine(Wait3Seconds());
        _nextButton.SetActive(true);

    }

    IEnumerator Wait3Seconds()
    {
        yield return new WaitForSeconds(2.0f);
        GetExactTutorial();
    }

    public void GetExactTutorial()
    {
        int i = 0;
        foreach(bool b in isClear)
        {
            if(b == false)
            {
                print(i);
                OpenPage(i);
                if(i == isClear.Length)
                {
                    GameManager.GM.IsTutorialItemDone = true;
                    _nextButton.SetActive(false);
                }
                return;
            }
            i ++;
        }
    }

    private void OpenPage(int num)
    {
        switch(num)
        {
            case 0 :
                _tutorialWindow.SetActive(true);
                _uiManager.SetMonologuePanel("무언가 있는 것 같다. 다가가 보자.");
                isClear[num] = true;
                break;
                
            case 1 :
                _tutorialText.SetActive(false);
                _manipulateTexts[0].SetActive(true);
                isClear[num] = true;
                break;

            case 2 : 
                _tutorialWindow.SetActive(false);
                if(_deadLight != null)  _deadLight.SetActive(true);
                isClear[num] = true;
                break;

            case 3 :
                foreach(GameObject g in _interactItems)
                {
                    g.SetActive(true);
                }
                isClear[num] = true;
                break;

            case 4 : 
                _tutorialWindow.SetActive(true);
                _manipulateTexts[0].SetActive(false);
                _manipulateTexts[1].SetActive(true);
                isClear[num] = true;
                break;

            case 5 : 
                _tutorialWindow.SetActive(false);
                isClear[num] = true;
                break;

            case 6 :
                _tutorialWindow.SetActive(true);
                _manipulateTexts[1].SetActive(false);
                _manipulateTexts[2].SetActive(true);
                isClear[num] = true;
                break;

            case 7 : 
                _tutorialWindow.SetActive(false);
                isClear[num] = true;
                break;

            case 8 :
                is8done = true;
                _tutorialText.SetActive(true);
                _manipulateTexts[2].SetActive(false);
                _uiManager.SetMonologuePanel("약을 사용하면 체력을 회복할 수 있을 것 같다.");
                isClear[num] = true;
                break;

            case 9 : 
                _tutorialWindow.SetActive(false);
                isClear[num] = true;
                break;

            case 10 :
                _uiManager.SetMonologuePanel("배가 고파서 뭐라도 먹어야할 것 같다.");
                isClear[num] = true;
                break;

            case 11 : 
                _tutorialWindow.SetActive(false);
                isClear[num] = true;
                break;

            case 12 :
                _uiManager.SetMonologuePanel("스마트폰을 켜볼까?");
                isClear[num] = true;
                break;

            case 13 : 
                _tutorialWindow.SetActive(false);
                isClear[num] = true;
                break;

            case 14 :
                _uiManager.SetMonologuePanel("중요한 소식들을 알 수 있을 것 같다. 매일 확인하자.");
                isClear[num] = true;
                break;
            
            case 15 :
                _uiManager.SetMonologuePanel("이상한 소리가 들린다, 혹시 모르니 대비하자.");
                isClear[num] = true;
                break;

            case 16 :
                _tutorialWindow.SetActive(true);
                _tutorialText.SetActive(false);
                _manipulateTexts[2].SetActive(false);
                _manipulateTexts[3].SetActive(true);
                isClear[num] = true;
                break;

            case 17 :
            _manipulateTexts[3].SetActive(false);
                _uiManager.SetMonologuePanel("다른 소리는 들리지 않는다. 이곳은 안전한 것 같다.");
                isClear[num] = true;
                break;

        }

    }
}
