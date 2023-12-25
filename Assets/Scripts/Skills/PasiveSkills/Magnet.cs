using GameSystemsCookbook;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : PassiveSkill
{
    [SerializeField] private float magnetRange;
    private CapsuleCollider magnetCollider;

    private void Awake()
    {
        magnetCollider = GetComponent<CapsuleCollider>();
        magnetCollider.radius = magnetRange;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IMagnatable magnatable))
        {
            magnatable.MoveToTarget(transform.position);
        }
    }

    public override void UpgradeSkill()
    {
        magnetRange++;
        magnetCollider.radius = magnetRange;
        base.UpgradeSkill(); 
    }
}
