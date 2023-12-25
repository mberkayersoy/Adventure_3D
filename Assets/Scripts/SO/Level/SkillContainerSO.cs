using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillContainerSO", menuName = "Level/Skill Container")]
public class SkillContainerSO : LevelObjectsSO
{
    public List<ActiveSkill> activeSkillList;
    public List<PassiveSkill> passiveSkillList;
}
