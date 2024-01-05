using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolotovController : ActiveSkill
{
    [SerializeField] private MolotovInteraction _molotovPrefab;
    [SerializeField] private int _damage;
    [SerializeField] private float _area;
    [SerializeField] private int _molotovCount;
    [SerializeField] private MolotovInteraction[] _molotovList;
    /// <summary>
    /// params: area, damage
    /// </summary>
    public event Action<float ,int> OnActivateAction;

    private new void Start()
    {
        CreateMolotovs();
        base.Start();
    }

    private void CreateMolotovs()
    {
        _molotovList = new MolotovInteraction[_molotovCount];
        List<Vector3> directions = GenerateDirections();
        for (int i = 0; i < _molotovCount; i++)
        {
            MolotovInteraction newMolotov = Instantiate(_molotovPrefab, transform);
            _molotovList[i] = newMolotov;
            _molotovList[i].Direction = directions[i];
            //newMolotov.gameObject.SetActive(false);
        }
    }

    public override void UpgradeSkill()
    {
        _damage += 50;
        _area += 1f;
        _molotovCount += 1;
        DestroyMolotovs();
        CreateMolotovs();
    }

    public override void Activate()
    {
        base.Activate();
        ActivateMolotovs();
    }

    private void ActivateMolotovs()
    {
        List<Vector3> directions = GenerateDirections();

        for (int i = 0; i < _molotovList.Length; i++)
        {
            _molotovList[i].gameObject.SetActive(IsActive);
            _molotovList[i].Direction = directions[i];
            _molotovList[i].transform.position = transform.position;
        }
        OnActivateAction(_area, _damage);
    }

    private void DestroyMolotovs()
    {
        foreach (MolotovInteraction molotov in _molotovList)
        {
            Destroy(molotov.gameObject);
        }
    }

    public List<Vector3> GenerateDirections()
    {
        List<Vector3> vectors = new List<Vector3>();

        float angleStep = 360f / _molotovCount;

        for (int i = 0; i < _molotovCount; i++)
        {
            float angle = i * angleStep;
            Vector3 vector = Quaternion.Euler(0f, angle, 0f) * transform.forward + Vector3.up;
            vectors.Add(vector);
        }

        return vectors;
    }

    public override void DeActivate()
    {
        base.DeActivate();
        DeActivateMolotovs();
    }

    private void DeActivateMolotovs()
    {
        foreach (MolotovInteraction molotov in _molotovList)
        {
            molotov.gameObject.SetActive(IsActive);
        }
    }
}
