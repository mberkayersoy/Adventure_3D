using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillInteraction : MonoBehaviour
{
    [SerializeField] private DrillShotController _controller;
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _controller = GetComponentInParent<DrillShotController>();
        _controller.OnActivateAction += Controller_OnActivateAction;
    }

    private void OnEnable()
    {

    }
    private void FixedUpdate()
    {
        rb.velocity = _speed * Time.fixedDeltaTime * transform.forward;
    }

    //private void OnDisable()
    //{
    //    _controller.OnActivateAction -= Controller_OnActivateAction;
    //}

    private void Controller_OnActivateAction(float speed, int damage)
    {
        _speed = speed;
        _damage = damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 collisionNormal = collision.contacts[0].normal;
        Vector3 reflectedVector = Vector3.Reflect(transform.forward, collisionNormal);

        Quaternion lookRotation = Quaternion.LookRotation(reflectedVector, Vector3.up);
        lookRotation.x = 0;
        lookRotation.z = 0;

        transform.rotation = lookRotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IDamageable enemy))
        {
            enemy.TakeDamage(_damage);
        }
    }
    private void OnDestroy()
    {
        _controller.OnActivateAction -= Controller_OnActivateAction;
    }
}
