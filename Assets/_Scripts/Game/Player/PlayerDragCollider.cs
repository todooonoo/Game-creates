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

        if (draggable)
        {
            Debug.Log(draggable.name);
            player.SetLastDraggable(draggable);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var draggable = other.GetComponentInParent<Draggable>();

        if(draggable)
        {
            player.SetLastDraggable(draggable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var draggable = other.GetComponentInParent<Draggable>();

        if (draggable)
        {
            player.ClearLastDraggable(draggable);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        var draggable = other.GetComponentInParent<Draggable>();

        if (draggable)
        {
            player.SetLastDraggable(draggable);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var draggable = other.GetComponentInParent<Draggable>();

        if (draggable)
        {
            player.ClearLastDraggable(draggable);
        }
    }
}
