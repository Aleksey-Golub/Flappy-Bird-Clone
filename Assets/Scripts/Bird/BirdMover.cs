using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class BirdMover : MonoBehaviour
{
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private float _speed;
    [SerializeField] private float _tapForce;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _maxRotationZ;
    [SerializeField] private float _minRotationZ;

    [SerializeField] private Transform _landingPoint;

    private Rigidbody2D _rigidbody;
    private Bird _bird;
    private Quaternion _maxRotation;
    private Quaternion _minRotation;
    private bool isGameWin = false;

    public event UnityAction HappyEnd;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _bird = GetComponent<Bird>();

        _maxRotation = Quaternion.Euler(0, 0, _maxRotationZ);
        _minRotation = Quaternion.Euler(0, 0, _minRotationZ);

        ResetBird();

        enabled = false;
    }

    private void OnEnable()
    {
        _bird.GameWon += OnGameWon;
    }

    private void OnDisable()
    {
        _bird.GameWon -= OnGameWon;
    }

    void Update()
    {
        if (isGameWin == false && Input.GetMouseButtonDown(0))    // было (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.velocity = new Vector2(_speed, 0);                   // гашение скорости перед кликом
            transform.rotation = _maxRotation;
            _rigidbody.AddForce(Vector2.up * _tapForce, ForceMode2D.Force);
        }

        if (isGameWin)
        {
            transform.position = Vector3.MoveTowards(transform.position, _landingPoint.position, _speed * Time.deltaTime);

            if (transform.position == _landingPoint.position)
            {
                HappyEnd?.Invoke();
                //_happyEndScreen.Open();
            }
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, _minRotation, _rotationSpeed * Time.deltaTime);
    }

    public void ResetBird()
    {
        transform.position = _startPosition;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        _rigidbody.velocity = Vector2.zero;
    }

    private void OnGameWon()
    {
        _rigidbody.simulated = false;
        isGameWin = true;
        _minRotation = Quaternion.Euler(0, 0, 0);
    }
}
