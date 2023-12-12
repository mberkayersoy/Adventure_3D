using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyMovementSO", menuName = "Enemy/EnemyMovement")]
public class EnemyConfigSO : ScriptableObject
{
    public float speed;
    public float smoothRotation;
    public float moveStoppingDistance;
    public float rotationStopDistance;
}
