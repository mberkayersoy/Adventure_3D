using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillInteraction : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private int damage;

    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable enemy))
        {
            enemy.TakeDamage(damage);
        }
        else
        {
            // Çarpýþan yüzeyin normal vektörü
            Vector3 collisionNormal = collision.contacts[0].normal;
            Vector3 direction = rb.velocity.normalized;

            Vector3 reflectedVector = Vector3.Reflect(direction, collisionNormal).normalized;

            rb.AddForce(reflectedVector * rb.velocity.magnitude, ForceMode.Force);
            //rb.velocity = reflectedVector * rb.velocity.magnitude;

            // Yeni rotasyon
            Quaternion lookRotation = Quaternion.LookRotation(rb.velocity, Vector3.up);
            lookRotation.x = 0;
            lookRotation.z = 0;
            transform.rotation = lookRotation;
        }
    }
}
