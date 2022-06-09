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
    #region ���ھ� ����
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
        print($"EnemyCount : {Enemy.enemyCount}");
    }

    /// <summary>
    /// ���� ���� �Լ�
    /// </summary>
    public void PlusScore()
    {
        currentScore++;
        if(JsonManager.instance.Data.maxScore < currentScore)
        {
            JsonManager.instance.Data.maxScore = currentScore;
        }
        UIManager.instance.DisplayScoreUI();
    }

    /// <summary>
    /// ���� ��� �Լ�
    /// </summary>
    public void DisplayScore()
    {
        bestText.text = $"Best Score : {JsonManager.instance.Data.maxScore}";
        scoreText.text = $"Score : {currentScore}";
    }
}
