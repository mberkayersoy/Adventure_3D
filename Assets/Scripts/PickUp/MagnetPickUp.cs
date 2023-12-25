using DG.Tweening;
using GameSystemsCookbook;
using Lean.Pool;
using UnityEngine;

public class MagnetPickUp : MonoBehaviour
{
    [Header("Broadcast on Event Channels")]
    [SerializeField] private Vector3EventChannelSO _magnetPickedUp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerScoresHandler player))
        {
            _magnetPickedUp.RaiseEvent(player.gameObject.transform.position);
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
        transform.DORotate(new Vector3(0f, 360f, 0f), 1f, RotateMode.LocalAxisAdd).
            SetEase(Ease.Linear).
            SetLoops(-1);
    }

    private void StopTween()
    {
        transform.DOKill();
    }
}
