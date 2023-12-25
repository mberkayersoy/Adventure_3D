using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;

    private void Start()
    {
        if ( _mainCamera == null )
        {
            _mainCamera = Camera.main;
        }
    }
    private void LateUpdate()
    {
        transform.LookAt(_mainCamera.transform);
    }
}
