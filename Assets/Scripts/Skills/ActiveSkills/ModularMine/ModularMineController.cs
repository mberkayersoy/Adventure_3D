using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModularMineController : ActiveSkill
{
    [SerializeField] private MineInteraction _minePrefab;
    [SerializeField] private int _mineCount;
    [SerializeField] private int _mineDamage;
    [SerializeField] private int _explosionRadius;
    [SerializeField] private float _minePlacementRadius;
    [SerializeField] private MineInteraction[] _mineList;
    [SerializeField] private LayerMask _minePlacementLayer;
    private new void Start()
    {
        CreateMines();
        base.Start();
    }
    public override void UpgradeSkill()
    {
        _mineCount++;
        _mineDamage += 100;
        _explosionRadius++;
        DestroyMines();
        CreateMines();
    }

    public override void DeActivate()
    {
        base.DeActivate();
        DeActivateMines();
    }

    private void DeActivateMines()
    {
        foreach (MineInteraction mine in _mineList)
        {
            mine.gameObject.SetActive(false);
        }
    }
    private void DestroyMines()
    {
        foreach (MineInteraction mine in _mineList)
        {
            Destroy(mine.gameObject);
        }
    }
    public override void Activate()
    {
        base.Activate();
        ActivateMines();
    }

    private void ActivateMines()
    {
        Vector3[] minePositions = FindPositionForMine();

        for (int i = 0; i < _mineCount; i++)
        {
            _mineList[i].transform.position = minePositions[i];
            _mineList[i].gameObject.SetActive(true);
        }
    }

    private void CreateMines()
    {
        _mineList = new MineInteraction[_mineCount];

        for (int i = 0; i < _mineCount; i++)
        {
            _mineList[i] = Instantiate(_minePrefab);
            _mineList[i].gameObject.SetActive(false);
            _mineList[i].SetMineDatas(_mineDamage, _explosionRadius);
        }

    }

    private Vector3[] FindPositionForMine()
    {
        Vector3[] minePositions = new Vector3[_mineCount];

        for (int i = 0; i < _mineCount; i++)
        {
            Vector3 randomPoint = UnityEngine.Random.insideUnitSphere * _minePlacementRadius;
            randomPoint.y = 0f; 

            RaycastHit hit;

            if (Physics.Raycast(new Vector3(transform.position.x + randomPoint.x, 1f, transform.position.z + randomPoint.z), Vector3.down, out hit, 100f, _minePlacementLayer))
            {
                minePositions[i] = hit.point;
            }
            else
            {
                minePositions[i] = Vector3.zero;
            }
        }

        return minePositions;
    }

}
