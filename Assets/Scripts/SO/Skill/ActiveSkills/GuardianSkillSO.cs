using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GuardianSkillSO", menuName = "Create Skill/Active Skill/Guardian")]
public class GuardianSkillSO : ActiveSkillSO
{
    [SerializeField] private GuardianInteraction _guardianPartPrefab;
    [SerializeField] private int _damage;
    [SerializeField] private int _guardianCount;
    [SerializeField] private float _orbitRadius;

    private readonly float _animationDuration = 1f;
    private const float _rotationSpeed = 1f;
    private float _angleBetweenGuardians;

    void RotateObject(Transform rotatingTransform)
    {
        rotatingTransform.DORotate(new Vector3(0f, 360f, 0f), _rotationSpeed, RotateMode.FastBeyond360)
            .SetLoops(-1)
            .SetEase(Ease.Linear);
    }

    // Calculate orbit position based on angle and orbit radius
    Vector3 CalculateOrbitPosition(float angle, Transform transform)
    {
        Vector3 orbitPosition = Quaternion.Euler(0, angle, 0) * new Vector3(0, 0, _orbitRadius);
        return transform.position + orbitPosition;
    }

    private void GenerateGuardianParts(Transform transform)
    {
        _angleBetweenGuardians = 360f / _guardianCount;

        for (int i = 0; i < _guardianCount; i++)
        {
            float angle = i * _angleBetweenGuardians;
            GuardianInteraction guardianPart = Instantiate(_guardianPartPrefab, transform);
            guardianPart.SetDamage(_damage);

            // Calculate the position based on the angle and orbit radius
            Vector3 guardianPosition = CalculateOrbitPosition(angle, transform);
            guardianPart.transform.position = guardianPosition;

            RotateObject(guardianPart.transform);
        }
    }

    public override void Activate(Transform transform)
    {
        base.Activate();

        Debug.Log("GuardianController Activate");
        foreach (Transform child in transform)
        {
            if (child == transform) continue;
            child.gameObject.SetActive(true);
        }

        transform.DOScale(Vector3.one, _animationDuration);
    }
    public override void DeActivate(Transform transform)
    {
        base.DeActivate();
        Debug.Log("GuardianController DeActivate");
        transform.DOScale(Vector3.zero, _animationDuration).OnComplete(() =>
        {
            foreach (Transform child in transform)
            {
                if (child == transform) continue;
                child.gameObject.SetActive(false);
            }
        });
    }
}
