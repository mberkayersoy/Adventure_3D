using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceShildController : ActiveSkill
{
    [SerializeField] private int damage;
    [SerializeField] private int radius;
    private new SphereCollider collider;
    private new void Start()
    {
        transform.localScale = new Vector3(radius, radius, radius);
        collider = GetComponent<SphereCollider>();
        base.Start();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IDamageable enemy))
        {
            enemy.TakeDamage(damage);
        }
    }
    
    private void UpgradeForceShildPower(int damage, int radius)
    {
        this.damage = damage;
        SetRadius(radius);
    }
    private void SetRadius(int newRadius)
    {
        radius = newRadius;
        transform.localScale = new Vector3(radius, radius, radius);
    }

    public override void Activate()
    {
        base.Activate();
        collider.enabled = IsActive;
        transform.DOScale(radius, 1f);
    }

    public override void DeActivate()
    {
        base.DeActivate();
        collider.enabled = IsActive;
        transform.DOScale(Vector3.zero, 1f);
    }

    public override void UpgradeSkill()
    {
        throw new System.NotImplementedException();
    }
}
