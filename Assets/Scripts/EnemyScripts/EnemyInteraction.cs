using GameSystemsCookbook;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteraction : MonoBehaviour
{
    [SerializeField] private EnemyDamageSO _defaultEnemyDamage;
    [SerializeField] private IntEventChannelSO _enemyAttack;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerHealth playerHealth))
        {
            //playerHealth.TakeDamage(_defaultEnemyDamage.damage);
            _enemyAttack.RaiseEvent(_defaultEnemyDamage.damage);
        }
    }

}
