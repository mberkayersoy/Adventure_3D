using GameSystemsCookbook;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private HealthSO enemyHealthSO;
    [SerializeField] private int _currentHealth;

    [Header("Broadcast on Event Channels")]
    [SerializeField] private Vector3EventChannelSO EnemyDeadPosition;
    [SerializeField] private VoidEventChannelSO EnemyDead;

    [Header("Listen to Event Channels")]
    public float placeHolder;

    public string Name => throw new System.NotImplementedException();

    public GameObject PoolObject => throw new System.NotImplementedException();

    private void OnEnable()
    {
        _currentHealth = enemyHealthSO.maxHealth;
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
        EnemyDeadPosition.RaiseEvent(transform.position);
        EnemyDead.RaiseEvent();
        //gameObject.SetActive(false);
        Lean.Pool.LeanPool.Despawn(gameObject);
    }
}
