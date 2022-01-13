using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Spaceship spaceship;
    [SerializeField] private TrackManager trackManager;
    [SerializeField] private StartScreen startScreen;
    [SerializeField] private GameOverScreen gameOverScreen;
    [SerializeField] private GameScreen gameScreen;

    void Start()
    {
        Time.timeScale = 0;
        startScreen.Open();
        gameScreen.Close();
        gameOverScreen.Close();
    }

    private void OnEnable()
    {
        startScreen.PlayButtonClick += OnPlayButtonClick;
        gameOverScreen.RestartButtonClick += OnRestartButtonClick;
        spaceship.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        startScreen.PlayButtonClick -= OnPlayButtonClick;
        gameOverScreen.RestartButtonClick -= OnRestartButtonClick;
        spaceship.GameOver -= OnGameOver;
    }

    private void OnPlayButtonClick()
    {
        startScreen.Close();
        gameScreen.Open();
        Time.timeScale = 1;
    }

    private void OnRestartButtonClick()
    {
        gameOverScreen.Close();
        gameScreen.Open();
        ResetGame();
        Time.timeScale = 1;
    }

    private void OnGameOver()
    {
        gameScreen.Close();
        gameOverScreen.Open();
        gameOverScreen.Result.text = gameScreen.GetResults();
        Time.timeScale = 0;
    }
    
    private void ResetGame()
    {
        spaceship.ResetParametres();
        trackManager.ResetSegments();
        gameScreen.ResetTimer();
    }   
}
