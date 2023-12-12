using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GuardianController : Skill
{
    [SerializeField] private GuardianInteraction guardianPartPrefab;
    [SerializeField] private const float rotationSpeed = 1f;
    [SerializeField] private int damage;
    [SerializeField] private int guardianCount;

    [SerializeField] private float orbitRadius;

    private readonly float animationDuration = 1f;
    private float angleBetweenGuardians;

    private new void Start()
    {
        RotateObject(transform);
        GenerateGuardianParts();
        base.Start();
    }

    // Rotate Guardian constantly
    void RotateObject(Transform rotatingTransform)
    {
        rotatingTransform.DORotate(new Vector3(0f, 360f, 0f), rotationSpeed, RotateMode.FastBeyond360)
            .SetLoops(-1)
            .SetEase(Ease.Linear);
    }

    // Calculate orbit position based on angle and orbit radius
    Vector3 CalculateOrbitPosition(float angle)
    {
        Vector3 orbitPosition = Quaternion.Euler(0, angle, 0) * new Vector3(0, 0, orbitRadius);
        return transform.position + orbitPosition;
    }
    private void UpgradeGuardianPower(int guardianCount, int damage, float orbitRadius)
    {
        this.guardianCount = guardianCount;
        this.damage = damage;
        this.orbitRadius = orbitRadius;

        GenerateGuardianParts();
    }
    private void GenerateGuardianParts()
    {
        angleBetweenGuardians = 360f / guardianCount;

        for (int i = 0; i < guardianCount; i++)
        {
            float angle = i * angleBetweenGuardians;
            GuardianInteraction guardianPart = Instantiate(guardianPartPrefab, transform);
            guardianPart.SetDamage(damage);

            // Calculate the position based on the angle and orbit radius
            Vector3 guardianPosition = CalculateOrbitPosition(angle);
            guardianPart.transform.position = guardianPosition;

            RotateObject(guardianPart.transform);
        }
    }

    public override void Activate()
    {
        base.Activate();

        Debug.Log("GuardianController Activate");
        foreach (Transform child in transform)
        {
            if (child == transform) continue;
            child.gameObject.SetActive(true);
        }

        transform.DOScale(Vector3.one, animationDuration);
    }
    public override void DeActivate()
    {
        base.DeActivate();
        Debug.Log("GuardianController DeActivate");
        transform.DOScale(Vector3.zero, animationDuration).OnComplete(() =>
        {
            foreach (Transform child in transform)
            {
                if (child == transform) continue;
                child.gameObject.SetActive(false);
            }
        });
    }

    public override void UpgradeSkill()
    {
        //UpgradeGuardianPower();
    }

}
