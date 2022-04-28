using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneEffect : MonoBehaviour
{
    [SerializeField] Image _fadeImage;
    private float _time = 0f;
    private float _fadeTime = 1f;

    public void SceneChange(int sceneNum)
    {
        //Scene change Coroutine
        StartCoroutine(FadeFlow(sceneNum));
    }

    public void FadeEffect(int num)
    {
        StartCoroutine(FadeFlow(num));
    }

    IEnumerator FadeFlow(int num)
    {
        //Fade out
        _time = 0f;
        _fadeImage.gameObject.SetActive(true);
        Color fadeAlpha = _fadeImage.color;

        while(fadeAlpha.a < 1f)
        {
            _time += Time.deltaTime / _fadeTime;
            fadeAlpha.a = Mathf.Lerp(0, 1, _time);
            _fadeImage.color = fadeAlpha;
            yield return null;
        }
        GameManager.GM.isSceneLoadDone = true;

        //Change Scene
        if(num != -1)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(num);
            // Scene을 불러오는 것이 완료되면, AsyncOperation은 isDone 상태가 된다.
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            if(asyncLoad.isDone) GameManager.GM.isSceneLoadDone = true;
        }  
        
        //Fade In
        _time = 0f;
        yield return new WaitForSeconds(1f);

        while(fadeAlpha.a > 0f)
        {
            _time += Time.deltaTime / _fadeTime;
            fadeAlpha.a = Mathf.Lerp(1, 0, _time);
            _fadeImage.color = fadeAlpha;
            yield return null;
        }
        _fadeImage.gameObject.SetActive(false);
        GameManager.GM.isSceneLoadDone = false;
        yield return null;
    }
}
