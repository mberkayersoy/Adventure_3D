using UnityEngine;
using DG.Tweening;
using GameSystemsCookbook;

public class PickUpMovement : MonoBehaviour, IMagnatable
{
    [SerializeField] private Vector3EventChannelSO _magnetPickedUp;
    private void OnEnable()
    {
        _magnetPickedUp.OnEventRaised += MoveToTarget;
        StartRotation();
    }

    private void OnDisable()
    {
        StopTween();
        _magnetPickedUp.OnEventRaised -= MoveToTarget;
    }

    private void StartRotation()
    {
        transform.DORotate(new Vector3(0f, 360f, 0f), 1f, RotateMode.LocalAxisAdd).
            SetEase(Ease.Linear).
            SetLoops(-1);
    }
    public void MoveToTarget(Vector3 target)
    {
        if (target != null)
        {
            transform.DOMove(target, 0.25f);
        }
    }

    private void StopTween()
    {
        transform.DOKill();
    }
}
