using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image startFade;
    [SerializeField] private bool isMenu = false;
    [SerializeField] private Text scoreTxt;

    private Text _titleTxt;

    public static UIManager instance;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    public void Start()
    {

        startFade.DOFade(0, 2f);

        _titleTxt = GameObject.Find("Canvas/GameTitle").GetComponent<Text>();
        _titleTxt.DOText("Run & Gun", 3f);

        Debug.Log("Tlqkf");

        DisplayScoreUI();


        //startFadeOut.gameObject.SetActive(false);
    }
    public void MenuScene()
    {
        startFade.DOFade(1, 2f);
        startFade.gameObject.SetActive(true);
        StartCoroutine(SceneMoveMenu());
    }
    public void InGameScene()
    {
        startFade.DOFade(1, 2f);
        startFade.gameObject.SetActive(true);
        StartCoroutine(SceneMoveInGame());
    }

    public void DisplayScoreUI()
    {
        if (!isMenu)
        {
            scoreTxt.text = $"Score : {GameManager.instance.currentScore}";

        }
    }

    IEnumerator SceneMoveInGame()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("InGame");
    }
    IEnumerator SceneMoveMenu()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("Menu");
    }
}
