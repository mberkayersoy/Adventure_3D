using System;
using System.Collections.Generic;
using UnityEngine;

public class LightningController : ActiveSkill
{
    [SerializeField] private int damage;
    [SerializeField] private float lightningRadius;
    [SerializeField] private const float detectionSize = 5;
    //[SerializeField] private Transform player;

    [SerializeField] private LightningInteraction attackPoint;
    [SerializeField] private Transform lightingInteractionParent;

    public event Action<Vector3, int, float> OnEnemyAttackAction;

    private new void Start()
    {
        base.Start();
    }
    private void FindEnemy()
    {
        // Calculate half extents for the cube (assuming it's centered at the player position)
        Vector3 halfExtents = new Vector3(detectionSize, detectionSize / 2f, detectionSize);

        Collider[] colliders = new Collider[20];

        // Find all colliders within the specified box
        int colliderCount = Physics.OverlapBoxNonAlloc(transform.position + Vector3.up * detectionSize / 2f, halfExtents, colliders, Quaternion.identity);

        List<Collider> enemyColliders = new List<Collider>();

        // Check each collider for IDamageable interface
        for (int i = 0; i < colliderCount; i++)
        {
            // If the collider has IDamageable interface, add it to the list
            if (colliders[i].TryGetComponent<IDamageable>(out var damageable))
            {
                enemyColliders.Add(colliders[i]);
            }
        }

        if (enemyColliders.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, enemyColliders.Count);
            Collider randomCollider = enemyColliders[randomIndex];

            lightingInteractionParent.gameObject.SetActive(IsActive);
            OnEnemyAttackAction?.Invoke(randomCollider.transform.position, damage, lightningRadius);
        }
    }
    
    private void OnDrawGizmos()
    {
        // Calculate half extents for the cube (assuming it's centered at the player position)
        Vector3 halfExtents = new Vector3(detectionSize, detectionSize / 2f, detectionSize);

        // Draw the wire cube
        Gizmos.DrawWireCube(transform.position + Vector3.up * detectionSize / 2f, 2 * halfExtents);
    }

    public override void Activate()
    {
        base.Activate();
        FindEnemy();
    }
    public override void DeActivate()
    {
        base.DeActivate();
        lightingInteractionParent.gameObject.SetActive(IsActive);
    }

    public override void UpgradeSkill()
    {
        throw new NotImplementedException();
    }
}
