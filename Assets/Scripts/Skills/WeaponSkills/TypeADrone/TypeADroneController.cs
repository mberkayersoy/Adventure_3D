using System.Collections;
using UnityEngine;


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
    [SerializeField] private MissileInteraction _missiliesPrefab;
    [SerializeField] private MissileInteraction[] _missiliesList;
    [SerializeField] private int _missileDamage = 100; // For each missile
    [SerializeField] private int _missiliesCount = 15;
    [SerializeField] private LayerMask _missileInteractLayer;

    private Transform _target;
    private Vector3 refVelocity = Vector3.zero;

    private new void  Start()
    {
        _target = transform;
        _droneModel.SetParent(null, false); // To avoid being affected by the parent's transform.
        CreateMissiles();
        base.Start();
    }
    public override void DeActivate()
    {
        base.DeActivate();
        DeActivateMissiles();
    }
    public override void Activate()
    {
        base.Activate();
        StartCoroutine(ActivateMissiles());
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

    private IEnumerator ActivateMissiles()
    {
        Vector3 targetPoint = FindFireDirection();
        float deviationRadius = 0.5f;
        foreach (MissileInteraction missile in _missiliesList)
        {
            missile.transform.position = _droneModel.position;
            Vector3 deviation = new Vector3(Random.Range(-deviationRadius, deviationRadius), 0f, Random.Range(-deviationRadius, deviationRadius));
            Vector3 targetWithDeviation = targetPoint + deviation;
            yield return new WaitForSeconds(0.05f);
            missile.gameObject.SetActive(true);
            missile.StartMissileMovement(targetWithDeviation);
        }
    }
    private void DeActivateMissiles()
    {
        foreach (MissileInteraction missile in _missiliesList)
        {
            missile.gameObject.SetActive(false);
        }
    }

    private Vector3 FindFireDirection()
    {
        RaycastHit hit;

        if (Physics.Raycast(_droneModel.position, _droneModel.forward - Vector3.up, out hit, 100f, _missileInteractLayer))
        {
            return hit.point;
        }
        return Vector3.zero;
    }
    private void CreateMissiles()
    {
        _missiliesList = new MissileInteraction[_missiliesCount];
        for (int i = 0; i < _missiliesCount; i++)
        {
            _missiliesList[i] = Instantiate(_missiliesPrefab);
            _missiliesList[i].gameObject.SetActive(false);
            _missiliesList[i].damage = _missileDamage;
        }
    }


    #endregion
    public override void UpgradeSkill()
    {
        _missileDamage += 100;
    }

    private void OnDrawGizmos()
    {
        if (FindFireDirection() != Vector3.zero)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(FindFireDirection(), 0.25f);
        }
    }
}
