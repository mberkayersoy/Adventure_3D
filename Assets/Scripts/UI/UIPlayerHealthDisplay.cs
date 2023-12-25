using GameSystemsCookbook;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerHealthDisplay : MonoBehaviour
{
    [SerializeField] private Image _fillImage;
    [SerializeField] private HealthSO _playerHealth;

    [Header("Listen to Event Channels")]
    [SerializeField] private IntEventChannelSO _playerHealthChanged;

    private void OnEnable()
    {
        _playerHealthChanged.OnEventRaised += UpdateHealthDisplay;
        _fillImage.fillAmount = 1;
    }
    private void OnDisable()
    {
        _playerHealthChanged.OnEventRaised += UpdateHealthDisplay;
    }

    private void UpdateHealthDisplay(int currentHealth)
    {
        float targetFillAmount = (float)currentHealth / _playerHealth.maxHealth;

        Debug.Log("TargetFill: " + targetFillAmount);
        _fillImage.fillAmount = targetFillAmount;
    }

}
