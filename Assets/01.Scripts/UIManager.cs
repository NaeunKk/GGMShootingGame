using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    #region ���̵� �� / �ƿ�
    [Header("���̵�")]
    [SerializeField] private Image startFade;
    #endregion
    #region �÷��� ���
    [Header("�÷��� ���")]
    [SerializeField] private Image htp;
    #endregion
    #region �޴�
    [Header("�޴�")]
    [SerializeField] private bool isMenu = false;
    #endregion
    #region ���ھ�
    [Header("���ھ�")]
    [SerializeField] private Text scoreTxt;
    #endregion
    #region ���� Ÿ��Ʋ
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
        _titleTxt.DOText("Run & Gun", 3f);

        Debug.Log("Tlqkf");

        DisplayScoreUI();


        //startFadeOut.gameObject.SetActive(false);
    }

    /// <summary>
    /// �޴� �ǳ� �Լ�
    /// </summary>
    public void MenuScene()
    {
        startFade.DOFade(1, 2f);
        startFade.gameObject.SetActive(true);
        StartCoroutine(SceneMoveMenu());
    }

    /// <summary>
    /// �ΰ��� ��ȯ ��ư �Լ�
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
    /// ���� ��� ���� �Լ�
    /// </summary>
    public void HowToPlayOpen()
    {
        htp.gameObject.SetActive(true);
    }

    /// <summary>
    /// ���� ��� �ݱ� �Լ�
    /// </summary>
    public void HowToPlayClose()
    {
        htp.gameObject.SetActive(false);
    }

    /// <summary>
    /// �ΰ��� ��ȯ �Լ�
    /// </summary>
    /// <returns></returns>
    IEnumerator SceneMoveInGame()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("InGame");
    }

    /// <summary>
    /// �޴� ��ȯ �Լ�
    /// </summary>
    /// <returns></returns>
    IEnumerator SceneMoveMenu()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("Menu");
    }
}
