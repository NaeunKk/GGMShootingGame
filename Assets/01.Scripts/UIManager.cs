using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    #region 페이드 인 / 아웃
    [Header("페이드")]
    [SerializeField] private Image startFade;
    #endregion
    #region 플레이 방법
    [Header("플레이 방법")]
    [SerializeField] private Image htp;
    #endregion
    #region 메뉴
    [Header("메뉴")]
    [SerializeField] private bool isMenu = false;
    #endregion
    #region 스코어
    [Header("스코어")]
    [SerializeField] private Text scoreTxt;
    #endregion
    #region 게임 타이틀
    private Text _titleTxt;
    #endregion

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
        _titleTxt.DOText("Hungry Dino", 3f);

        Debug.Log("Tlqkf");

        DisplayScoreUI();


        //startFadeOut.gameObject.SetActive(false);
    }

    /// <summary>
    /// 메뉴 판넬 함수
    /// </summary>
    public void MenuScene()
    {
        startFade.DOFade(1, 2f);
        startFade.gameObject.SetActive(true);
        StartCoroutine(SceneMoveMenu());
    }

    /// <summary>
    /// 인게임 전환 버튼 함수
    /// </summary>
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

    /// <summary>
    /// 게임 방법 실행 함수
    /// </summary>
    public void HowToPlayOpen()
    {
        htp.gameObject.SetActive(true);
    }

    /// <summary>
    /// 게임 방법 닫기 함수
    /// </summary>
    public void HowToPlayClose()
    {
        htp.gameObject.SetActive(false);
    }

    /// <summary>
    /// 인게임 전환 함수
    /// </summary>
    /// <returns></returns>
    IEnumerator SceneMoveInGame()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("InGame");
    }

    /// <summary>
    /// 메뉴 전환 함수
    /// </summary>
    /// <returns></returns>
    IEnumerator SceneMoveMenu()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("Menu");
    }
}
