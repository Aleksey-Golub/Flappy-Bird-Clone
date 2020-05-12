using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class HappyEndScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Open()
    {
        _animator.SetTrigger("HappyEnd");
        //Debug.Log("HappyEnd");
        //_canvasGroup.alpha = 1;
    }
}