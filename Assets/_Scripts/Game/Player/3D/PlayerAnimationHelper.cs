using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHelper : MonoBehaviour {

    private Player3D player;

	// Use this for initialization
	void Start () {
        player = GetComponentInParent<Player3D>();
	}

    public void EndAction()
    {
        player.EndAction();
    }

    public void AddForce(float amount)
    {
        player.AddForce(amount);
    }
}
