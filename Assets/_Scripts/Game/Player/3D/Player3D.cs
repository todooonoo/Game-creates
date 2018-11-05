using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player3D : Player {

    // Non-player component properties
    public PlayerAnimationController AnimationController { get; private set; }

    // Player components
    private PlayerComponent[] components;
    private Rigidbody rBody;

    [SerializeField] protected float fadeTime = 0.5f;

    protected override void Start()
    {
        base.Start();
        AnimationController = GetComponent<PlayerAnimationController>();
        components = GetComponentsInChildren<PlayerComponent>();
        renderers = GetComponentsInChildren<Renderer>();
        rBody = GetComponent<Rigidbody>();
    }

    public override void HandleUpdate()
    {
        for (int i = 0; i < components.Length; i++)
            components[i].HandleUpdate(this);
        SetAlpha();
    }

    public override void HandleFixedUpdate()
    {
        for (int i = 0; i < components.Length; i++)
            components[i].HandleFixedUpdate(this);
    }
    
    protected void SetAlpha()
    {
        var delta = targetAlpha - currentAlpha;
        if (delta == 0)
            return;
        currentAlpha = Mathf.Clamp(currentAlpha + (delta > 0 ? 1 : -1) * Time.deltaTime / fadeTime, 0, 1);
        for (int i = 0; i < renderers.Length; i++)
        {
            var mat = renderers[i].material;
            var color = mat.color;
            color.a = currentAlpha;
            mat.color = color;
        }
    }

    // Animation event
    public void EndAction()
    {
        if (playerState == PlayerState.Action)
        {
            playerState = PlayerState.Idle;
        }
    }

    public void AddForce(float amount)
    {
        rBody.AddForce(transform.forward * amount);
    }
}
