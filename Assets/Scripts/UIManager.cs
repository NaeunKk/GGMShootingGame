using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Text currentScoreTxt;
    public Text bestScoreTxt;
    int currentScore;
    int bestScore;

    private void Start()
    {
        currentScore = PlayerPrefs.GetInt("Score", 0);
        currentScoreTxt.text = "Score : " + currentScore;
        bestScore = PlayerPrefs.GetInt("Best Score", 0);
        bestScoreTxt.text = "Best Score : " + bestScore;
    }
    public void Restart()
    {
        SceneManager.LoadScene("InGame");
    }
}
