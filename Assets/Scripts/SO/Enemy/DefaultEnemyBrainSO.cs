using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultEnemySO", menuName = "Enemy/DefaultEnemyBrain")]
public class DefaultEnemyBrainSO : EnemyBrainSO
{
    public override void Move(Rigidbody enemyRigidbody, Transform enemyTransform, Transform target)
    {
        if (target != null)
        {
            float distanceToTarget = Vector3.Distance(enemyTransform.position, target.position);
            Vector3 newVelocity = enemyRigidbody.velocity = (target.position - enemyTransform.position).normalized * enemyConfigSO.speed * Time.fixedDeltaTime;
            newVelocity.y = 0;

            if (distanceToTarget < enemyConfigSO.moveStoppingDistance)
            {
                enemyRigidbody.velocity = Vector3.zero;
            }
        }
    }

    public void Rotate(Transform enemy, Transform target)
    {
        if (target != null)
        {
            float distanceToTarget = Vector3.Distance(enemy.position, target.position);

            if (distanceToTarget > enemyConfigSO.rotationStopDistance)
            {
                Quaternion toRotation = Quaternion.LookRotation((target.position - enemy.position).normalized, Vector3.up);
                toRotation.x = 0;
                enemy.rotation = Quaternion.Lerp(enemy.rotation, toRotation, Time.deltaTime * enemyConfigSO.smoothRotation);
            }
        }
    }
}
