using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] private Bird _bird;
    [SerializeField] private BirdMover _birdMover;
    [SerializeField] private PipeSpawner _pipeSpawner;
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private GameOverScreen _gameOverScreen;
    [SerializeField] private HighScore _highScore;
    [SerializeField] private Button _resetHighscoreButton;
    [SerializeField] private GameObject _resetHighscorePanel;
   

    private void OnEnable()
    {
        _startScreen.StartButtonClicked += OnStartButtonClick;
        _gameOverScreen.RestartButtonClicked += OnRestartButtonClick;
        _bird.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        _startScreen.StartButtonClicked -= OnStartButtonClick;
        _gameOverScreen.RestartButtonClicked -= OnRestartButtonClick;
        _bird.GameOver -= OnGameOver;
    }

    private void Start()
    {
        Time.timeScale = 0;
        _startScreen.Open();
        _gameOverScreen.Close();
        _highScore.Open();
    }

    private void OnStartButtonClick()
    {
        _resetHighscoreButton.gameObject.SetActive(false);
        _startScreen.Close();
        StartGame();
        _highScore.Close();
    }

    private void OnRestartButtonClick()
    {
        _gameOverScreen.Close();
        _highScore.Close();
        _pipeSpawner.ResetPool();
        StartGame();
    }

    private void StartGame()
    {
        _birdMover.enabled = true;
        Time.timeScale = 1;
        _bird.ResetPlayer();
    }

    public void OnGameOver()
    {
        Time.timeScale = 0;
        _gameOverScreen.Open();
        _highScore.Open();
        _birdMover.enabled = false;
    }

    public void ShowResetHighScorePanel()
    {
        _resetHighscorePanel.SetActive(true);
        _resetHighscoreButton.interactable = false;
    }

    public void HideResetHighScorePanel()
    {
        _resetHighscorePanel.SetActive(false);
        _resetHighscoreButton.interactable = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
