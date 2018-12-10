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
    private MoveBehaviour moveBehaviour;

    private void Start()
    {
        rBody = GetComponent<Rigidbody>();
        jumpInput = InputHandler.Instance.GetInput(InputAction.Jump);
        moveBehaviour = GetComponent<MoveBehaviour>();
    }
    
    public override void HandleUpdate(Player3D player)
    {
        moveBehaviour.HandleUpdate();
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
        HandleMove(player);
    }

    private void HandleMove(Player3D player)
    {
        if (player.playerState == PlayerState.Action)
            return;

        moveDelta = new Vector2(Input.GetAxis(Static.horizontalAxis), Input.GetAxis(Static.verticalAxis));
        
        if (moveDelta.x != 0 || moveDelta.y != 0)
        {
            var lookDir = WorldManager.Instance.GameCamera.LookDirection;
            lookDir.y = 0;
            var angle = Vector2.SignedAngle(Vector2.up, moveDelta);
            var dir = Quaternion.Euler(0, -angle, 0) * lookDir;

            if (player.Dragging)
            {
                var dirModifier = Mathf.Abs(Vector3.SignedAngle(dir, transform.forward, Vector3.up)) < 90 ? 1.0f : -1.0f;
                transform.LookAt(transform.position - player.dragDirection);

                if (dirModifier > 0)
                    moveBehaviour.Move(moveDelta.x, moveDelta.y);
                else
                    moveBehaviour.Move(0, 0);
            } else
            {
                transform.LookAt(transform.position + dir);
                moveBehaviour.Move(moveDelta.x, moveDelta.y);
            }
            Moving = true;
        }
        else
        {
            Moving = false;
            moveBehaviour.Move(0, 0);
        }
    }

    public void Stop()
    {
        Moving = false;
        rBody.velocity = Vector3.up * rBody.velocity.y;
    }
    
}