using GameSystemsCookbook;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private HealthSO _playerHealthSO;
    [SerializeField] private int _currentHealth;

    [Header("Listen to Event Channels")]
    [SerializeField] private IntEventChannelSO _playerGainHealth;
    [SerializeField] private IntEventChannelSO _playerDamaged;

    [Header("Broadcast on Event Channels")]
    [SerializeField] private IntEventChannelSO _playerHealthChanged;
    private void Awake()
    {
        _currentHealth = _playerHealthSO.maxHealth;
    }

    private void OnEnable()
    {
        _playerGainHealth.OnEventRaised += Heal;
        _playerDamaged.OnEventRaised += TakeDamage;
    }
    private void OnDisable()
    {
        _playerGainHealth.OnEventRaised -= Heal;
        _playerDamaged.OnEventRaised -= TakeDamage;
    }

    private void Heal(int healPercetange)
    {
        _currentHealth += Mathf.RoundToInt(_playerHealthSO.maxHealth * healPercetange / 100);
        _playerHealthChanged.OnEventRaised(_currentHealth);
    }

    private void TakeDamage(int takenDamage)
    {
        _currentHealth -= takenDamage;
        _playerHealthChanged.OnEventRaised(_currentHealth);
    }
}
