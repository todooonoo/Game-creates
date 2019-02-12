using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerComponent2D : MonoBehaviour {

    public virtual void HandleUpdate(Player2D player) { }
    public virtual void HandleFixedUpdate(Player2D player) { }
}
