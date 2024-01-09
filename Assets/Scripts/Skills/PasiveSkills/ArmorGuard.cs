using GameSystemsCookbook;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorGuard : PassiveSkill
{
    [SerializeField] private int _receivedDamagePer;
    [SerializeField] private IntEventChannelSO _playerReceivedDamage;
    public override void UpgradeSkill()
    {
        _receivedDamagePer += 10;
        _playerReceivedDamage.RaiseEvent(_receivedDamagePer);
    }
    private void Start()
    {
        _playerReceivedDamage.RaiseEvent(_receivedDamagePer);
    }
}
