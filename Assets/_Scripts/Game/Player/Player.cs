using UnityEngine;
using System.Collections.Generic;

public enum PlayerState
{
    Idle,
    Action,
    Transition
}

public abstract class Player : MonoBehaviour
{
    public PlayerState playerState;
    [SerializeField] protected float speed = 3.0f;

    protected Renderer[] renderers;
    protected float targetAlpha = 1.0f, currentAlpha = 1.0f;
    
    // Dragging
    protected Draggable lastDraggable;
    protected InputPair interactInput;
    [HideInInspector] public Vector3 dragDirection;

    [SerializeField]
    protected Vector3[] directions = new Vector3[] { Vector3.left, Vector3.right, Vector3.forward, -Vector3.forward };
    public bool Pushing { get; protected set; }

    protected virtual void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        interactInput = InputHandler.Instance.GetInput(InputAction.Interact);
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
        get { return speed; }
    }

    public void SetLastDraggable(Draggable draggable)
    {
        if(!Pushing && lastDraggable != draggable)
        {
            lastDraggable = draggable;
            lastDraggable.OnTrigger();
        }
    }

    public void ClearLastDraggable(Draggable draggable)
    {
        if (!Pushing)
        {
            if (lastDraggable == draggable)
            {
                lastDraggable = null;
            }
        }
    }

    public virtual void Stop()
    {
        playerState = PlayerState.Action;
    }

    public virtual void HandleUpdate()
    {
        if(lastDraggable)
        {
            GameManager.Instance.ShowInteractIcon(lastDraggable.InteractUIPos);
            if (interactInput.GetAxisDown)
            {
                SetDragDirection(lastDraggable);
                Pushing = lastDraggable.OnDrag(transform);
                return;
            }
            else if(interactInput.GetAxisUp)
            {
                lastDraggable.OnRelease(transform);
                Pushing = false;
            } 
        } else
        {
            GameManager.Instance.HideInteractIcon();
        }
    }

    public virtual void SetDragDirection(Draggable lastDraggable)
    {
        if(lastDraggable.lookAtTarget)
        {
            transform.LookAt(lastDraggable.transform);
            return;
        }

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
