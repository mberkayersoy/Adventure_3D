using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSkill : ActiveSkill
{
    private new void Start()
    {
        Activate();
    }
    public override void Activate()
    {
        base.Activate();
    }
    public override void DeActivate()
    {
        base.DeActivate();
    }

    public override void UpgradeSkill()
    {
        throw new System.NotImplementedException();
    }
}
