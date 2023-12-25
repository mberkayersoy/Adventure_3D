using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerBallInteraction : MonoBehaviour
{
    [SerializeField] private SoccerBallController _controller;
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private int _bounceCount;
    [SerializeField] private float _ballLifeTime;
    private float _remainingTime;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _controller = GetComponentInParent<SoccerBallController>();
        transform.SetParent(null, true);
    }

    private void Update()
    {
        if (_remainingTime >= 0)
        {
            _remainingTime -= Time.deltaTime;

            if (_remainingTime <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
    private void OnEnable()
    {
        _controller.OnActivateAction += Controller_OnActivateAction;
    }

    private void Controller_OnActivateAction(float speed, int damage, int bounceCount)
    {
        this._speed = speed;
        this._damage = damage;
        this._bounceCount = bounceCount;
        _remainingTime = _ballLifeTime;
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
        _bounceCount--;
        if (collision.gameObject.TryGetComponent(out IDamageable enemy))
        {
            enemy.TakeDamage(_damage);
            Debug.Log(collision.gameObject.name);
            Vector3 reflectionVector = Vector3.Reflect(_rb.velocity, Vector3.up);
            _rb.AddForce(reflectionVector.normalized * _rb.velocity.magnitude, ForceMode.Impulse);
        }
        if (_bounceCount <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
