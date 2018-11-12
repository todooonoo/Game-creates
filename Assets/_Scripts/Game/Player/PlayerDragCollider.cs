using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDragCollider : MonoBehaviour {

    private Player player;

	// Use this for initialization
	void Start ()
    {
        player = GetComponentInParent<Player>();
	}

    private void OnTriggerEnter(Collider other)
    {
        var draggable = other.GetComponentInParent<Draggable>();

        if(draggable)
        {
            player.SetLastDraggable(draggable);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var draggable = other.GetComponentInParent<Draggable>();

        if (draggable)
        {
            player.SetLastDraggable(draggable);
        }
    }
}
