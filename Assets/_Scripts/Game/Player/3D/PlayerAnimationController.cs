using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour {

    [SerializeField]
    private Animator animator;
    private static readonly string moveBoolStr = "Moving";

	// Use this for initialization
	void Start ()
    {
        animator = animator ?? GetComponentInChildren<Animator>();
	}

    public void SetMove(bool moving)
    {
        animator.SetBool(moveBoolStr, moving);
    }
}
