using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteraction : MonoBehaviour
{
    [SerializeField] private EnemyDamageSO _defaultEnemyDamageSO;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerHealth playerHealth))
        {
            playerHealth.TakeDamage(_defaultEnemyDamageSO.damage);
        }
    }

}
