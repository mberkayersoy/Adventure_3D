using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class BoomerangInteraction : MonoBehaviour
{
    [EnumPaging] public Ease easeMode = Ease.Linear;
    [SerializeField] private float distanceMult;
    private int _damage;
    private float _duration;
    private Tween _rotationTween;
    private Tween _movementTween;
    private BoomerangController _controller;

    private void Awake()
    {
        _controller = GetComponentInParent<BoomerangController>();
        transform.SetParent(null, true);
    }

    private void OnEnable()
    {
        _controller.OnActivateAction += Controller_OnActivateAction;
        Rotate();
        Move();
    }

    private void Controller_OnActivateAction(int damage, float duration)
    {
        _damage = damage;
        _duration = duration;
    }

    private void OnDisable()
    {
        _controller.OnActivateAction -= Controller_OnActivateAction;
        KillTweens();
    }
    private void Rotate()
    {
        _rotationTween = transform.DORotate(new Vector3(0f, 360f, 0f), 0.5f, RotateMode.LocalAxisAdd)
            .SetLoops(-1)
            .SetEase(Ease.Linear);
    }

    private void KillTweens()
    {
        _rotationTween.Kill();
        _movementTween.Kill();
    }
    private void Move()
    {
        Vector3 randomDirection = Random.onUnitSphere;
        Vector3 normalizedVector = new Vector3(randomDirection.x, 0f, randomDirection.z).normalized;

        _movementTween =  transform.DOMove(normalizedVector * distanceMult, _duration * 1 / 3).SetEase(easeMode).
            OnComplete(() => transform.DOMove(-normalizedVector * distanceMult * 2, _duration * 2 / 3).SetEase(easeMode));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable enemy))
        {
            enemy.TakeDamage(_damage);
        }
    }
}
