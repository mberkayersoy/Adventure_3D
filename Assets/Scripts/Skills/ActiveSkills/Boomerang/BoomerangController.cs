using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangController : ActiveSkill
{
    [SerializeField] private BoomerangInteraction _boomerangPrefab;
    [SerializeField] private int _damage;
    [SerializeField] private float _speed; // DoTweenAnimation Duration
    [SerializeField] private int _boomerangCount;
    [SerializeField] private BoomerangInteraction[] _boomerangList;

    /// <summary>
    /// params:  _damage, _speed
    /// </summary>
    public event Action<int, float> OnActivateAction;
    private new void Start()
    {
        CreateBoomerangs();
        base.Start();
    }
    private void CreateBoomerangs()
    {
        _boomerangList = new BoomerangInteraction[_boomerangCount];
        for (int i = 0; i < _boomerangCount; i++)
        {
            BoomerangInteraction newBoomerang = Instantiate(_boomerangPrefab, transform);
            _boomerangList[i] = newBoomerang;
        }
    }
    public override void DeActivate()
    {
        base.DeActivate();
        DeActivateBoomerangs();
    }

    private void DeActivateBoomerangs()
    {
        foreach (BoomerangInteraction boomerang in _boomerangList)
        {
            boomerang.gameObject.SetActive(IsActive);
        }
    }

    public override void Activate()
    {
        base.Activate();
        ActivateBoomerangs();
    }

    private void ActivateBoomerangs()
    {
        for (int i = 0; i < _boomerangList.Length; i++)
        {
            _boomerangList[i].transform.position = transform.position;
            _boomerangList[i].gameObject.SetActive(IsActive);
        }
        OnActivateAction(_damage, ActiveTime);
    }
    private void DestroyBoomerangs()
    {
        foreach (BoomerangInteraction boomerang in _boomerangList)
        {
            Destroy(boomerang.gameObject);
        }
    }
    public override void UpgradeSkill()
    {
        _damage += 100;
        _boomerangCount++;
        DestroyBoomerangs();
        CreateBoomerangs();

    }

}
