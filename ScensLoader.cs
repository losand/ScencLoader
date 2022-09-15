using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ScensLoader : PersistentSingleton<ScensLoader>// 继承自Ps
{
    [SerializeField] Image Transitionimage;
    [SerializeField] float FadeTime = 3.5f;

    Color color;//渐变的颜色

    const string GAMEPLAY = "01";

    void Load(string scencName) 
    {
        SceneManager.LoadScene(scencName);// 加载场景
    }
    //枚举场景

    IEnumerator LoadCouroutine(string scencName) 
    {
        var loading = SceneManager.LoadSceneAsync(scencName);// 异步加载
        Transitionimage.gameObject.SetActive(true);
        loading.allowSceneActivation = false;
        //fade out
        while (color.a < 1f) 
        {
            color.a = Mathf.Clamp01(color.a + Time.unscaledDeltaTime / FadeTime);
            Transitionimage.color = color;

            yield return null;
        }
        loading.allowSceneActivation = true;
        while (color.a > 0f)
        {
            color.a = Mathf.Clamp01(color.a - Time.unscaledDeltaTime / FadeTime);
            Transitionimage.color = color;

            yield return null;
        }
        loading.allowSceneActivation = false;

    }
    public void LoadGmaePlayScenc() 
    {
        StartCoroutine(LoadCouroutine(GAMEPLAY));
    }
   
}
