using GameSystemsCookbook;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDrink : PassiveSkill, ITimer
{
    [SerializeField] private int _healPercentage;
    [SerializeField] private float _cooldown = 5f;
    [SerializeField] private float _remainingDuration;

    [Header("Broadcast on Event Channels")]
    [SerializeField] private IntEventChannelSO _playerGainHealth;

    public float CoolDown { get => _cooldown; set => _cooldown = value; }
    public float ActiveTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public float RemainingDuration { get => _remainingDuration; }

    public bool IsActive { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    private void Start()
    {
        _remainingDuration = _cooldown;
    }

    private void Update()
    {
        _remainingDuration -= Time.deltaTime;

        if (_remainingDuration <= 0)
        {
            Activate();
            _remainingDuration = _cooldown;
        }
    }

    private void HealPlayer()
    {
        _playerGainHealth.RaiseEvent(_healPercentage);
    }

    public void Activate()
    {
        HealPlayer();
    }

    public void DeActivate()
    {
        throw new NotImplementedException();
    }

    public override void UpgradeSkill()
    {
        _healPercentage++;
    }
}
