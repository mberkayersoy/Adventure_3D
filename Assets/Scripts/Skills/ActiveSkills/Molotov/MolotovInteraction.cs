using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolotovInteraction : MonoBehaviour
{
    private Rigidbody _rb;
    private SphereCollider _collider;
    private MolotovController _controller;
    [SerializeField] private GameObject _molotovModel;
    [SerializeField] private ParticleSystem _fireParticle;
    private Vector3 _direction;
    private int _damage;

    public Vector3 Direction { get => _direction; set => _direction = value; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<SphereCollider>();
        _controller = GetComponentInParent<MolotovController>();
        transform.SetParent(null, true);
    }

    private void OnEnable()
    {
        DeActivateMolotov();
        _controller.OnActivateAction += Controller_OnActivateAction;
    }
    private void OnDisable()
    {
        _controller.OnActivateAction -= Controller_OnActivateAction;
    }

    private void Controller_OnActivateAction(float area, int damage)
    {
        _collider.radius = area;
        _damage = damage;
        _rb.AddForce(_direction * 4 , ForceMode.VelocityChange);
        ParticleSystem.ShapeModule shapeModule = _fireParticle.shape;
        shapeModule.radius = area;
    }

    private void OnCollisionEnter(Collision collision)
    {
        ActivateMolotov();
    }

    private void ActivateMolotov()
    {
        _rb.velocity = Vector3.zero;
        _collider.enabled = true;
        _molotovModel.SetActive(false);
        _fireParticle.Play(true);
    }
    private void DeActivateMolotov()
    {
        _collider.enabled = false;
        _molotovModel.SetActive(true);
        _fireParticle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        _rb.velocity = Vector3.zero;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable enemy))
        {
            enemy.TakeDamage(_damage);
        }
    }
}
