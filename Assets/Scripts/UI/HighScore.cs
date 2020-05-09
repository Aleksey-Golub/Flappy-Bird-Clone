using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    [SerializeField] private TMP_Text _highScore;
    [SerializeField] private Bird _bird;
    [SerializeField] private CanvasGroup _canvasGroup;

    private void Start()
    {
        _highScore.text = "High Score: " + PlayerPrefs.GetInt("HighScore").ToString();
    }

    private void OnEnable()
    {
        _bird.GameOver += OnGameOver;
    }


    private void OnDisable()
    {
        _bird.GameOver -= OnGameOver;
    }

    public void Open()
    {
        _canvasGroup.alpha = 1;
    }

    public void Close()
    {
        _canvasGroup.alpha = 0;
    }

    private void OnGameOver()
    {
        int savedScore = PlayerPrefs.GetInt("HighScore");

        if (_bird.Score > savedScore)
        {
            PlayerPrefs.SetInt("HighScore", _bird.Score);
            _highScore.text = "High Score: " + _bird.Score.ToString();
        }
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        _highScore.text = "High Score: 0";
        Debug.Log("reset");
    }
}