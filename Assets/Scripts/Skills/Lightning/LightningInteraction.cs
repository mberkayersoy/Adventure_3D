using UnityEngine;

public class LightningInteraction : MonoBehaviour
{
    [SerializeField] private LightningController lightningController;
    private int damage;
    private SphereCollider sphereCollider;

    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }
    private void OnEnable()
    {
        lightningController.OnEnemyAttackAction += LightningController_OnEnemyAttackAction;
    }

    private void LightningController_OnEnemyAttackAction(Vector3 position, int damage, float radius)
    {
        transform.position = position;
        this.damage = damage;
        sphereCollider.radius = radius;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable enemy))
        {
            enemy.TakeDamage(damage);
        }
    }

    private void OnDisable()
    {
        lightningController.OnEnemyAttackAction -= LightningController_OnEnemyAttackAction;
    }
}
