using System;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Text _score;
    [SerializeField] private Text _bestScore;
    [SerializeField] private Text _countOfPassedObstacles;
    [SerializeField] private Text _timerText;
    
    private float timer;

    private void OnEnable()
    {
        scoreManager.ScoreChanged += OnScoreChanged;
        scoreManager.BestScoreChanged += OnBestScoreChanged;
        scoreManager.CountOfPassedObstaclesChanged += OnCountOfPassedObstaclesChanged;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        TimeSpan timeSpan = TimeSpan.FromSeconds(timer);
        _timerText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
    }

    private void OnDisable()
    {
        scoreManager.ScoreChanged -= OnScoreChanged;
        scoreManager.BestScoreChanged -= OnBestScoreChanged;
        scoreManager.CountOfPassedObstaclesChanged -= OnCountOfPassedObstaclesChanged;
    }


    public void Open()
    {
        canvasGroup.alpha = 1;
    }

    public void Close()
    {
        canvasGroup.alpha = 0;
    }
  
    public void ResetTimer()
    {
        timer = 0;
    }

    //generating statistics for the session
    public string GetResults()
    {
        string result="";
        result += "Time: " + _timerText.text;
        result += "\nFinal score: " + _score.text;
        result += "\n" + _countOfPassedObstacles.text;
        if(scoreManager.CheckRecord())
            result += "\nCongratulations, you broke your high score!";
        return result;
    }


    //output statistics when it changes
    private void OnScoreChanged(int score)
    {
        _score.text = score.ToString();
    }
    private void OnBestScoreChanged(int bestScore)
    {
        _bestScore.text = "Best score: " + bestScore.ToString();
    }
    private void OnCountOfPassedObstaclesChanged(int count)
    {
        _countOfPassedObstacles.text = "Asteroids: " + count.ToString();
    }
}
