using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Skill : MonoBehaviour
{
    public abstract void UpgradeSkill();
    public string GetUIInfo(int index)
    {
        return uiInfos[index];
    }

    [Range(0,6)] // 0 is unlock, 6 is evo.
    [SerializeField] protected int skillLevel;
    [SerializeField] protected string[] uiInfos;
}