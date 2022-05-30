using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] private bool isMenu = false;
    [SerializeField] private Text scoreTxt;

    private Text _titleTxt;

    public static UIManager instance;
    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

    }

    public void Start()
    {
        _titleTxt = GameObject.Find("Canvas/GameTitle").GetComponent<Text>();

        _titleTxt.DOText("Run & Gun", 2f);

        DisplayScoreUI();
    }
    public void MenuScene()
    {
        SceneManager.LoadScene("Menu");
    }
    public void InGameScene()
    {
        SceneManager.LoadScene("InGame");
    }

    public void DisplayScoreUI()
    {
        if (!isMenu)
        {
            scoreTxt.text = $"Score : {GameManager.instance.currentScore}";

        }
    }
}
