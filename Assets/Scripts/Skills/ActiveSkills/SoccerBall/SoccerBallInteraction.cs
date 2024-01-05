using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerBallInteraction : MonoBehaviour
{
    [SerializeField] private SoccerBallController _controller;
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private float _ballLifeTime;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _controller = GetComponentInParent<SoccerBallController>();
        transform.SetParent(null, true);
    }

    private void OnEnable()
    {
        _controller.OnActivateAction += Controller_OnActivateAction;
    }

    private void Controller_OnActivateAction(float speed, int damage)
    {
        _speed = speed;
        _damage = damage;
        ThrowTheBall();
    }

    private void ThrowTheBall()
    {
        _rb.velocity = Vector3.zero; // reset the velocity
        // Add Force Random direction
        Vector3 randomDirection = Random.onUnitSphere;
        randomDirection.y = 0f;
        _rb.AddForce(randomDirection * _speed, ForceMode.Impulse);

    }

    private void OnDisable()
    {
        _controller.OnActivateAction -= Controller_OnActivateAction;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable enemy))
        {
            enemy.TakeDamage(_damage);
            //Vector3 reflectionVector = Vector3.Reflect(_rb.velocity, Vector3.up);
            //_rb.AddForce(reflectionVector.normalized * _rb.velocity.magnitude, ForceMode.Impulse);
        }   
    }
}
