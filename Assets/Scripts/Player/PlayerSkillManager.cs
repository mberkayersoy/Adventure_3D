using System;
using System.Collections;
using System.Collections.Generic;
using GameSystemsCookbook;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    [SerializeField] private ActiveSkill _playerDefaultWeapon; // weapon is also have a Active skill behavior
    [SerializeField] private SkillInventory _inventory;
    [SerializeField] private Transform _playerSkillRoot; // Skill controller's transform parent.

    [Header("Listen to Event Channels")]
    [SerializeField] private SkillChooseEventChannelSO _skillChoose;

    private void Awake()
    {
        _inventory = new SkillInventory();
        _inventory.AddSkill(_playerDefaultWeapon);
    }

    private void OnEnable()
    {
        _skillChoose.OnEventRaised += AddChoosenSkill;
    }

    private void OnDisable()
    {
        _skillChoose.OnEventRaised -= AddChoosenSkill;
    }
    private void AddChoosenSkill(Skill newSkill)
    {
        if (newSkill is ActiveSkill)
        {
            _inventory.AddSkill(newSkill as ActiveSkill);
        }
        else
        {
            _inventory.AddSkill(newSkill as PassiveSkill);
        }
    }
}
