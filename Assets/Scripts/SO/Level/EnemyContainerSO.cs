using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyContainerSO", menuName = "Level/Enemy Object Container")]
public class EnemyContainerSO : LevelObjectsSO
{
    public PoolableData[] standartEnemies;
    public GameObject[] eliteEnemies;
    public GameObject[] bossEnemies;
}