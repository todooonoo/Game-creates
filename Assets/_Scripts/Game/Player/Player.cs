using UnityEngine;
using System.Collections;

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

    protected virtual void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        dashInput = InputHandler.Instance.GetInput(InputAction.Dash);
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

    public abstract void HandleUpdate();
    public abstract void HandleFixedUpdate();
    public abstract void SetCollision(bool active);
}
