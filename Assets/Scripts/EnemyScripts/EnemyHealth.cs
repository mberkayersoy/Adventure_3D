using GameSystemsCookbook;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private HealthSO enemyHealthSO;
    [SerializeField] private int _currentHealth;

    [Header("Broadcast on Event Channels")]
    [SerializeField] private Vector3EventChannelSO _enemyDeadPosition;
    [SerializeField] private VoidEventChannelSO _enemyDead;

    [Header("Listen to Event Channels")]
    [SerializeField] private VoidEventChannelSO _destoryAllEnemies;

    public string Name => throw new System.NotImplementedException();

    public GameObject PoolObject => throw new System.NotImplementedException();

    private void OnEnable()
    {
        _currentHealth = enemyHealthSO.maxHealth;
        _destoryAllEnemies.OnEventRaised += KillSelf;
    }

    private void OnDisable()
    {
        _destoryAllEnemies.OnEventRaised -= KillSelf;
    }

    private void KillSelf()
    {
        Die();
    }

    public void TakeDamage(int takenDamage)
    {
        _currentHealth -= takenDamage;

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        _enemyDeadPosition.RaiseEvent(transform.position);
        _enemyDead.RaiseEvent();
        //gameObject.SetActive(false);
        Lean.Pool.LeanPool.Despawn(gameObject);
    }
}
