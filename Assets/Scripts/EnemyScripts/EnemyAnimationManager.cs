using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationManager : MonoBehaviour
{
    private Animator animator;
    private Enemy enemyMovement;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyMovement = GetComponent<Enemy>();
    }
}
