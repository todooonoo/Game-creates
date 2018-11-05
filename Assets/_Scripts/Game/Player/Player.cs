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
    protected Renderer[] renderers;
    protected float targetAlpha = 1.0f, currentAlpha = 1.0f;

    protected virtual void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
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

    public abstract void HandleUpdate();
    public abstract void HandleFixedUpdate();

}
