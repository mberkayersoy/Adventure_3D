using System;
using System.Collections;
using System.Collections.Generic;
using GameSystemsCookbook;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PlayerSkillManager : MonoBehaviour
{
    [SerializeField] private ActiveSkill _playerDefaultWeapon; // weapon is also have a Active skill behavior
    [SerializeField] private Transform _playerSkillRoot; // Skill controller's parent transform.

    [Header("Listen to Event Channels")]
    [SerializeField] private SkillEventChannelSO _skillChoose;

    [Header("Broadcast")]
    [SerializeField] private SkillEventChannelSO _skillAdded;
    [SerializeField] private SkillEventChannelSO _skillUpgraded;

    [SerializeField] private List<CardData> _currentActiveSkills = new List<CardData>();
    [SerializeField] private List<CardData> _currentPassiveSkills = new List<CardData>();

    [SerializeField] private List<Skill> _currentSkills = new List<Skill>();    
    private void OnEnable()
    {   
        _skillChoose.OnEventRaised += AddSkill;
    }

    private void OnDisable()
    {
        _skillChoose.OnEventRaised -= AddSkill;
    }
    public void AddSkill(CardData newSkill)
    {
        if (newSkill.cardSkill is ActiveSkill)
        {
            AddActiveSkill(newSkill);
        }
        else
        {
            AddPassiveSkill(newSkill);
        }
    }

    private void AddActiveSkill(CardData newActiveSkill)
    {
        CardData existingSkill = _currentActiveSkills.Find(skill => skill.cardSkill.GetType() == newActiveSkill.cardSkill.GetType());
        
        if (existingSkill != null)
        {
            UpgradeActiveSkill(existingSkill);
        }
        else
        {
            Skill newSkill = Instantiate(newActiveSkill.cardSkill, _playerSkillRoot);
            _currentSkills.Add(newSkill);
            _currentActiveSkills.Add(newActiveSkill);
            _skillAdded.RaiseEvent(newActiveSkill);
        }
    }

    private void AddPassiveSkill(CardData newPassiveSkill)
    {
        CardData existingSkill = _currentPassiveSkills.Find(skill => skill.cardSkill.GetType() == newPassiveSkill.cardSkill.GetType());
        
        if (existingSkill != null)
        {
            UpgradePassiveSkill(existingSkill);
            return;
        }
        else
        {
            Skill newSkill = Instantiate(newPassiveSkill.cardSkill, _playerSkillRoot);
            _currentSkills.Add(newSkill);
            _currentPassiveSkills.Add(newPassiveSkill);
            _skillAdded.RaiseEvent(newPassiveSkill);
        }
    }

    private void UpgradeActiveSkill(CardData upgradeSkill)
    {
        _currentSkills.Find(value => value.GetType() == upgradeSkill.cardSkill.GetType()).UpgradeSkill();
        _skillUpgraded.RaiseEvent(upgradeSkill);
    }

    private void UpgradePassiveSkill(CardData upgradeSkill)
    {
        _currentSkills.Find(value => value.GetType() == upgradeSkill.cardSkill.GetType()).UpgradeSkill();
        _skillUpgraded.RaiseEvent(upgradeSkill);
    }
}
