using UnityEngine;
using System.Collections.Generic;
using UnityStandardAssets.Characters.ThirdPerson;

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
    protected InputPair interactInput, reverseInput;
    [HideInInspector] public Vector3 dragDirection;

    [SerializeField]
    protected Vector3[] directions = new Vector3[] { Vector3.left, Vector3.right, Vector3.forward, -Vector3.forward };
    public bool Pushing { get; protected set; }
    public bool Pulling { get; protected set; }

    // Hard fix
    private ThirdPersonCharacter controller;

    protected virtual void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        interactInput = InputHandler.Instance.GetInput(InputAction.Interact);
        reverseInput = InputHandler.Instance.GetInput(InputAction.Reverse);
        controller = GetComponent<ThirdPersonCharacter>();
    }

    protected virtual void OnDisable()
    {
        ClearLastDraggable(lastDraggable);
    }

    public void SetVisible(bool visible)
    {
        targetAlpha = visible ? 1.0f : 0.0f;
    }

    public virtual Collider2D CheckCollision()
    {
        // Do nothing in base 3D
        return null;
    }

    public float TargetSpeed
    {
        get { return speed; }
    }

    public void SetLastDraggable(Draggable draggable)
    {
        if(!Pushing && !Pulling && lastDraggable != draggable)
        {
            lastDraggable = draggable;
            lastDraggable.OnTrigger();
        }
    }

    public void ClearLastDraggable(Draggable draggable)
    {
        if (!Pushing && !Pulling)
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
        if (lastDraggable)
        {
            GameManager.Instance.ShowInteractIcon(lastDraggable.InteractUIPos);

            if (!controller.IsGrounded)
                return;

            if (!Pushing && reverseInput.GetAxis)
            {
                SetDragDirection(lastDraggable);
                Pulling = lastDraggable.OnDrag(transform, true);
                return;
            }
            else if (Pulling && reverseInput.GetAxisUp)
            {
                lastDraggable.OnRelease(transform);
                Pulling = false;
            }

            if (!Pulling && interactInput.GetAxis)
            {
                SetDragDirection(lastDraggable);
                Pushing = lastDraggable.OnDrag(transform);
                return;
            }
            else if (Pushing && interactInput.GetAxisUp)
            {
                lastDraggable.OnRelease(transform);
                Pushing = false;
            }
        }
        else
        {
            if(GameManager.Instance)
                GameManager.Instance.HideInteractIcon();
        }
    }

    public virtual void SetDragDirection(Draggable lastDraggable)
    {
        if(lastDraggable.lookAtTarget)
        {
            Vector3 targetPos = lastDraggable.transform.position;
            targetPos.y = transform.position.y;
            transform.LookAt(targetPos);
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
