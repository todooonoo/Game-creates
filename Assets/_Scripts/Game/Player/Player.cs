using UnityEngine;
using System.Collections.Generic;

public enum PlayerState
{
    Idle,
    Action
}

public abstract class Player : MonoBehaviour
{
    public PlayerState playerState;
    [SerializeField] protected float speed = 3.0f;

    protected Renderer[] renderers;
    protected float targetAlpha = 1.0f, currentAlpha = 1.0f;

    // Dash
    protected InputPair dashInput;
    protected float dashAmount;

    // Dragging
    protected Draggable lastDraggable;
    protected InputPair dragInput;
    [HideInInspector] public Vector3 dragDirection;

    [SerializeField]
    protected Vector3[] directions = new Vector3[] { Vector3.left, Vector3.right, Vector3.forward, -Vector3.forward };
    public bool Pushing { get; protected set; }

    protected virtual void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        dashInput = InputHandler.Instance.GetInput(InputAction.Dash);
        dragInput = InputHandler.Instance.GetInput(InputAction.Drag);
    }

    public void SetVisible(bool visible)
    {
        targetAlpha = visible ? 1.0f : 0.0f;
    }

    public virtual bool CheckCollision()
    {
        // Do nothing in base 3D
        return false;
    }

    public float TargetSpeed
    {
        get { return dashInput.GetAxis ? speed * 2 : speed; }
    }

    public void SetLastDraggable(Draggable draggable)
    {
        if(!Pushing)
        {
            lastDraggable = draggable;
        }
    }

    public void ClearLastDraggable(Draggable draggable)
    {
        if (!Pushing)
        {
            if (lastDraggable == draggable)
                lastDraggable = null;
        }
    }

    public virtual void HandleUpdate()
    {
        if(lastDraggable)
        {
            if (dragInput.GetAxisDown)
            {
                SetDragDirection(lastDraggable);
                lastDraggable.OnDrag(transform);
                Pushing = true;
            }
            else if(dragInput.GetAxisUp)
            {
                lastDraggable.OnRelease(transform);
                Pushing = false;
            }
        } 
    }

    public virtual void SetDragDirection(Draggable lastDraggable)
    {
        var delta = transform.position - lastDraggable.transform.position;
        var angle = float.MaxValue;
        for (int i = 0; i < directions.Length; i++)
        {
            var tempAngle = Mathf.Abs(Vector3.SignedAngle(delta, directions[i], Vector3.up));

            if(tempAngle < angle)
            {
                angle = tempAngle;
                dragDirection = directions[i];
            }
        }
        transform.LookAt(transform.position - dragDirection);
    }

    public abstract void HandleFixedUpdate();
    public abstract void SetCollision(bool active);
}
