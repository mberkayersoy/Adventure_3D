using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillShotController : ActiveSkill
{
    [SerializeField] private DrillInteraction _drillPrefab;
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private int _drillCount;
    [SerializeField] private List<DrillInteraction> _drillList;

    /// <summary>
    /// params: _speed, _damage
    /// </summary>
    public event Action<float, int> OnActivateAction;
    private new void Start()
    {
        CreateDrills();
        base.Start();
    }

    private new void Update()
    {
        base.Update();
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    private void CreateDrills()
    {
       // _drillList = new DrillInteraction[_drillCount];
        //for (int i = 0; i < _drillCount; i++)
        //{
            DrillInteraction newDrill = Instantiate(_drillPrefab, transform.position, Quaternion.identity, transform);
            _drillList.Add(newDrill);
            newDrill.gameObject.SetActive(true);
        //
    }

    private void DestroyDrills()
    {
        foreach (DrillInteraction item in _drillList)
        {
            Destroy(item.gameObject);
        }
    }
    private void ActivateDrills()
    {
        foreach (DrillInteraction drill in _drillList)
        {
            drill.gameObject.SetActive(IsActive);
        }
        OnActivateAction?.Invoke(_speed, _damage);
    }

    private void DeActivateDrills()
    {
        foreach (DrillInteraction drill in _drillList)
        {
            drill.gameObject.SetActive(IsActive);
        }
    }
    public override void Activate()
    {
        base.Activate();
        ActivateDrills();
    }

    public override void DeActivate()
    {
        base.DeActivate();
        DeActivateDrills();
    }

    public override void UpgradeSkill()
    {
        _drillCount++;
        _speed += 50f;
        _damage += 100; 
        CreateDrills();
        ActivateDrills();
    }
}
