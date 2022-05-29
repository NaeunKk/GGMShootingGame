using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text scoreTxt;
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
        scoreTxt.text = $"Score : {GameManager.instance.currentScore}";
    }
}
