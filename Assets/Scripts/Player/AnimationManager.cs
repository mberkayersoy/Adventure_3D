using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class AnimationManager : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement playerMovement;


    // Animation parameteres 
    private int animSpeedID;
    private int animIDJump;
    private int animIDGrounded;
    private int animIDFreeFall;
    private int animIDMotionSpeed;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        AssignAnimationIDs();
        playerMovement.OnSpeedChangeAction += PlayerMovement_OnSpeedChangeAction;
        playerMovement.OnAnimationBlendChangeAction += PlayerMovement_OnAnimationBlendChangeAction;
        playerMovement.OnGroundStateChangeAction += PlayerMovement_OnGroundStateChangeAction;
        playerMovement.OnJumpAction += PlayerMovement_OnJumpAction;
        playerMovement.OnFreeFallAction += PlayerMovement_OnFreeFallAction;
    }
    private void PlayerMovement_OnFreeFallAction(bool isFreeFall)
    {
        animator.SetBool(animIDFreeFall, isFreeFall);
    }

    private void PlayerMovement_OnJumpAction(bool isJumping)
    {
        animator.SetBool(animIDJump, isJumping);
    }

    private void PlayerMovement_OnGroundStateChangeAction(bool isGrounded)
    {
        animator.SetBool(animIDGrounded, isGrounded);
    }

    private void PlayerMovement_OnAnimationBlendChangeAction(float animationBlend)
    {
        animator.SetFloat(animIDMotionSpeed, animationBlend);
    }

    private void PlayerMovement_OnSpeedChangeAction(float speed)
    {
        animator.SetFloat(animSpeedID, speed);
    }
    private void AssignAnimationIDs()
    {
        animSpeedID = Animator.StringToHash("Speed");
        animIDJump = Animator.StringToHash("Jump");
        animIDGrounded = Animator.StringToHash("Grounded");
        animIDFreeFall = Animator.StringToHash("FreeFall");
        animIDMotionSpeed = Animator.StringToHash("MotionSpeed");

    }

    private void OnFootstep(AnimationEvent animationEvent)
    {
        //if (animationEvent.animatorClipInfo.weight > 0.5f)
        //{
        //    if (FootstepAudioClips.Length > 0)
        //    {
        //        var index = Random.Range(0, FootstepAudioClips.Length);
        //        AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.TransformPoint(_controller.center), FootstepAudioVolume);
        //    }
        //}
    }

    private void OnLand(AnimationEvent animationEvent)
    {
        //if (animationEvent.animatorClipInfo.weight > 0.5f)
        //{
        //    AudioSource.PlayClipAtPoint(LandingAudioClip, transform.TransformPoint(_controller.center), FootstepAudioVolume);
        //}
    }
}
