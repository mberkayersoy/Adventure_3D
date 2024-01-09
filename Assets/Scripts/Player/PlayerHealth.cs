using GameSystemsCookbook;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private HealthSO _playerHealthSO;

    [Header("Listen to Event Channels")]
    [SerializeField] private IntEventChannelSO _playerGainHealth;
    [SerializeField] private IntEventChannelSO _playerDamaged;
    [SerializeField] private IntEventChannelSO _playerReceivedDamage;

    [Header("Broadcast on Event Channels")]
    [SerializeField] private IntEventChannelSO _playerHealthChanged;

    private int _receivedDamagePer = 0;
    private int _currentHealth;
    private void Awake()
    {
        _currentHealth = _playerHealthSO.maxHealth;
    }

    private void OnEnable()
    {
        _playerGainHealth.OnEventRaised += Heal;
        _playerDamaged.OnEventRaised += TakeDamage;
        _playerReceivedDamage.OnEventRaised += SetDecreaseTakenDamage;
    }


    private void OnDisable()
    {
        _playerGainHealth.OnEventRaised -= Heal;
        _playerDamaged.OnEventRaised -= TakeDamage;
        _playerReceivedDamage.OnEventRaised -= SetDecreaseTakenDamage;
    }
    private void SetDecreaseTakenDamage(int receivedDamagePer)
    {
        _receivedDamagePer = receivedDamagePer;
    }

    private void Heal(int healPercetange)
    {
        _currentHealth += Mathf.RoundToInt(_playerHealthSO.maxHealth * healPercetange / 100);
        ClampHealth();
        _playerHealthChanged.OnEventRaised(_currentHealth);
    }

    private void TakeDamage(int takenDamage)
    {
        _currentHealth -= CalculateTakenDamage(takenDamage);
        ClampHealth();
        _playerHealthChanged.OnEventRaised(_currentHealth);
    }

    private int CalculateTakenDamage(int takenDamage)
    {
        Debug.Log("Before TakenDamage: " + takenDamage);
        Debug.Log("After TakenDamage: " + (takenDamage - Mathf.RoundToInt(takenDamage * _receivedDamagePer / 100)));
        return takenDamage - Mathf.RoundToInt(takenDamage * _receivedDamagePer / 100);
    }
    private void ClampHealth()
    {
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _playerHealthSO.maxHealth);
    }
}
