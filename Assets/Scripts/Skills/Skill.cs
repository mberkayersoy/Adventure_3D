using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Skill : MonoBehaviour
{
    [SerializeField] private float skillCoolDown = 1.5f;
    [SerializeField] private float activeTime = 3f;

    [SerializeField] private float remainingDuration;
    [SerializeField] private bool isActive = true;

    public float SkillCoolDown { get => skillCoolDown; set => skillCoolDown = value; }
    public float ActiveTime { get => activeTime; set => activeTime = value; }
    public float RemainingDuration { get => remainingDuration; }
    public bool IsActive { get => isActive; set => isActive = value; }

    protected virtual void Start()
    {
        Activate();
    }

    protected virtual void Update()
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

    public virtual void Activate()
    {
        remainingDuration = activeTime;
        isActive = true;
    }
    public virtual void DeActivate()
    {
        remainingDuration = skillCoolDown;
        isActive = false;
    }

    public abstract void UpgradeSkill();
}