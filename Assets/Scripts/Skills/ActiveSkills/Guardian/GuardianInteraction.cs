using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianInteraction : MonoBehaviour
{
    private int damage;
    [SerializeField] private float pushForce;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable enemy))
        {
            enemy.TakeDamage(damage);

            if (other.TryGetComponent(out Rigidbody enemyRb))
            {
                enemyRb.AddForce(-enemyRb.velocity.normalized * pushForce, ForceMode.VelocityChange);
            }
        }
    }

    public void SetDamage (int damage)
    {
        this.damage = damage;
    }
}
