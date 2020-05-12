using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BirdMover))]
public class Bird : MonoBehaviour
{
    [SerializeField] private int _scoreForVictory;

    private BirdMover _mover;
    private int _score;
    private PolygonCollider2D _polygonCollider2D;

    public event UnityAction GameOver;
    public event UnityAction<int> ScoreChanged;
    public event UnityAction GameWining;
    public event UnityAction GameWon;

    public int Score => _score;

    private void Start()
    {
        _mover = GetComponent<BirdMover>();
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    public void ResetPlayer()
    {
        _score = 0;
        ScoreChanged?.Invoke(_score);
        _mover.ResetBird();
    }

    public void Die()
    {
        GameOver?.Invoke();
    }

    internal void IncreaseScore()
    {
        _score++;
        ScoreChanged?.Invoke(_score);

        if (_score == _scoreForVictory - 1)
        { 
            GameWining?.Invoke();
        }

        if (_score == _scoreForVictory)
        {
            GameWon?.Invoke();
            StartCoroutine(DelayedColliderReactivation());
        }
    }

    private IEnumerator DelayedColliderReactivation()   // для избегания случайного столкновения после перехвата управления
    {
        _polygonCollider2D.enabled = false;
        yield return new WaitForSeconds(2);
        _polygonCollider2D.enabled = true;
        _polygonCollider2D.isTrigger = true;
    }
}
