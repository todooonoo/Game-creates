using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Invector.CharacterController;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerMoveController : PlayerComponent
{
    public bool Moving { get; private set; }

    private Rigidbody rBody;
    private InputPair jumpInput;
    private Vector2 moveDelta = Vector2.up;
    private ThirdPersonCharacter controller;

    private void Start()
    {
        rBody = GetComponent<Rigidbody>();
        jumpInput = InputHandler.Instance.GetInput(InputAction.Jump);
        controller = GetComponent<ThirdPersonCharacter>();
    }
    
    public override void HandleUpdate(Player3D player)
    {
        HandleMove(player);
        // cc.UpdateMotor();

        // Jump
        /*
        if (!player.Dragging && jumpInput.GetAxisDown)
            controller.Jump();
            */
    }

    public override void HandleFixedUpdate(Player3D player)
    {
        /*
        if(!player.Dragging)
            cc.AirControl();
            */
    }

    private void HandleMove(Player3D player)
    {
        if (player.playerState == PlayerState.Action)
            return;

        moveDelta = new Vector2(Input.GetAxis(Static.horizontalAxis), Input.GetAxis(Static.verticalAxis));
        
        if (moveDelta.x != 0 || moveDelta.y != 0)
        {
            // cc.input = moveDelta;
            var lookDir = WorldManager.Instance.GameCamera.LookDirection;
            lookDir.y = 0;
            var angle = Vector2.SignedAngle(Vector2.up, moveDelta);
            var dir = Quaternion.Euler(0, -angle, 0) * lookDir;

            if (player.Dragging)
            {
                var dirModifier = Mathf.Abs(Vector3.SignedAngle(dir, transform.forward, Vector3.up)) < 90 ? 1.0f : -1.0f;
                transform.LookAt(transform.position - player.dragDirection);

                if (dirModifier > 0)
                    controller.Move(-player.dragDirection, false, false, true);
                else
                    controller.Move(Vector3.zero, false, false, true);
            } else
            {
                transform.LookAt(transform.position + dir);
                controller.Move(dir, false, !player.Dragging && jumpInput.GetAxisDown, player.Dragging);
            }
            // cc.ControlSpeed(dirModifier * player.TargetSpeed);
            Moving = true;
        }
        else
        {
            Moving = false;
            // cc.input.x = cc.input.y = 0;
            // cc.ControlSpeed(0);
            controller.Move(Vector3.zero, false, !player.Dragging && jumpInput.GetAxisDown);
        }
        // player.AnimationController.SetMove(Moving);
    }

    public void Stop()
    {
        Moving = false;
        rBody.velocity = Vector3.up * rBody.velocity.y;
    }
    
}