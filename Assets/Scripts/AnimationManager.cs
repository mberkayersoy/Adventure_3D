using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement playerMovement;


    // Animation parameteres 
    private int animSpeedID;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        AssignAnimationIDs();
        playerMovement.OnSpeedChangeAction += PlayerMovement_OnSpeedChangeAction;
    }

    private void PlayerMovement_OnSpeedChangeAction(float speed)
    {
        animator.SetFloat(animSpeedID, speed);
    }

    private void AssignAnimationIDs()
    {
        animSpeedID = Animator.StringToHash("Speed");
    }
}
