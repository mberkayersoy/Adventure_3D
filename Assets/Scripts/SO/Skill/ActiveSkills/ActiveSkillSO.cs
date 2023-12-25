using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveSkillSO : SkillSO
{
    [SerializeField] private float skillCoolDown = 1.5f;
    [SerializeField] private float activeTime = 3f;

    [SerializeField] private float remainingDuration;
    [SerializeField] private bool isActive = true;

    public void ActiveSkillUpdate()
    {
        if (isActive)
        {
            remainingDuration -= Time.deltaTime;

            if (remainingDuration <= 0f)
            {
                DeActivate();
            }
        }
        else
        {
            remainingDuration -= Time.deltaTime;

            if (remainingDuration <= 0f)
            {
                Activate();
            }
        }
    }
    public virtual void Activate(Transform transform = null)
    {
        remainingDuration = activeTime;
        isActive = true;
    }
    public virtual void DeActivate(Transform transform = null)
    {
        remainingDuration = skillCoolDown;
        isActive = false;
    }

}
