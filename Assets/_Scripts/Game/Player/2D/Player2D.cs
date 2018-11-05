using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Movement2D
{
    Horizontal,
    Free,
}

public class Player2D : Player
{
    [SerializeField] private Movement2D movement;
    [SerializeField] private float speed;
    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundLayer;
    private InputPair jumpInput;

    [Header("Contact Check")]
    [SerializeField] private float contactDist = 0.05f;
    [SerializeField] private LayerMask contactLayer;

    private Rigidbody2D rBody;
    private Collider2D col;

    // Use this for initialization
    protected override void Start ()
    {
        base.Start();
        rBody = GetComponent<Rigidbody2D>();
        col = GetComponentInChildren<Collider2D>();
        jumpInput = InputHandler.Instance.GetInput(InputAction.Jump);
	}

    public override void HandleUpdate()
    {
        Move();

        if(jumpInput.GetAxisDown && IsGrounded())
        {
            Jump();
        }
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
            rBody.velocity = moveDelta * speed;
        }
        else if (movement == Movement2D.Horizontal)
        {
            rBody.velocity = new Vector2(moveDelta.x * speed, rBody.velocity.y);
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
        return Physics2D.Raycast(new Vector2(col.bounds.center.x, col.bounds.min.y), Vector2.down, contactDist, groundLayer);
    }

    public override bool CheckCollision()
    {
        return Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0, Vector2.zero, 0.01f, contactLayer);
    }
}
