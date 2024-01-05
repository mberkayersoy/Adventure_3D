using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class GuardianController : ActiveSkill
{
    [SerializeField] private GuardianInteraction _guardianPartPrefab;
    [SerializeField] private int _damage;
    [SerializeField] private int _guardianCount = 2;

    private readonly float _scaleAnimationDuration = 1f;
    private float _rotationSpeedDuration = 1f;

    private const float _orbitRadius = 3f;

    private Tween _rotationTween;
    private Tween _scaleTween;
    private new void Start()
    {
        GenerateGuardianParts();
        RotateObject();
        base.Start();
    }

    // Rotate Guardian constantly
    private void RotateObject( )
    {
        _rotationTween = transform.DORotate(new Vector3(0f, 360f, 0f), _rotationSpeedDuration, RotateMode.LocalAxisAdd)
            .SetLoops(-1)
            .SetEase(Ease.Linear);
    }

    private Vector3 CalculateOrbitPosition(float angle)
    {
        Vector3 orbitPosition = Quaternion.Euler(0, angle, 0) * new Vector3(0, 0, _orbitRadius);
        return transform.position + orbitPosition;
    }
    private void GenerateGuardianParts()
    {
        float _angleBetweenGuardians = 360f / _guardianCount;

        for (int i = 0; i < _guardianCount; i++)
        {
            float angle = i * _angleBetweenGuardians;

            // Calculate the position based on the angle and orbit radius
            Vector3 guardianPosition = CalculateOrbitPosition(angle);

            GuardianInteraction guardianPart = Instantiate(_guardianPartPrefab, transform);//, guardianPosition, Quaternion.identity, transform);
            guardianPart.SetDamage(_damage);
            guardianPart.gameObject.transform.position = guardianPosition;
        }
    }

    public override void Activate()
    {
        base.Activate();
        
        foreach (Transform child in transform)
        {
            if (child != transform && child != null) child.gameObject.SetActive(true);
        }

        _scaleTween = transform.DOScale(Vector3.one, _scaleAnimationDuration);
    }
    public override void DeActivate()
    {
        base.DeActivate();
        _scaleTween = transform.DOScale(Vector3.zero, _scaleAnimationDuration).OnComplete(() =>
        {
            foreach (Transform child in transform)
            {
                if (child != transform && child != null) child.gameObject.SetActive(false);
            }
        });
    }

    public override void UpgradeSkill()
    {
        // To prevent DoScale tween bug.
        _scaleTween.Kill();
        transform.localScale = Vector3.one;

        _guardianCount++;
        _damage *= 2;

        DestroyOldGuardians();
        GenerateGuardianParts();
    }
    private void DestroyOldGuardians()
    {
        foreach (Transform child in transform)
        {
            if (child != transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
