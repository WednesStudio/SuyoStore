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

        //Change Scene
        if(num != -1)  SceneManager.LoadScene(num);
        
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
        yield return null;
    }
}
