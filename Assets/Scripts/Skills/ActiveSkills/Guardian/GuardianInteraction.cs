using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianInteraction : MonoBehaviour
{
    private int _damage;
    private Tween _rotationTween;
    [SerializeField] private float _pushForce;

    private void Start()
    {
        _rotationTween = transform.DORotate(new Vector3(0f, 360f, 0f), 1f, RotateMode.FastBeyond360)
            .SetLoops(-1)
            .SetEase(Ease.Linear);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable enemy))
        {
            enemy.TakeDamage(_damage);

            if (other.TryGetComponent(out Rigidbody enemyRb))
            {
                enemyRb.AddForce(-enemyRb.velocity.normalized * _pushForce, ForceMode.VelocityChange);
            }
        }
    }

    public void SetDamage (int damage)
    {
        this._damage = damage;
    }

    private void OnDestroy()
    {
        _rotationTween.Kill();
    }
}
