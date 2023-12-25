using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillInventory
{
    public Dictionary<ActiveSkill, int> currentActiveSkills;
    public Dictionary<PassiveSkill, int> currentPassiveSkills;

    [Header("Broadcast")]
    [SerializeField] private SkillChooseEventChannelSO _skillAdded;
    [SerializeField] private SkillChooseEventChannelSO _skillUpgraded;
    public void AddSkill(Skill newSkill)
    {
        if (newSkill is ActiveSkill)
        {
            AddActiveSkill(newSkill as ActiveSkill);
        }
        else
        {
            AddPassiveSkill(newSkill as PassiveSkill);
        }
    }

    private void AddActiveSkill(ActiveSkill newActiveSkill)
    {
        if (currentActiveSkills.ContainsKey(newActiveSkill)) 
        {
            UpgradeActiveSkill(newActiveSkill);

            return;
        }
        else
        {
            currentActiveSkills.Add(newActiveSkill, 1);
            _skillAdded.RaiseEvent(newActiveSkill);
        }
    }

    private void AddPassiveSkill(PassiveSkill newPassiveSkill)
    {
        if (currentPassiveSkills.ContainsKey(newPassiveSkill))
        {
            UpgradePassiveSkill(newPassiveSkill);
            return;
        }
        else
        {
            currentPassiveSkills.Add(newPassiveSkill, 1);
            _skillAdded.RaiseEvent(newPassiveSkill);
        }
    }

    private void UpgradeActiveSkill(ActiveSkill upgradeSkill)
    {
        currentActiveSkills[upgradeSkill]++;
        _skillUpgraded.RaiseEvent(upgradeSkill);
    }

    private void UpgradePassiveSkill(PassiveSkill upgradeSkill)
    {
        currentPassiveSkills[upgradeSkill]++;
        _skillUpgraded.RaiseEvent(upgradeSkill);
    }
}
