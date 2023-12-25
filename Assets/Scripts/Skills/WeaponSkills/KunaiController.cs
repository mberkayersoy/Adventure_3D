using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiController : WeaponSkill
{
    [SerializeField] private GameObject _kunaiPrefab;
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;

    /// <summary>
    /// params: _damage, _speed
    /// </summary>
    public Action<int, float> OnActiveAction;
    private new void Start()
    {
        ActivateKunai();
    }

    // Update is called once per frame
    private new void Update()
    {
        
    }

    public override void Activate()
    {
        base.Activate();
        ActivateKunai();
    }

    private void ActivateKunai()
    {
        throw new NotImplementedException();
    }

    public override void DeActivate()
    {
        base.DeActivate();
        DeActivateKunai();
    }

    private void DeActivateKunai()
    {
        throw new NotImplementedException();
    }
}
