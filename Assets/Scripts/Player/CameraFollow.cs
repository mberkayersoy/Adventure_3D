using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 offset;

    [SerializeField] private float shakeDuration;
    [SerializeField] private float shakeStrength;

    private void Start()
    {
        offset = transform.position;
    }
    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.localPosition + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            transform.LookAt(target);
        }
    }

    // Shake the camera
    public void ShakeCamera()
    {
        transform.DOShakePosition(shakeDuration, shakeStrength);  
    }
}
