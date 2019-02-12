﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Movement2D
{
    Horizontal,
    Free,
}

public class Player2D : Player
{
    public PlayerAnimationController2D AnimationController { get; private set; }

    [SerializeField] private Movement2D movement;
    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundLayer;
    private InputPair jumpInput, transitionInput;
    private const string transitionSFXName = "Transition";

    [Header("Contact Check")]
    [SerializeField] private float contactDist = 0.05f;
    [SerializeField] private LayerMask contactLayer;

    private Rigidbody2D rBody;
    private Collider2D col;
    private int direction = 1;

    // Use this for initialization
    protected override void Start ()
    {
        base.Start();
        rBody = GetComponent<Rigidbody2D>();
        col = GetComponentInChildren<Collider2D>();
        AnimationController = GetComponent<PlayerAnimationController2D>();
        jumpInput = InputHandler.Instance.GetInput(InputAction.Jump);
        transitionInput = InputHandler.Instance.GetInput(InputAction.TransitionMain);
    }

    public override void HandleUpdate()
    {
        bool grounded = IsGrounded();

        if(grounded && transitionInput.GetAxisDown)
        {
            WorldManager.Instance.Transition(WorldType.World3D);
            AudioManager.Instance.PlaySFX(transitionSFXName);
            return;
        }
        Move();

        if(jumpInput.GetAxisDown && grounded)
        {
            Jump();
        }
        base.HandleUpdate();
    }

    public override void HandleFixedUpdate()
    {
        CheckContact();
    }

    private void Move()
    {
        var moveDelta = new Vector2(Input.GetAxis(Static.horizontalAxis), Input.GetAxis(Static.verticalAxis));

        if (movement == Movement2D.Free)
        {
            rBody.velocity = moveDelta * TargetSpeed;

            if(moveDelta.x != 0 || moveDelta.y != 0)
            {
                AnimationController.SetState(PlayerAnimationState.Move);
            } else
            {
                AnimationController.SetState(PlayerAnimationState.Idle);
            }
        }
        else if (movement == Movement2D.Horizontal)
        {
            rBody.velocity = new Vector2(moveDelta.x * TargetSpeed, rBody.velocity.y);

            if(moveDelta.x == 0)
            {
                bool right = direction > 0;
                AnimationController.SetState(
                    right ? PlayerAnimationState.IdleRight2D : PlayerAnimationState.IdleLeft2D);
            } else
            {
                bool right = moveDelta.x > 0;
                AnimationController.SetState(
                    right ? PlayerAnimationState.MoveRight2D : PlayerAnimationState.MoveLeft2D);
                direction = right ? 1 : -1;
            }
        }
    }

    private void Jump()
    {
        rBody.velocity = new Vector2(rBody.velocity.x, jumpForce);
    }

    private void CheckContact()
    {
        if (CheckContact(new Vector2(col.bounds.center.x, col.bounds.min.y), Vector2.down))
            return;
        if (CheckContact(new Vector2(col.bounds.max.x, col.bounds.center.y), Vector2.right))
            return;
        if (CheckContact(new Vector2(col.bounds.min.x, col.bounds.center.y), Vector2.left))
            return;
    }

    private bool CheckContact(Vector2 origin, Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, dir, contactDist, contactLayer);
        if (hit)
        {
            var contact = hit.collider.GetComponent<WorldObjectCloneContact>();

            if (contact)
            {
                contact.SetContactPos(this);
                return true;
            }
        }
        return false;
    }

    private bool IsGrounded()
    {
        if (!col)
            Start();
        return Physics2D.Raycast(new Vector2(col.bounds.center.x, col.bounds.min.y), Vector2.down, contactDist, groundLayer);
    }

    public override Collider2D CheckCollision()
    {
        return Physics2D.OverlapBox(col.bounds.center, col.bounds.size, 0, contactLayer);
    }
    
    public override void SetCollision(bool active)
    {
        if(!rBody)
            rBody = GetComponent<Rigidbody2D>();
        rBody.isKinematic = !active;
    }
}
