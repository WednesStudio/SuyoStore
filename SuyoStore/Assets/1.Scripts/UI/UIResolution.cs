using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIResolution : MonoBehaviour
{
    [SerializeField] Canvas _mainCanvas;
    [SerializeField] GameObject[] _panels;
    private int setWidth = 1920; 
    private int setHeight = 1080;
    private int[,] _resolutionArray = new int[5, 2] {{1920, 1080}, {1600, 1024}, {1600, 900}, {1366, 768}, {1280, 720}};
    private Vector3[] _panelScales = {new Vector3(1f, 1f, 0f), new Vector3(0.83f, 0.94f, 0f), new Vector3(0.83f, 0.83f, 0f), new Vector3(0.71f, 0.71f, 0f), new Vector3(0.66f, 0.66f, 0f)};
    
    private void Start()
    {
        SetResolution();
        gameObject.GetComponent<OptionSettingUI>().OffResolutionSetting();
    }

    public void GetResolution(int width, int height)
    {
        _mainCanvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(width, height);
    }

    //Set Resolution
    public void SetResolution()
    {
        int deviceWidth = Screen.width; 
        int deviceHeight = Screen.height; 

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true); 

        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) 
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); 
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f);
        }
        else
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); 
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); 
        }
    }

    public void ResolutionSetting(int num)
    {
        for(int i = 0; i < 3; i ++)
        {
            GetResolution(_resolutionArray[num, 0], _resolutionArray[num, 1]);
            _panels[i].GetComponent<RectTransform>().localScale = _panelScales[num];
        }
                
        gameObject.GetComponent<OptionSettingUI>().OffResolutionSetting();
    }
}
