using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningController : Skill
{
    [SerializeField] private int damage;
    [SerializeField] private float lightningRadius;
    [SerializeField] private const float detectionSize = 5;
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask enemyLayer;

    [SerializeField] private LightningInteraction attackPoint;
    [SerializeField] private Transform lightingInteractionParent;

    public event Action<Vector3, int, float> OnEnemyAttackAction;

    private void FindNearestEnemy()
    {
        // Calculate half extents for the cube (assuming it's centered at the player position)
        Vector3 halfExtents = new Vector3(detectionSize, detectionSize / 2f, detectionSize);

        // Find all colliders within the specified radius on the "Enemy" layer
        Collider[] colliders = Physics.OverlapBox(player.position + Vector3.up * detectionSize / 2f, 2 * halfExtents, Quaternion.identity, enemyLayer);

        if (colliders.Length > 0)
        {
            Collider nearestCollider = colliders[0];
            float nearestDistance = Vector3.Distance(player.position, nearestCollider.transform.position);

            // Iterate through all colliders to find the nearest one
            foreach (Collider collider in colliders)
            {
                float distance = Vector3.Distance(player.position, collider.transform.position);

                if (distance < nearestDistance)
                {
                    nearestCollider = collider;
                    nearestDistance = distance;
                }
            }
            lightingInteractionParent.gameObject.SetActive(IsActive);
            OnEnemyAttackAction?.Invoke(nearestCollider.transform.position, damage, lightningRadius);
        }
    }

    private void OnDrawGizmos()
    {
        // Calculate half extents for the cube (assuming it's centered at the player position)
        Vector3 halfExtents = new Vector3(detectionSize, detectionSize / 2f, detectionSize);

        // Draw the wire cube
        Gizmos.DrawWireCube(player.position + Vector3.up * detectionSize / 2f, 2 * halfExtents);
    }

    public override void Activate()
    {
        base.Activate();
        FindNearestEnemy();
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
