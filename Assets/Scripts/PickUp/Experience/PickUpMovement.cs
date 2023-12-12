using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Lean.Pool;

public class PickUpMovement : MonoBehaviour
{
    private void OnEnable()
    {
        StartRotation();
    }

    private void OnDisable()
    {
        StopRotation();
    }

    private void StartRotation()
    {
        transform.DORotate(new Vector3(0f, 360f, 0f), 1f, RotateMode.LocalAxisAdd).
            SetEase(Ease.Linear).
            SetLoops(-1);
    }

    private void StopRotation()
    {
        transform.DOKill();
    }
}
