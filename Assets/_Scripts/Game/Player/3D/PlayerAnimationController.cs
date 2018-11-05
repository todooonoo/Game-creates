using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour {

    [SerializeField]
    private Animator animator;
    private static readonly string moveBoolStr = "Moving",
        attackTriggerStr = "Attack", weaponIntStr = "WeaponType";

	// Use this for initialization
	void Start ()
    {
        animator = animator ?? GetComponentInChildren<Animator>();
	}

    public void SetMove(bool moving)
    {
        animator.SetBool(moveBoolStr, moving);
    }

    public void TriggerAttack()
    {
        animator.SetTrigger(attackTriggerStr);
    }

    public void SetWeaponType(WeaponType weaponType)
    {
        animator.SetInteger(weaponIntStr, (int)weaponType);
    }
}
