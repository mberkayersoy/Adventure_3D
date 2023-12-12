using GameSystemsCookbook;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean;
using Lean.Pool;

public class SpawnManager : MonoBehaviour
{
    [Tooltip("Get player transform reference to spawn enemies around the player.")]
    [SerializeField] private GameObjectRuntimeSetSO playerRunTimeSet;

    [Header("Broadcast on Event Channels")]

    [Header("Listen to Event Channels")]
    [SerializeField] private Vector3EventChannelSO _enemyDeadPosition;

    [Header("Level Spawn Objects")]
    [SerializeField] private EnemyContainerSO _enemyContainer;
    [SerializeField] private ExperienceContainerSO _experienceContainer;

    [Header("Spawn Manager Settings")]
    [SerializeField] private float _outerSpawnRadius = 6f;
    [SerializeField] private float _spawnInterval = 0.1f;
    [SerializeField] private bool _shouldSpawn;

    private float _remainingTime;
    private Transform _playerTransform;
    
    private void Awake()
    {
        // Get Player Transform referance to spawn enemies around the player.
        _playerTransform = playerRunTimeSet.Items[0].transform;
    }

    private void OnEnable()
    {
        _enemyDeadPosition.OnEventRaised += SpawnExperience;
    }

    private void OnDisable()
    {
        _enemyDeadPosition.OnEventRaised -= SpawnExperience;
    }

    private void SpawnExperience(Vector3 deadEnemyPosition)
    {
        SelectRandomObject(_experienceContainer.experienceObjects);
        LeanPool.Spawn(SelectRandomObject(_experienceContainer.experienceObjects), deadEnemyPosition + Vector3.up, Quaternion.identity);
    }


    private void Update()
    {
        SpawnEnemy();
    }
    private void SpawnEnemy()
    {
        if (_shouldSpawn)
        {
            _remainingTime -= Time.deltaTime;

            if (_remainingTime <= 0)
            {
                float randomAngle = Random.Range(0f, 360f);

                float randomDistance = _outerSpawnRadius + Random.Range(0f, 1f);
                Vector2 randomCirclePoint = Quaternion.Euler(0f, 0f, randomAngle) * Vector2.up * randomDistance;
                Vector3 spawnPosition = _playerTransform.position + new Vector3(randomCirclePoint.x, 0f, randomCirclePoint.y);

                LeanPool.Spawn(SelectRandomObject(_enemyContainer.standartEnemies), spawnPosition,
                    Quaternion.LookRotation(_playerTransform.position - spawnPosition));

                _remainingTime = _spawnInterval;
            }
        }
    }
    private GameObject SelectRandomObject(PoolableData[] poolableDatas)
    {
        float totalChance = 0f;

        foreach (PoolableData poolableData in poolableDatas)
        {
            totalChance += poolableData.chanceRate;
        }

        float randomValue = Random.Range(0f, totalChance);

        foreach (PoolableData poolableData in poolableDatas)
        {
            if (randomValue < poolableData.chanceRate)
            {
                return poolableData.poolableObject;
            }
            else
            {
                randomValue -= poolableData.chanceRate;
            }
        }
        return null;
    }

    private void OnDrawGizmos()
    {
        if (_playerTransform != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_playerTransform.position, _outerSpawnRadius);
        }
    }

}
