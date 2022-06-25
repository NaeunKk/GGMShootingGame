using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Camera cam;
    public Transform minPos;
    public Transform maxPos;
    #region 스코어 관련
    public int currentScore;
    [SerializeField] private Text bestText;
    [SerializeField] private Text scoreText;
    #endregion

    private void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        //print($"EnemyCount : {Enemy.enemyCount}");
    }

    /// <summary>
    /// 점수 관련 함수
    /// </summary>
    public void PlusScore()
    {
        currentScore++;
        PlayerController.instance.currentHp++;

        if(JsonManager.instance.Data.maxScore < currentScore)
        {
            JsonManager.instance.Data.maxScore = currentScore;
        }
        UIManager.instance.DisplayScoreUI();
    }

    /// <summary>
    /// 점수 출력 함수
    /// </summary>
    public void DisplayScore()
    {
        //bestText.text = $"Best Score : {JsonManager.instance.Data.maxScore}";
        //scoreText.text = $"Score : {currentScore}";
    }
}
