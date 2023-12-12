using GameSystemsCookbook;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO EnemyDead;

    private void OnEnable() 
    {
        EnemyDead.OnEventRaised += UpdateDeadEnemyCount;
    }

    private void UpdateDeadEnemyCount()
    {
        Debug.Log("EnemyDead");
    }

    private void OnDisable()
    {
        EnemyDead.OnEventRaised += UpdateDeadEnemyCount;
    }


}
