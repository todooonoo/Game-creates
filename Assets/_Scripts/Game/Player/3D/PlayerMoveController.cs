﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Invector.CharacterController;

public class PlayerMoveController : PlayerComponent
{
    public bool Moving { get; private set; }

    private Rigidbody rBody;
    private InputPair jumpInput;
    private vThirdPersonController cc;
    private Vector2 moveDelta = Vector2.up;

    private void Start()
    {
        rBody = GetComponent<Rigidbody>();
        cc = GetComponent<vThirdPersonController>();
        cc.Init();
        jumpInput = InputHandler.Instance.GetInput(InputAction.Jump);
    }
    
    public override void HandleUpdate(Player3D player)
    {
        HandleMove(player);
        cc.UpdateMotor();

        // Jump
        if (jumpInput.GetAxisDown)
            cc.Jump();
    }

    public override void HandleFixedUpdate(Player3D player)
    {
        cc.AirControl();
    }

    private void HandleMove(Player3D player)
    {
        if (player.playerState == PlayerState.Action)
            return;

        moveDelta = new Vector2(Input.GetAxis(Static.horizontalAxis), Input.GetAxis(Static.verticalAxis));
        
        if (moveDelta.x != 0 || moveDelta.y != 0)
        {
            cc.input = moveDelta;
            var lookDir = WorldManager.Instance.GameCamera.LookDirection;
            lookDir.y = 0;
            var angle = Vector2.SignedAngle(Vector2.up, moveDelta);
            transform.LookAt(transform.position + Quaternion.Euler(0, -angle, 0) * lookDir);
            cc.ControlSpeed(player.TargetSpeed);
            Moving = true;
        }
        else
        {
            cc.input.x = cc.input.y = 0;
            Moving = false;
            cc.ControlSpeed(0);
        }
        player.AnimationController.SetMove(Moving);
    }

    public void Stop()
    {
        Moving = false;
        rBody.velocity = Vector3.up * rBody.velocity.y;
    }
}