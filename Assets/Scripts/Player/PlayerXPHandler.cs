using GameSystemsCookbook;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerXPHandler : MonoBehaviour
{
    [SerializeField] private int _currentLevel = 1;
    [SerializeField] private int _currentXP = 0;
    [SerializeField] private int _experienceMultiplier = 1000;

    [Header("Listen to Event Channels")]
    [SerializeField] private IntEventChannelSO _experienceGained;

    [Header("Broadcast on Event Channels")]
    [SerializeField] private IntEventChannelSO _playerLeveledUp;

    private void OnEnable()
    {
        _experienceGained.OnEventRaised += UpdateExperience;
    }

    private void UpdateExperience(int gainedExperience)
    {
        _currentXP += gainedExperience;

        if (_currentXP >= _currentLevel * _experienceMultiplier)
        {
            _playerLeveledUp.RaiseEvent(_currentLevel);
            IncreaseLevel();
        }
    }
    /// <summary>
    /// Reset the xp value every time the player levels up.
    /// </summary>
    private void IncreaseLevel()
    {
        _currentLevel++;
        _currentXP = 0;
    }

    private void OnDisable()
    {
        _experienceGained.OnEventRaised -= UpdateExperience;
    }
}
