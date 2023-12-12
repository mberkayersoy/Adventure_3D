using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimer
{
    public float SkillCoolDown { get; set; }
    public float ActiveTime { get; set; }
    public float RemainingDuration { get; }
    public bool IsActive { get; set; }
    public void Activate();
    public void DeActivate();
}
