using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController2D : MonoBehaviour {

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private PlayerAnimationStruct[] animationStructs;

    [HideInInspector]
    public PlayerAnimationState currentState;
    
	// Use this for initialization
	void Start ()
    {
        currentState = PlayerAnimationState.IdleRight2D;
        animator = animator ?? GetComponentInChildren<Animator>();
	}

    public void SetState(PlayerAnimationState state)
    {
        if (currentState == state)
            return;
        
        for (int i = 0; i < animationStructs.Length; i++)
        {
            if(animationStructs[i].state == state)
            {
                currentState = state;
                Debug.Log(animationStructs[i].stateName);
                animator.CrossFade(animationStructs[i].stateName, animationStructs[i].transitionTime);
                return;
            }
        }
    }
}
