using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class FadeInOut : MonoBehaviour
{
    [SerializeField] private Image startFadeIn;
    [SerializeField] private Image startFadeOut;

    void Start()
    {
        startFadeOut.DOFade(0, 2f);
        startFadeOut.gameObject.SetActive(false);
    }

    public void InGameScene()
    {
        startFadeOut.DOFade(1, 2f);
        startFadeOut.gameObject.SetActive(true);
        StartCoroutine(SceneMove());
    }
    IEnumerator SceneMove()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("InGame");
    }
}
