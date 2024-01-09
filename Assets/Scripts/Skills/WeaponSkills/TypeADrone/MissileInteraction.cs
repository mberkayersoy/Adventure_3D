using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissileInteraction : MonoBehaviour
{

    [HideInInspector] public int damage;
    [SerializeField] private ParticleSystem _explosionVFX;
    [SerializeField] private Transform _missileModel;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private float _smoothTime;
    private Rigidbody rb; 
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        rb.velocity = Vector3.zero;
    }

    public void StartMissileMovement(Vector3 target)
    {
        targetPosition = target;
    }
    private void OnDisable()
    {
        _missileModel.gameObject.SetActive(true);
        GetComponent<Collider>().enabled = true;
    }
    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, _smoothTime * Time.deltaTime);
        transform.LookAt(targetPosition);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable enemy))
        {
            enemy.TakeDamage(20);
        }

        _explosionVFX.Play(true);
        _missileModel.gameObject.SetActive(false);
        GetComponent<Collider>().enabled = false;
    }

    private void ResetMissile()
    {
        gameObject.SetActive(false);
    }
}
