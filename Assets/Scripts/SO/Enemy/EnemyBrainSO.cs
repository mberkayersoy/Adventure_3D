using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBrainSO : ScriptableObject
{
    public EnemyConfigSO enemyConfigSO;
    public abstract void Move(Rigidbody enemyRigidbody, Transform enemyTranform, Transform target);
}
