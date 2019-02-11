using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAnimationState
{
    Idle,
    Move,
    PushStart,
    Push,
    PushEnd,
    Jump,
    Fall,
    Land
}

[System.Serializable]
public struct PlayerAnimationStruct
{
    public PlayerAnimationState state;
    public string stateName;
    public float transitionTime;

    public PlayerAnimationStruct(PlayerAnimationState state, string stateName, float transitionTime)
    {
        this.state = state;
        this.stateName = stateName;
        this.transitionTime = transitionTime;
    }
}

public class PlayerAnimationController : MonoBehaviour {

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private PlayerAnimationStruct[] animationStructs;
    [SerializeField]
    private float landingTime = 0.5f;
    private float currentLandingTime;

    [HideInInspector]
    public PlayerAnimationState currentState;

    public bool IsJumping { get; private set; }



	// Use this for initialization
	void Start ()
    {
        animator = animator ?? GetComponentInChildren<Animator>();
	}

    public void SetState(PlayerAnimationState state)
    {
        if (currentState == state)
            return;

        if (currentState != PlayerAnimationState.Jump && state == PlayerAnimationState.Fall)
            return;
        if (currentState != PlayerAnimationState.Fall && state == PlayerAnimationState.Land)
            return;
        if(currentState == PlayerAnimationState.Land && (state == PlayerAnimationState.Idle || state == PlayerAnimationState.Move))
        {
            currentLandingTime += Time.deltaTime;
            if (currentLandingTime < landingTime)
                return;
            else
                currentLandingTime = 0;
        }

        if (!IsJumping && state == PlayerAnimationState.Jump)
            IsJumping = true;
        else if (IsJumping && state == PlayerAnimationState.Land)
        {
            IsJumping = false;
        }
        else if (IsJumping && (state == PlayerAnimationState.Move || state == PlayerAnimationState.Push))
            return;

        for (int i = 0; i < animationStructs.Length; i++)
        {
            if(animationStructs[i].state == state)
            {
                if(state == PlayerAnimationState.Idle)
                {
                    ResetVars();
                }
                currentState = state;
                animator.CrossFade(animationStructs[i].stateName, animationStructs[i].transitionTime);
                return;
            }
        }
    }

    private void ResetVars()
    {
        IsJumping = false;
        currentLandingTime = 0;
    }
}
