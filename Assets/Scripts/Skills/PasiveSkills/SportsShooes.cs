using GameSystemsCookbook;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SportsShooes : PassiveSkill
{
    [SerializeField] private int _addSpeedPer = 10;
    [SerializeField] private int _gainedSpeed = 10;

    [Header("Broadcast")]
    [SerializeField] private IntEventChannelSO _increasePlayerSpeed;

    private void Start()
    {
        _increasePlayerSpeed.RaiseEvent(_gainedSpeed);
    }
    public override void UpgradeSkill()
    {
        _gainedSpeed += _addSpeedPer;
        _increasePlayerSpeed.RaiseEvent(_gainedSpeed);
    }

}
