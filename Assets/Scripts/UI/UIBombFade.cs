using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using GameSystemsCookbook;
using System;
using Unity.VisualScripting;

public class UIBombFade : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO _allEnemiesDestroy;
    [SerializeField] private Image _fadeImage;
    private void OnEnable()
    {
        _allEnemiesDestroy.OnEventRaised += DisplayFade;
    }

    private void OnDisable()
    {
        _allEnemiesDestroy.OnEventRaised -= DisplayFade;
    }

    private void DisplayFade()
    {
        _fadeImage.DOColor(Color.white, 0.5f).OnComplete(() =>
        {
            _fadeImage.DOColor(new Color(1, 1, 1, 0), 0.5f);
        });
    }
}
