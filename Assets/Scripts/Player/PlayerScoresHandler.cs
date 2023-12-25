using GameSystemsCookbook;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoresHandler : MonoBehaviour
{
    [SerializeField] private int _currentLevel = 1;
    [SerializeField] private int _currentXP = 0;
    [SerializeField] private int _experienceMultiplier = 1000;

    [Header("Listen to Event Channels")]
    [SerializeField] private IntEventChannelSO _experienceGained;

    [Header("Broadcast on Event Channels")]
    [SerializeField] private IntEventChannelSO _playerLeveledUp;
    [SerializeField] private ExperienceDataEventChannelSO _playerExperienceData;

    private ExperienceData _experienceData;
    private void OnEnable()
    {
        _experienceGained.OnEventRaised += UpdateExperience;
        SendExperienceData();
    }

    private void OnDisable()
    {
        _experienceGained.OnEventRaised -= UpdateExperience;
    }

    private void SendExperienceData()
    {
        _experienceData = new ExperienceData();
        _experienceData.currentLevel = _currentLevel;
        _experienceData.currentXP = _currentXP;
        _experienceData.experienceMultiplier = _experienceMultiplier;

        _playerExperienceData.RaiseEvent(_experienceData);
    }
    private void UpdateExperience(int gainedExperience)
    {
        _currentXP += gainedExperience;

        if (_currentXP >= _currentLevel * _experienceMultiplier)
        {
            IncreaseLevel();
        }
        SendExperienceData();
    }
    /// <summary>
    /// Reset the xp value every time the player levels up.
    /// </summary>
    private void IncreaseLevel()
    {
        _currentLevel++;
        _playerLeveledUp.RaiseEvent(_currentLevel);
        _currentXP = 0;
        SendExperienceData();
    }
}

[Serializable]
public struct ExperienceData
{
    public int currentLevel;
    public int currentXP;
    public int experienceMultiplier;
}
