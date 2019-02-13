using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinableMove : Combinable
{
    public float multiplier = 4.0f;
    public bool canJump = false;

    public override void HandleUpdate(Player2D player)
    {
        player.Locomotion(multiplier, canJump);
    }
}
