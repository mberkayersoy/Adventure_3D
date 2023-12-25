using DG.Tweening;
using GameSystemsCookbook;
using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPickUp : MonoBehaviour
{
    [SerializeField] private GameObjectRuntimeSetSO _enemiesRunTimeSet;

    [Header("Broadcast on Event Channels")]
    [SerializeField] private VoidEventChannelSO _destroyAllEnemies;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerScoresHandler player))
        {
            _destroyAllEnemies.RaiseEvent();
            LeanPool.Despawn(this.gameObject);
        }
    }
    
    private void OnEnable()
    {
        StartRotation();
    }

    private void OnDisable()
    {
        StopTween();
    }

    private void StartRotation()
    {
        transform.DORotate(new Vector3(180f, 360f, 0f), 1f, RotateMode.LocalAxisAdd).
            SetEase(Ease.Linear).
            SetLoops(-1);
    }

    private void StopTween()
    {
        transform.DOKill();
    }
}
