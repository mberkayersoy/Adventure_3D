using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private HealthSO _playerHealthSO;
    [SerializeField] private int _currentHealth;

    private void Awake()
    {
        _currentHealth = _playerHealthSO.maxHealth;
    }
    public void TakeDamage(int takenDamage)
    {
        _currentHealth -= takenDamage;
    }
}
