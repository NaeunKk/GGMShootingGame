using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Camera cam;
    public int currentScore;
    [SerializeField] private Text bestText;
    [SerializeField] private Text scoreText;

    private void Awake()
    {
        instance = this;
    }
    public void Update()
    {
        print($"EnemyCount : {Enemy.enemyCount}");
    }
    public void PlusScore()
    {
        currentScore++;
        if(JsonManager.instance.Data.maxScore < currentScore)
        {
            JsonManager.instance.Data.maxScore = currentScore;
        }
    }
}
