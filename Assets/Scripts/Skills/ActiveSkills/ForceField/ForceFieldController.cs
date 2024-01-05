using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldController : ActiveSkill
{
    [SerializeField] private int _damage;
    [SerializeField] private int _radius;
    private new SphereCollider collider;
    private new void Start()
    {
        transform.localScale = new Vector3(_radius, _radius, _radius);
        collider = GetComponent<SphereCollider>();
        base.Start();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IDamageable enemy))
        {
            enemy.TakeDamage(_damage);
        }
    }

    public override void Activate()
    {
        base.Activate();
        collider.enabled = IsActive;
        transform.DOScale(_radius, 1f);
    }

    public override void DeActivate()
    {
        base.DeActivate();
        collider.enabled = IsActive;
        transform.DOScale(Vector3.zero, 1f);
    }

    public override void UpgradeSkill()
    {
        _damage += 50;
        _radius++;
        //transform.localScale = new Vector3(_radius, _radius, _radius);
    }
}
