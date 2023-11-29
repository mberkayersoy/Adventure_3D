using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int health;
    public void TakeDamage(int takenDamage)
    {
        health -= takenDamage;

        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

}
