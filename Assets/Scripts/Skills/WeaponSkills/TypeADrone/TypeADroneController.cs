using DG.Tweening;
using GameSystemsCookbook;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class TypeADroneController : WeaponSkill
{
    [Header("Movement")]
    [SerializeField] private Transform _droneModel;
    [SerializeField] private float _followSpeed = 100f;
    [SerializeField] private float _droneHeight = 2f;
    [SerializeField] private float _hoverDistance = 0.5f;
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _minDistanceFromTarget = 1f;
    [SerializeField] private float _maxDistanceFromTarget = 3f;

    [Header("Attack")]
    //[SerializeField] private MissiliesInteraction _missiliesPrefab;
    //[SerializeField] private MissiliesInteraction[] _missiliesList;
    [SerializeField] private int _missilesDamage = 100; // For each missile
    [SerializeField] private int _missiliesCount = 15;

    private Transform _target;
    private Vector3 refVelocity = Vector3.zero;

    private new void  Start()
    {
        _target = transform;
        _droneModel.SetParent(null, false); // To avoid being affected by the parent's transform.
        base.Start();
    }
    public override void DeActivate()
    {
        base.DeActivate();
    }
    public override void Activate()
    {
        base.Activate();
        FireMissiles();
    }


    private new void Update()
    {
        if (_target != null)
        {
            RotateDrone();
            FollowTarget();
        }
        base.Update();
    }
    #region Movement
    float GetHoverOffset()
    {
        float hoverYOffset = Mathf.Sin(Time.time) * _hoverDistance;
        return hoverYOffset;
    }
    //void Hover()
    //{
    //    //_hoverTween = _droneModel.DOLocalMoveY(_hoverDistance, _hoverDuration).
    //    //                          SetLoops(-1, LoopType.Yoyo).
    //    //                          SetEase(Ease.Linear);
    //}

    void FollowTarget()
    {
        Vector3 toTarget = _target.position - _droneModel.position;
        toTarget.y = 0f;

        Vector3 desiredDirection = toTarget.normalized;

        Vector3 desiredPosition = _target.position - desiredDirection * Mathf.Clamp(toTarget.magnitude, _minDistanceFromTarget, _maxDistanceFromTarget);
        desiredPosition.y = _target.position.y + _droneHeight + GetHoverOffset(); // Add Hover offset.

        Vector3 smoothedPosition = Vector3.SmoothDamp(_droneModel.position, desiredPosition, ref refVelocity, _followSpeed * Time.deltaTime);
        _droneModel.position = smoothedPosition;
    }

    void RotateDrone()
    {
        _droneModel.rotation = Quaternion.Lerp(_droneModel.rotation, transform.rotation, _rotationSpeed * Time.deltaTime);
    }
    #endregion

    #region Attack

    private void FireMissiles()
    {

    }

    private void CreateMissiles()
    {

    }
    #endregion
    public override void UpgradeSkill()
    {
        throw new System.NotImplementedException();
    }

}
