﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Invector.CharacterController;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerMoveController : PlayerComponent
{
    public bool Moving { get; private set; }
    
    private RigidbodyConstraints defaultConstraints, pullConstraints;

    private Rigidbody rBody;
    private InputPair jumpInput;
    private Vector2 moveDelta = Vector2.up;
    private ThirdPersonCharacter controller;

    private void Start()
    {
        rBody = GetComponent<Rigidbody>();
        jumpInput = InputHandler.Instance.GetInput(InputAction.Jump);
        controller = GetComponent<ThirdPersonCharacter>();
        defaultConstraints = rBody.constraints;
        pullConstraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
    }
    
    public override void HandleUpdate(Player3D player)
    {
        HandleMove(player);
        HandleJump(player);
    }

    public override void HandleFixedUpdate(Player3D player)
    {
    }

    private void HandleMove(Player3D player)
    {
        if (player.playerState == PlayerState.Action)
        {
            Stop(player);
            player.CurrentSFX = null;
            return;
        }
        if (player.AnimationController.IsJumping)
            CheckJumpAnimation(player);

        var lookDir = WorldManager.Instance.GameCamera.LookDirection;
        lookDir.y = 0;
        lookDir.Normalize();
        var angle = Vector2.SignedAngle(Vector2.up, moveDelta);
        var dir = Quaternion.Euler(0, -angle, 0) * lookDir;

        if (controller.IsGrounded && (player.Pushing || player.Pulling))
        {
            transform.LookAt(transform.position - player.dragDirection);
            controller.Move(-player.dragDirection, false, false, true, player.Pulling);
            player.AnimationController.SetState(PlayerAnimationState.Push);
            rBody.constraints = pullConstraints;
            player.CurrentSFX = player.pushPullSFX;
            return;
        }
        rBody.constraints = defaultConstraints;
        moveDelta = new Vector2(Input.GetAxis(Static.horizontalAxis), Input.GetAxis(Static.verticalAxis));
        
        if ((moveDelta.x != 0 || moveDelta.y != 0) && player.playerState != PlayerState.Transition)
        {
            transform.LookAt(transform.position + dir);

            bool grounded = controller.IsGrounded;
            controller.Move(dir, false, !player.Pushing && jumpInput.GetAxisDown, player.Pushing);

            // Animation
            if (grounded && !controller.IsGrounded)
            {
                player.AnimationController.SetState(PlayerAnimationState.Jump);
                player.CurrentSFX = null;
            } else
            {
                player.AnimationController.SetState(PlayerAnimationState.Move);
                player.CurrentSFX = player.walkHard ? player.walkHardSFX : player.walkSoftSFX;
            }
            Moving = true;
        }
        else
        {
            if (player.AnimationController.IsJumping)
            {
                CheckJumpAnimation(player);
            } else
            {
                if (controller.IsGrounded && (player.Pushing || player.Pulling))
                {
                    player.AnimationController.SetState(PlayerAnimationState.PushStart);
                    player.CurrentSFX = player.pushPullSFX;
                }
                else
                {
                    player.AnimationController.SetState(PlayerAnimationState.Idle);
                    player.CurrentSFX = null;
                }
            }
            Stop(player);
        }
    }

    private void CheckJumpAnimation(Player3D player)
    {
        if (rBody.velocity.y < 0)
        {
            player.AnimationController.SetState(PlayerAnimationState.Fall);

            if (controller.IsGrounded)
            {
                player.AnimationController.SetState(PlayerAnimationState.Land);
            }
        } else
        {
            if (controller.IsGrounded)
            {
                player.AnimationController.SetState(PlayerAnimationState.Land);
            }
        }
    }

    private void HandleJump(Player3D player)
    {
        /*
        if(jumpInput.GetAxisDown)
        {
            if(controller.Jump())
            {
                player.AnimationController.SetState(PlayerAnimationState.Jump);
            }
        }
        */
    } 

    public void Stop(Player3D player)
    {
        Moving = false;

        bool grounded = controller.IsGrounded;
        controller.Move(Vector3.zero, false, !player.Pushing && jumpInput.GetAxisDown);
        
        // Animation
        if (grounded && !controller.IsGrounded)
        {
            player.AnimationController.SetState(PlayerAnimationState.Jump);
        }
        else
        {
            player.AnimationController.SetState(PlayerAnimationState.Idle);
        }
        player.CurrentSFX = null;
    }
}