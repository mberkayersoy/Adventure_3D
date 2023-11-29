using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Skill : MonoBehaviour
{
    [SerializeField] private float deActiveTime = 1.5f;
    [SerializeField] private float activeTime = 3f;

    [SerializeField] private float remainingDuration;
    [SerializeField] private bool isActive = true;

    public bool IsActive => isActive;
    public float RemainingDuration => remainingDuration;
    public float DeactivateTime => deActiveTime;
    public float ActivateTime => activeTime;
    protected void Start()
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
        isActive = true;
        remainingDuration = activeTime;
    }
    public virtual void DeActivate()
    {
        isActive = false;
        remainingDuration = deActiveTime;
    }

    public abstract void UpgradeSkill();
}