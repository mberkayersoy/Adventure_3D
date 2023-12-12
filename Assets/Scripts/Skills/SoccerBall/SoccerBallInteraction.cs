using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerBallInteraction : MonoBehaviour
{
    [SerializeField] SoccerBallController controller;
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private int bounceCount;
    [SerializeField] private float ballLifeTime;
    private float remainingTime;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponentInParent<SoccerBallController>();
        transform.SetParent(null, true);

    }

    private void Update()
    {
        if (remainingTime >= 0)
        {
            remainingTime -= Time.deltaTime;

            if (remainingTime <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
    private void OnEnable()
    {
        controller.OnActivateAction += Controller_OnActivateAction;
    }

    private void Controller_OnActivateAction(float speed, int damage, int bounceCount)
    {
        this.speed = speed;
        this.damage = damage;
        this.bounceCount = bounceCount;
        remainingTime = ballLifeTime;
        ThrowTheBall();
    }

    private void ThrowTheBall()
    {
        rb.velocity = Vector3.zero; // reset the velocity
        // Add Force Random direction
        Vector3 randomDirection = Random.onUnitSphere;
        randomDirection.y = 0f;
        rb.AddForce(randomDirection * speed, ForceMode.Impulse);

    }

    private void OnDisable()
    {
        controller.OnActivateAction -= Controller_OnActivateAction;
    }
    private void OnCollisionEnter(Collision collision)
    {
        bounceCount--;
        if (collision.gameObject.TryGetComponent(out IDamageable enemy))
        {
            enemy.TakeDamage(damage);
            Debug.Log(collision.gameObject.name);
            //rb.AddForce(-rb.velocity * speed, ForceMode.VelocityChange);
        }
        if (bounceCount <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
